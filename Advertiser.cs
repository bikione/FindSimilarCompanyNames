using System.Text.RegularExpressions;

namespace ConsoleApp1;

public class Advertiser
{
    public string OriginalName { get; }
    public string NormalizedName { get; }
    public List<string> SplitedName { get; }

    public Advertiser(string name)
    {
        OriginalName = name;
        NormalizedName = NormalizeName(name);
        SplitedName = SplitName(name);

    }
    // NOTE: Normalize by removing non-alphanumeric characters and lowercasing
    private static string NormalizeName(string name)
    {
        return Regex
            .Replace(name, @"[^a-zA-Z0-9]", "")
            .ToLower();
    }

    private static List<string> SplitName(string name)
    {
        return Regex
            .Split(name.ToLower(), @"[^a-zA-Z0-9]+", RegexOptions.None)
            .Where(s => !string.IsNullOrEmpty(s))
            .ToList();
    }
}