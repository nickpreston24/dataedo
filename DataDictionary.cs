using System.Reflection;
using System.Text;
using CodeMechanic.Async;
using CodeMechanic.FileSystem;
using CodeMechanic.RegularExpressions;
using CodeMechanic.Shargs;
using CodeMechanic.Types;

namespace CodeMechanic.DataDictionary;

public class DataDictionaryService : QueuedService
{
    private readonly ArgsMap arguments;

    public DataDictionaryService(ArgsMap arguments)
    {
        this.arguments = arguments;

        if (this.arguments.HasCommand("create"))
            steps.Add(RunQueue);
    }

    private async Task<string[]> GetMarkdownFiles()
    {
        string cwd = Directory.GetCurrentDirectory();
        var md_files = new Grepper() { RootPath = cwd, FileSearchMask = "*.md" }
            .GetFileNames()
            .ToArray();
        return md_files;
    }

    private async Task RunQueue()
    {
        string cwd = Directory.GetCurrentDirectory();

        var md_files = await GetMarkdownFiles();
        var Q = new SerialQueue();
        var tasks = md_files.Select(file_path =>
            Q.Enqueue(() =>
            {
                Console.WriteLine($"reading file {file_path} ");
                string text = File.ReadAllText(file_path);

                if (text.IsEmpty())
                    return;

                var sql = text.Extract<MarkdownToSqlInfo>(DDPatterns.Query.CompiledRegex)
                    .Select(md => md.raw_query)
                    .Aggregate(
                        new StringBuilder(),
                        (builder, next) =>
                        {
                            builder.AppendLine(next);
                            return builder;
                        }
                    )
                    .ToString();

                if (sql.IsEmpty())
                    return;

                var map = new Dictionary<string, string>()
                {
                    [@"\s+-\s+"] = "_",
                    [@"\s+"] = "_",
                    [@"\("] = "",
                    [@"\)"] = "",
                    [@"</?\w+>"] = "",
                    [@","] = "",
                    [@"&"] = "and",
                };

                string save_file_name = $"{Path.GetFileNameWithoutExtension(file_path)}.sql";
                string save_file_path = save_file_name
                    .AsArray()
                    .ReplaceAll(map)
                    .FlattenText()
                    .ToLowerInvariant();

                new SaveFile(sql).To(cwd, ".sql/").As(save_file_path);
            })
        );

        await Task.WhenAll(tasks);
    }
}

public class DDPatterns : RegexEnumBase
{
    public static DDPatterns Query = new DDPatterns(
        1,
        @"Query",
        @"(\#+\s*Query)?\s*`{3}(?<raw_query>(.*\s*)+?)`{3}",
        "https://regex101.com/r/Aku8Sc/1"
    );

    protected DDPatterns(int id, string name, string pattern, string uri = "")
        : base(id, name, pattern, uri)
    {
    }
}

public class MarkdownToSqlInfo
{
    public string raw_query { get; set; } = string.Empty;
}

public static class StringExtensions
{
    public static string FlattenText(this IEnumerable<string> lines, string delimiter = " ") =>
        lines
            ?.Aggregate(
                new StringBuilder(),
                (builder, next) =>
                {
                    // builder.AppendFormat("{0}---{1}", next.Trim(), delimiter);
                    if (delimiter?.Length > 0)
                        builder.Append(next.Trim() + delimiter);
                    else
                    {
                        builder.AppendLine(next.Trim());
                    }

                    return builder;
                }
            )
            .RemoveFromEnd(1)
            .ToString() ?? string.Empty;
}

public static class Resources
{
    private static string[] resources;

    public static Assembly ThisAssembly => typeof(Resources).Assembly;

    public static void ListResourcesInAssembly(Assembly? assembly)
    {
        if (assembly is null)
        {
            Console.WriteLine("assembly is null");
            return;
        }

        resources = assembly.GetManifestResourceNames();
        Console.WriteLine("total resources found: " + resources.Length);
        if (resources.Length == 0)
            return;

        Console.WriteLine($"Resources in {assembly.FullName}");
        foreach (var resource in resources)
        {
            Console.WriteLine(resource);
        }
    }

    public static class Embedded
    {
        private static bool debug = true;

        public static string GetFile(string filename, bool debug = false)
        {
            if (filename.IsEmpty())
                throw new ArgumentNullException(nameof(filename));

            string filepath = resources.FirstOrDefault(name => name.ToLower().Contains(filename));
            if (debug)
                Console.WriteLine("file path: \n" + filepath);

            using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(filepath);
            using var streamReader = new StreamReader(stream, Encoding.UTF8);

            return streamReader.ReadToEnd();
        }

        // public static string SqlFile
        // {
        //     get
        //     {
        //         var info = Assembly.GetExecutingAssembly().GetName();
        //         var ass_name = info.Name;
        //         if (debug) Console.WriteLine("ass name:>> " + ass_name);
        //         string filename = $"{sproc_name}.sql";
        //         string filepath = resources.FirstOrDefault(name =>
        //             name.ToLower().Contains(filename)
        //         );
        //         // if (debug)
        //         Console.WriteLine("file path: \n" + filepath);
        //         using var stream = Assembly
        //             .GetExecutingAssembly()
        //             .GetManifestResourceStream(filepath)!;
        //         using var streamReader = new StreamReader(stream, Encoding.UTF8);
        //         return streamReader.ReadToEnd();
        //     }
        // }
    }
}