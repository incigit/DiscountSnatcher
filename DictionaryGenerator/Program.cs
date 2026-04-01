using System.Text;

var sourcePath = "source.txt";
var endPath = "end.txt";

var sources = File.ReadAllLines(sourcePath);
var ends = File.ReadAllLines(endPath);

var sb = new StringBuilder();

sb.AppendLine("// AUTO-GENERATED CODE //" + Environment.NewLine);
sb.AppendLine("public static class SectionMapper");
sb.AppendLine("{");
sb.AppendLine("private static readonly Dictionary<string, string> SectionNames = new()");
sb.AppendLine("{");

for (int i = 0; i < sources.Length; i++)
{
    if (string.IsNullOrWhiteSpace(sources[i])) continue;
    sb.AppendLine($"    {{\"{sources[i]}\", \"{ends[i]}\"}},");
}

sb.AppendLine("};");
sb.AppendLine("}");

// Output to console or file
File.WriteAllText("SectionMapper.cs", sb.ToString());
