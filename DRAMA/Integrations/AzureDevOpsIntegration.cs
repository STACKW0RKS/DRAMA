namespace DRAMA.Integrations;

public class AzureDevOpsIntegration
{
    /// <summary>
    ///     Creates an Azure DevOps integration instance.
    ///     The value of "project" can be either a project name or a project GUID.
    /// </summary>
    public AzureDevOpsIntegration(string personalAccessToken, string host, string project)
    {
        Host = host; Project = project;

        Connection = new(new Uri($"https://{host}"), new VssBasicCredential(string.Empty, personalAccessToken));
        Connection.ConnectAsync().Run();
    }

    private string Host { get; set; }
    private string Project { get; set; }

    public VssConnection Connection { get; set; }

    /// <summary>
    ///     Provides the basic JSON Patch document for creating a work item.
    ///     This method, out of the box, only adds the JSON Patch document title.
    ///     All other JSON Patch document field operations need to be passed as a parameter.
    ///     
    ///     <code>
    ///         List&lt;JsonPatchOperation&gt; operations = new()
    ///         {
    ///             new JsonPatchOperation()
    ///             {
    ///                 Operation = Operation.Add,
    ///                 Path = "/fields/Custom.Category",
    ///                 Value = "AUTOMATED"
    ///             }
    ///         };
    ///     </code>
    ///     
    ///     The value of "type" can be either a work item type name (e.g. "Bug") or a work item type reference (e.g. "Microsoft.VSTS.WorkItemTypes.Bug").
    /// </summary>
    public async Task<WorkItem> CreateWorkItem(string type, string title, List<JsonPatchOperation> operations, bool simulation = false)
    {
        JsonPatchDocument document =
        [
            new JsonPatchOperation()
            {
                Operation = Operation.Add,
                Path = "/fields/System.Title",
                Value = title
            }
        ];

        document.AddRange(operations);

        return await Connection.GetClient<WorkItemTrackingHttpClient>().CreateWorkItemAsync(document, Project, type, validateOnly: simulation);
    }

    /// <summary>
    ///     Retrieves all instances of a work item by type, title, and, optionally, by state.
    ///     The value of "type" is the work item name (e.g. "Bug"), not the work item type reference (e.g. "Microsoft.VSTS.WorkItemTypes.Bug").
    /// </summary>
    public async Task<List<WorkItem>> GetWorkItem(string type, string title, string stateFilter = "<> 'Closed'", string createdFilter = "> @today-365")
    {
        Wiql query = new() // https://learn.microsoft.com/en-gb/azure/devops/boards/queries/wiql-syntax
        {
            Query = new StringBuilder()
                .AppendLine("SELECT [System.Id]")
                .AppendLine("FROM WorkItems")
                .AppendLine($"WHERE [System.TeamProject] = '{Project}'")
                .AppendLine($"AND [System.WorkItemType] = '{type}'")
                .AppendLine($"AND [System.Title] = '{title}'")
                .AppendLine($"AND [System.State] {stateFilter}")
                .AppendLine($"AND [System.CreatedDate] {createdFilter}")
                .AppendLine("ORDER BY [System.Id] DESC")
                .ToString()
        };

        WorkItemTrackingHttpClient client = Connection.GetClient<WorkItemTrackingHttpClient>();

        WorkItemQueryResult result = await client.QueryByWiqlAsync(query);

        return result.WorkItems.Any()
            ? await client.GetWorkItemsAsync(result.WorkItems.Select(item => item.Id))
            : new List<WorkItem>();
    }

    /// <summary>
    ///     Updates a work item via JSON Patch operations.
    ///     
    ///     <code>
    ///         List&lt;JsonPatchOperation&gt; operations = new()
    ///         {
    ///             new JsonPatchOperation()
    ///             {
    ///                 Operation = Operation.Replace,
    ///                 Path = "/fields/Custom.Category",
    ///                 Value = "AUTOMATED"
    ///             }
    ///         };
    ///     </code>
    ///     
    ///     Work items can be retrieved via the GetWorkItem() method.
    /// </summary>
    public async Task<WorkItem> UpdateWorkItem(WorkItem work, List<JsonPatchOperation> operations, bool simulation = false)
    {
        JsonPatchDocument document = [.. operations];

        return await Connection.GetClient<WorkItemTrackingHttpClient>().UpdateWorkItemAsync(document, Convert.ToInt32(work.Id), validateOnly: simulation);
    }

    /// <summary>
    ///     Returns the work item types defined by a project.
    ///     By default, it only returns work item types which are not disabled.
    ///     The project is defined in the Azure DevOps integration constructor.
    /// </summary>
    public async Task<List<WorkItemType>> GetWorkItemTypes(bool onlyEnabledTypes = true)
    {
        List<WorkItemType> types = await Connection.GetClient<WorkItemTrackingHttpClient>().GetWorkItemTypesAsync(Project);

        return onlyEnabledTypes ? types.Where(type => type.IsDisabled is false).ToList() : types;
    }

    /// <summary>
    ///     Returns the fields that a work item type defined by a project supports.
    ///     By default, it only returns mandatory fields.
    ///     The project is defined in the Azure DevOps integration constructor.
    ///     <br/>
    ///     The value of "type" can be either a work item type name (e.g. "Bug") or a work item type reference (e.g. "Microsoft.VSTS.WorkItemTypes.Bug").
    /// </summary>
    public async Task<List<WorkItemTypeFieldWithReferences>> GetWorkItemTypeFields(string type, bool onlyRequiredFields = true)
    {
        List<WorkItemTypeFieldWithReferences> fields = await Connection.GetClient<WorkItemTrackingHttpClient>().GetWorkItemTypeFieldsWithReferencesAsync(Project, type);

        return onlyRequiredFields ? fields.Where(field => field.AlwaysRequired is true).ToList() : fields;
    }
}
