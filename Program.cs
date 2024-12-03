using CodeMechanic.DataDictionary;
using CodeMechanic.Shargs;

namespace CodeMechanic.Diagnostics;

public class Program
{
    static async Task Main(params string[] args)
    {
        var arguments = new ArgsMap(args);

        await new DataDictionaryService(arguments).Run();
    }
}
