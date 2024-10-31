using ConsoleApp1;

public class Program
{
    /*
     * This program compares company names from a text file to find names that look similar or might be duplicates.
     * It works in three main steps:
     *
     * 1. Preparation Step:
     *    - In this step, we get the data ready for comparison.
     *    - We "normalize" each company name by removing any characters that are not letters or numbers, and we change all letters to lowercase.
     *    - We also split each name into separate words based on non-alphanumeric characters. This gives us a list of words
     *      from each name, making it easier to compare them later.
     *
     * 2. Exact Match Comparison:
     *    - In this step, we go through the normalized company names to find exact matches.
     *    - If two names are exactly the same after removing non-alphanumeric characters, we consider them the same company and mark them as duplicates.
     *
     * 3. Partial Match Comparison:
     *    - This step performs a more flexible comparison by looking at the lists of words we created in the Preparation Step.
     *    - This helps us find company names that are similar but might have words in a different order or small differences.
     *      For example, "University of Chicago" and "Chicago University" would be matched in this step.
     *    - We say two names are similar if most of the words in the shorter list can be found in the longer list.
     *      This lets us catch names that are mostly the same, even if the words are not in the exact same order.
     *
     */
    public static void Main()
    {
        var advertisers = File.ReadAllLines("advertisers.txt")
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Select(line => new Advertiser(line.Trim()))
            .ToList();
        
        var duplicates = FindSimilarNames(/*advertisers.Take(5000).ToList()*/ advertisers);

        foreach (var (name1, name2) in duplicates)
        {
            Console.WriteLine($"- '{name1}' and '{name2}'");
        }
    }
    
    private static List<(string, string)> FindSimilarNames(List<Advertiser> advertisers)
    {
        var duplicates = new List<(string, string)>();
        for (var i = 0; i < advertisers.Count; i++)
        {
            for (var j = i + 1; j < advertisers.Count; j++)
            {
                // Mark as the same company if either the normalized names or the lists of split strings are similar
                if (AreNormalizedNamesSame(advertisers[i].NormalizedName, advertisers[j].NormalizedName) 
                    || AreSplitedStringsSimilar(advertisers[i].SplitedName, advertisers[j].SplitedName))
                {
                    duplicates.Add((advertisers[i].OriginalName, advertisers[j].OriginalName));
                }
            }
        }
        return duplicates;
    }

    private static bool AreNormalizedNamesSame(string normalizedName1, string normalizedName2)
    {
        return normalizedName1 == normalizedName2;
    }

    private static bool AreSplitedStringsSimilar(List<string> splitedNamesList1, List<string> splitedNamesList2)
    {
        var (longerList, shorterList) = splitedNamesList1.Count >= splitedNamesList2.Count ? (splitedNamesList1, splitedNamesList2) : (splitedNamesList2, splitedNamesList1);
        return AreListsSimilar(longerList, shorterList);
    }
    private static bool AreListsSimilar(List<string> longerList, List<string> shorterList)
    {
        // 0.85 represents the threshold ratio for matching words
        // 'threshHold' defines the minimum number of words that must match for the lists to be considered similar
        var threshHold = (int) Math.Round(longerList.Count * 0.85);

        var counter = shorterList
            .Count(word => 
                longerList.Contains(word));

        return counter >= threshHold && counter == shorterList.Count;
    }
}
