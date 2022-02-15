namespace DRAMA.Helpers.TargetFramework;

public static class StringHelpers
{
    public static string GenerateWhiteSpaces(int amount)
    {
        StringBuilder spaces = new();

        for (int i = 0; i < amount; i++) spaces.Append(' ');

        return spaces.ToString();
    }

    // TODO: Add Unit Test And Summary
}
