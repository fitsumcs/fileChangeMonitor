using System.Timers;
using DiffMatchPatch;

public class FileMonitor : IFileMonitor
{
    private string lastFileContent;
    private System.Timers.Timer timer;
    private string targetFilePath;

    /// <summary>
    /// Constructor to initialize FileMonitor with the configuration manager.
    /// </summary>
    /// <param name="configManager">The configuration manager to retrieve file path.</param>
    public FileMonitor(IConfigurationManager configManager)
    {
        targetFilePath =  configManager.GetTargetFilePath();
        // Read initial file content// Read initial file content
        lastFileContent = File.ReadAllText(targetFilePath);
        timer = new System.Timers.Timer(15000); // 15 seconds
        // Attach CheckFileChanges method to timer's Elapsed event
        timer.Elapsed += CheckFileChanges;
    }

    /// <summary>
    /// Starts monitoring the target file for changes.
    /// </summary>
    public void StartMonitoring()
    {
        timer.Start();
    }

    /// <summary>
    /// Event handler for checking file changes when timer elapses.
    /// </summary>
    /// <param name="sender">The event sender (Timer).</param>
    /// <param name="e">Elapsed event arguments.</param>
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

    /// <summary>
    /// Prints the differences between old and new file content using a diff_match_patch library.
    /// </summary>
    /// <param name="oldContent">The old content of the file.</param>
    /// <param name="newContent">The new content of the file.</param>
    private void PrintDifferences(string oldContent, string newContent)
    {
        // Create an instance of diff_match_patch
        var dmp = new diff_match_patch();
        // Compute the differences between old and new content
        var diffs = dmp.diff_main(oldContent, newContent);
        // Clean up the differences for better readability
        dmp.diff_cleanupSemantic(diffs);

        foreach (var diff in diffs)
        {
            switch (diff.operation)
            {
                case Operation.INSERT:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("+ " + diff.text); // Print inserted text in green
                    break;
                case Operation.DELETE:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("- " + diff.text); // Print deleted text in red
                    break;
                case Operation.EQUAL:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break; // Equal text remains gray (no change)
            }
        }
        Console.ResetColor(); // Reset console color to default
        Console.WriteLine();
    }
}

