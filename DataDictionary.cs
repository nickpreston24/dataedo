using System.Reflection;
using System.Text;
using CodeMechanic.Async;
using CodeMechanic.FileSystem;
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
        var md_files = new Grepper()
        {
            RootPath = cwd,
            FileSearchMask = "*.md"
        }.GetFileNames().ToArray();
        return md_files;
    }

    private async Task RunQueue()
    {
        string cwd = Directory.GetCurrentDirectory();

        var md_files = await GetMarkdownFiles();
        var Q = new SerialQueue();
        var tasks = md_files
            .Select(file_path => Q.Enqueue(() =>
            {
                Console.WriteLine($"reading file {file_path} ");
                string text = File.ReadAllText(file_path);
                string save_file_name = Path.GetFileName(file_path) + "sql";
                // ..
                string sql = @"select * from foo";

                new SaveFile(sql).To(cwd, "sql").As(save_file_name);
            }));

        await Task.WhenAll(tasks);
    }
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