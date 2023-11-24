namespace DRAMA.Helpers.BDD;

public static class TokenHelpers
{
    private const char EnvironmentVariableTokenStartCharacter = '[';
    private const char EnvironmentVariableTokenEndCharacter = ']';
    private const char TestRunContextTokenStartCharacter = '<';
    private const char TestRunContextTokenEndCharacter = '>';
    private const char FeatureContextTokenStartCharacter = '{';
    private const char FeatureContextTokenEndCharacter = '}';
    private const char ScenarioContextTokenStartCharacter = '(';
    private const char ScenarioContextTokenEndCharacter = ')';

    private const char TokenSymbolSeparatorCharacter = '¦';

    private static readonly string EnvironmentVariableTokenStart = EnvironmentVariableTokenStartCharacter.ToString() + TokenSymbolSeparatorCharacter.ToString();
    private static readonly string EnvironmentVariableTokenEnd = TokenSymbolSeparatorCharacter.ToString() + EnvironmentVariableTokenEndCharacter.ToString();
    private static readonly string TestRunContextTokenStart = TestRunContextTokenStartCharacter.ToString() + TokenSymbolSeparatorCharacter.ToString();
    private static readonly string TestRunContextTokenEnd = TokenSymbolSeparatorCharacter.ToString() + TestRunContextTokenEndCharacter.ToString();
    private static readonly string FeatureContextTokenStart = FeatureContextTokenStartCharacter.ToString() + TokenSymbolSeparatorCharacter.ToString();
    private static readonly string FeatureContextTokenEnd = TokenSymbolSeparatorCharacter.ToString() + FeatureContextTokenEndCharacter.ToString();
    private static readonly string ScenarioContextTokenStart = ScenarioContextTokenStartCharacter.ToString() + TokenSymbolSeparatorCharacter.ToString();
    private static readonly string ScenarioContextTokenEnd = TokenSymbolSeparatorCharacter.ToString() + ScenarioContextTokenEndCharacter.ToString();

    /// <summary>
    ///     Returns a value from an environment variable, the test run context, a feature context, or a scenario context, identified by the tokenised key.
    ///     <br />
    ///     <br />
    ///     <code>
    ///         [¦key¦] represents a value retrieved by key "key" from an environment variable
    ///         &lt;¦key¦&gt; represents a value retrieved by key "key" from the test run context
    ///         {¦key¦} represents a value retrieved by key "key" from a feature context
    ///         (¦key¦) represents a value retrieved by key "key" from a scenario context
    ///     </code>
    ///     <br />
    ///     The token symbol separator character "¦" is called a "broken bar" and has Unicode code <see href="https://www.compart.com/en/unicode/U+00A6">U+00A6</see>.
    /// </summary>
    public static string Detokenise(string token, FeatureContext? featureContext = null, ScenarioContext? scenarioContext = null)
    {
        if (token.StartsWith(EnvironmentVariableTokenStart) && token.EndsWith(EnvironmentVariableTokenEnd))
        {
            string input = token.TrimStart(EnvironmentVariableTokenStart.ToArray()).TrimEnd(EnvironmentVariableTokenEnd.ToArray());

            return Environment.GetEnvironmentVariable(input).HasTextContent() ? Environment.GetEnvironmentVariable(input)! : string.Empty;
        }

        else if (token.StartsWith(TestRunContextTokenStart) && token.EndsWith(TestRunContextTokenEnd))
        {
            string input = token.TrimStart(TestRunContextTokenStart.ToArray()).TrimEnd(TestRunContextTokenEnd.ToArray());

            return Contexts.TestRun.PropertyBag?.ContainsKey(input) ?? false ? Contexts.TestRun.PropertyBag.Get<dynamic>(input).ToString() ?? string.Empty : string.Empty;
        }

        else if (token.StartsWith(FeatureContextTokenStart) && token.EndsWith(FeatureContextTokenEnd))
        {
            if (featureContext is null) throw new NullReferenceException("Feature Context Is NULL");

            string input = token.TrimStart(FeatureContextTokenStart.ToArray()).TrimEnd(FeatureContextTokenEnd.ToArray());

            return featureContext.ContainsKey(input) ? featureContext.Get<dynamic>(input).ToString() : string.Empty;
        }

        else if (token.StartsWith(ScenarioContextTokenStart) && token.EndsWith(ScenarioContextTokenEnd))
        {
            if (scenarioContext is null) throw new NullReferenceException("Scenario Context Is NULL");

            string input = token.TrimStart(ScenarioContextTokenStart.ToArray()).TrimEnd(ScenarioContextTokenEnd.ToArray());

            return scenarioContext.ContainsKey(input) ? scenarioContext.Get<dynamic>(input).ToString() : string.Empty;
        }

        else return token;
    }
}
