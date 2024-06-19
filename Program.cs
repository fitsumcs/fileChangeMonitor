class Program
{
    static void Main(string[] args)
    {
        ConfigurationManager configManager = new ConfigurationManager();
        string targetFilePath = configManager.GetTargetFilePath();

        FileMonitor fileMonitor = new FileMonitor(targetFilePath);

        Console.WriteLine("Monitoring file changes. Press Enter to exit.");
        Console.ReadLine();
    }
}
