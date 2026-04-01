namespace Program.Models;

public static class SectionMapper
{
    private static Dictionary<string, string> SectionNames { get; set; } = new();
    private static readonly HashSet<string> MissingSlugs = new();
    
    private const string SourcePath = "source.txt";
    private const string EndPath = "end.txt";
    private const string MissingPath = "missing_slugs.txt";

    public static void Initialize()
    {
        var sources = File.ReadAllLines(SourcePath);
        var ends = File.ReadAllLines(EndPath);

        SectionNames = sources
            .Zip(ends, (slug, name) => (slug, name))
            .Where(pair => !string.IsNullOrWhiteSpace(pair.slug))
            .ToDictionary(pair => pair.slug, pair => pair.name);
    }

    public static string GetReadableName(string slug)
    {
        var value = SectionNames.GetValueOrDefault(slug);
        if (value == null)
        {
            MissingSlugs.Add(slug);
            return slug;
        }
        return value;
    }

    public static void SaveMissingSections()
    {
        if (MissingSlugs.Count != 0)
        {
            File.WriteAllLines(MissingPath, MissingSlugs.Order());
        }
    }
}