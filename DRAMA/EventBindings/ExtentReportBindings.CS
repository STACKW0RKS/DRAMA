// TODO: Remove This After Migrating Away From Extent Reports

using AventStack.ExtentReports;

namespace DRAMA.EventBindings;

[Binding]
public class ExtentReportBindings
{
    private static ExtentReports Extent = new();
    private static List<ExtentTest> Features = new();
    private ExtentTest CurrentScenario;
}
