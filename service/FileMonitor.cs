using System.Timers;
using DiffMatchPatch;

public class FileMonitor
{
    private string lastFileContent;
    private System.Timers.Timer timer;
    private string targetFilePath;

    public FileMonitor(string filePath)
    {
        targetFilePath = filePath;
        lastFileContent = File.ReadAllText(targetFilePath);
        timer = new System.Timers.Timer(15000); // 15 seconds
        timer.Elapsed += CheckFileChanges;
        timer.Start();
    }

    private void CheckFileChanges(object sender, ElapsedEventArgs e)
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

    private void PrintDifferences(string oldContent, string newContent)
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
