namespace DRAMA.EventBindings;

[Binding]
internal sealed class AzureDevOpsIntegrationBindings
{
    [BeforeFeature(Order = 2)]
    public static void BeforeFeature(FeatureContext featureContext)
    {
        if (TestRunContext.Profile.Integrations?.AzureDevOps?.Integration is null) return;

        Regex regex = new(@"^AZURE:AreaPath=""(?<path>.*)"";BugCategory=""(?<category>.*)"";BugSeverity=""(?<severity>.*)"";BugPriority=""(?<priority>.*)""$");

        string? tag = featureContext.FeatureInfo.Tags.SingleOrDefault(tag => regex.IsMatch(tag));

        string project = TestRunContext.Profile.Integrations?.AzureDevOps?.Project ?? throw new NullReferenceException("Azure DevOps Integration Project Is NULL");

        if (tag is null)
        {
            StringBuilder error = new StringBuilder()
                .AppendLine("An Azure DevOps integration is enabled, but the feature tag required by the integration was not found.")
                .AppendLine(@"The expected tag format is: @AZURE:AreaPath=""{AreaPath}"";BugCategory=""{BugCategory}"";BugSeverity=""{BugSeverity}"";BugPriority=""{BugPriority}"".")
                .AppendLine($@"Where {{AreaPath}} should not include the project name, e.g. just ""AUTOMATION"" not ""{project}\AUTOMATION"".")
                .AppendLine(@"Any white space characters in ""{AreaPath}"", ""{BugCategory}"", ""{BugSeverity}"", or ""{BugPriority}"" should be replaced with underscore (""_"") characters.");

            throw new NoMatchException(error.ToString());
        }

        string path = regex.Match(tag).Groups["path"].Value.Replace('_', ' ');
        string category = regex.Match(tag).Groups["category"].Value.Replace('_', ' ');
        string severity = regex.Match(tag).Groups["severity"].Value.Replace('_', ' ');
        string priority = regex.Match(tag).Groups["priority"].Value.Replace('_', ' ');

        if (path.StartsWith(project))
            throw new ArgumentException($@"The project name is automatically added to the start of the area path. ""{project}\"" is not required.");

        if (new string[] { "1 - Critical", "2 - High", "3 - Medium", "4 - Low" }.Contains(severity).Equals(false))
            throw new NoMatchException($@"Bug severity ""{severity}"" does not match a supported value. The supported bug severity values are ""1 - Critical"", ""2 - High"", ""3 - Medium"", and ""4 - Low"".");

        if (new string[] { "1", "2", "3", "4" }.Contains(priority).Equals(false))
            throw new NoMatchException($@"Bug priority ""{priority}"" does not match a supported value. The supported bug priority values are ""1"", ""2"", ""3"", and ""4"". ""1"" is the highest priority.");

        featureContext.Set(key: "Azure DevOps Area Path", value: path);
        featureContext.Set(key: "Azure DevOps Bug Category", value: category);
        featureContext.Set(key: "Azure DevOps Bug Severity", value: severity);
        featureContext.Set(key: "Azure DevOps Bug Priority", value: priority);
    }

    [AfterStep(Order = 2)]
    public async Task AfterStep(FeatureContext featureContext, ScenarioContext scenarioContext)
    {
        if (TestRunContext.Profile.Integrations?.AzureDevOps?.Integration is null || scenarioContext.StepContext.Status is not ScenarioExecutionStatus.TestError || scenarioContext.TestError is null) return;

        AzureDevOpsIntegration integration = TestRunContext.Profile.Integrations.AzureDevOps.Integration;

        string title = $@"[INTEGRATION] ""{featureContext.FeatureInfo.Title}"" > ""{scenarioContext.ScenarioInfo.Title}"" > ""{scenarioContext.StepContext.StepInfo.Text}"" > ""{scenarioContext.TestError.Message}""";

        List<WorkItem> items = await integration.GetWorkItem("Bug", title);

        if (items.Count is 0)
        {
            string path = featureContext.Get<string>("Azure DevOps Area Path");
            string category = featureContext.Get<string>("Azure DevOps Bug Category");
            string severity = featureContext.Get<string>("Azure DevOps Bug Severity");
            string priority = featureContext.Get<string>("Azure DevOps Bug Priority");

            StringBuilder steps = new StringBuilder()
                .AppendLine($"<b>Feature:</b> {featureContext.FeatureInfo.Title}")
                .AppendLine("<br/>")
                .AppendLine($"<b>Scenario:</b> {scenarioContext.ScenarioInfo.Title}")
                .AppendLine("<br/>")
                .AppendLine($"<b>Step:</b> {scenarioContext.StepContext.StepInfo.Text}")
                .AppendLine("<br/>");

            string expected = scenarioContext.StepContext.StepInfo.Text;

            StringBuilder actual = new StringBuilder()
                .AppendLine($"<b>{scenarioContext.TestError.Message}</b>")
                .AppendLine("<br/>")
                .AppendLine(scenarioContext.TestError.StackTrace);

            StringBuilder description = new StringBuilder()
                .AppendLine("<b>Occurrences:</b>")
                .AppendLine("<br/>")
                .AppendLine($"<p>&nbsp;&nbsp;&nbsp;&nbsp;{DateTime.UtcNow:O}</p>");

            string project = TestRunContext.Profile.Integrations?.AzureDevOps?.Project ?? throw new NullReferenceException("Azure DevOps Integration Project Is NULL");

            CreateBugOptions options = new()
            {
                Project = project,
                AreaPath = path,
                StepsToReproduce = steps.ToString(),
                ExpectedOutcome = expected,
                ActualOutcome = actual.ToString(),
                BugCategory = category,
                BugSeverity = severity,
                BugPriority = priority,
                BugDescription = description.ToString()
            };

            List<JsonPatchOperation> operations = CreateBugJsonPatchOperations(options);

            WorkItem _ = await integration.CreateWorkItem("Microsoft.VSTS.WorkItemTypes.Bug", title, operations, simulation: false);
        }

        else if (items.Count is 1)
        {
            StringBuilder description = new StringBuilder(items.Single().Fields["System.Description"].ToString())
                .AppendLine($"<p>&nbsp;&nbsp;&nbsp;&nbsp;{DateTime.UtcNow:O}</p>");

            List<JsonPatchOperation> operations = UpdateBugJsonPatchOperations(description.ToString());

            WorkItem _ = await integration.UpdateWorkItem(items.Single(), operations, simulation: false);
        }

        else throw new DuplicateException($@"0 or 1 bugs with title ""{title}"" were expected, but {items.Count} were found.");
    }

    private static List<JsonPatchOperation> CreateBugJsonPatchOperations(CreateBugOptions options)
    {
        List<JsonPatchOperation> mandatoryOperations =
        [
            # region System Fields
            new JsonPatchOperation()
            {
                Operation = Operation.Add,
                Path = "/fields/System.AreaPath",
                Value = $@"{options.Project}\{options.AreaPath}"
            },

            new JsonPatchOperation()
            {
                Operation = Operation.Add,
                Path = "/fields/System.IterationPath",
                Value = options.Project
            },

            //  State
            //      New = for triage
            //      Active = not yet fixed
            //      Blocked = unable to progress
            //      Resolved = fixed but not yet verified
            //      Closed = fix verified
            new JsonPatchOperation()
            {
                Operation = Operation.Add,
                Path = "/fields/System.State",
                Value = "New"
            },
            # endregion

            # region Microsoft.VSTS Fields
            new JsonPatchOperation()
            {
                Operation = Operation.Add,
                Path = "/fields/Microsoft.VSTS.TCM.ReproSteps",
                Value = options.StepsToReproduce
            },
            # endregion

            // TODO: Revise JSON Patch Operation Custom Fields

            # region Custom Fields
            new JsonPatchOperation()
            {
                Operation = Operation.Add,
                Path = "/fields/Custom.ExpectedResults",
                Value = options.ExpectedOutcome
            },

            new JsonPatchOperation()
            {
                Operation = Operation.Add,
                Path = "/fields/Custom.ActualResults",
                Value = options.ActualOutcome
            },
            
            new JsonPatchOperation()
            {
                Operation = Operation.Add,
                Path = "/fields/Custom.Category",
                Value = options.BugCategory
            },

            new JsonPatchOperation()
            {
                Operation = Operation.Add,
                Path = "/fields/Custom.Regression",
                Value = true
            }
            # endregion
        ];

        List<JsonPatchOperation> optionalOperations =
        [
            new JsonPatchOperation()
            {
                Operation = Operation.Add,
                Path = "/fields/System.Reason",
                Value = "Build Failure"
            },

            //  Severity
            //  A subjective rating of the impact of a bug on the project. You can specify the following values:
            //      1 - Critical
            //      2 - High
            //      3 - Medium
            //      4 - Low
            new JsonPatchOperation()
            {
                Operation = Operation.Add,
                Path = "/fields/Microsoft.VSTS.Common.Severity",
                Value = options.BugSeverity
            },

            //  Priority
            //  A subjective rating of the bug, issue, task, or test case as it relates to the business. You can specify the following values:
            //      1: Highest priority, implement feature or fix as soon as possible. Product cannot ship without successful resolution.
            //      2: Medium priority. Product cannot ship without successful resolution, but it does not need to be addressed immediately.
            //      3: Low priority. Implementation or fix is optional based on resources, time, and risk. If product ships without successful resolution, document the issue in release notes as known issues.
            //      4: Lowest priority. Tracks an issue that basically does not affect usage (such as a small typo).
            new JsonPatchOperation()
            {
                Operation = Operation.Add,
                Path = "/fields/Microsoft.VSTS.Common.Priority",
                Value = options.BugPriority
            },

            new JsonPatchOperation()
            {
                Operation = Operation.Add,
                Path = "/fields/System.Description",
                Value = options.BugDescription
            }
        ];

        return [.. mandatoryOperations, .. optionalOperations];
    }

    private static List<JsonPatchOperation> UpdateBugJsonPatchOperations(string description)
    {
        List<JsonPatchOperation> operations =
        [
            new JsonPatchOperation()
            {
                Operation = Operation.Replace,
                Path = "/fields/System.Description",
                Value = description
            }
        ];

        return operations;
    }

    private class CreateBugOptions
    {
        internal required string Project { get; set; }
        internal required string AreaPath { get; set; }
        internal required string StepsToReproduce { get; set; }
        internal required string ExpectedOutcome { get; set; }
        internal required string ActualOutcome { get; set; }
        internal required string BugCategory { get; set; }
        internal required string BugSeverity { get; set; }
        internal required string BugPriority { get; set; }
        internal required string BugDescription { get; set; }
    }
}
