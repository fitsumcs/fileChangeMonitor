using dotenv.net;
using DiffMatchPatch;
using System.Timers; // Explicitly using System.Timers

class Program
{
    private static string? targetFilePath;
    private static string? lastFileContent;
    private static System.Timers.Timer? timer; // Fully qualified name

    static void Main(string[] args)
    {
        // Load environment variables from .env file
        DotEnv.Load();

        // Read the target file path from environment variable
        targetFilePath = Environment.GetEnvironmentVariable("TARGET_FILE_PATH");

        if (string.IsNullOrEmpty(targetFilePath))
        {
            Console.WriteLine("Environment variable TARGET_FILE_PATH is not set. Exiting program.");
            return;
        }

        if (!File.Exists(targetFilePath))
        {
            Console.WriteLine("File does not exist at the specified path. Exiting program.");
            return;
        }

        lastFileContent = File.ReadAllText(targetFilePath);
        timer = new System.Timers.Timer(15000); // 15 seconds
        timer.Elapsed += CheckFileChanges;
        timer.Start();

        Console.WriteLine("Monitoring file changes. Press Enter to exit.");
        Console.ReadLine();
    }

    private static void CheckFileChanges(object sender, ElapsedEventArgs e)
    {
        string currentFileContent = File.ReadAllText(targetFilePath);

        if (currentFileContent != lastFileContent)
        {
            Console.WriteLine("File has been changed. Reporting changes:");
            PrintDifferences(lastFileContent, currentFileContent);
            lastFileContent = currentFileContent;
        }
        else
        {
            Console.WriteLine("No changes detected.");
        }
    }

    private static void PrintDifferences(string oldContent, string newContent)
    {
        var dmp = new diff_match_patch();
        var diffs = dmp.diff_main(oldContent, newContent);
        dmp.diff_cleanupSemantic(diffs);

        foreach (var diff in diffs)
        {
            switch (diff.operation)
            {
                case Operation.INSERT:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("+ " + diff.text);
                    break;
                case Operation.DELETE:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("- " + diff.text);
                    break;
                case Operation.EQUAL:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
            }
        }
        Console.ResetColor();
        Console.WriteLine();
    }
}
