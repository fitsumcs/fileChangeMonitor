using dotenv.net;

public class ConfigurationManager : IConfigurationManager
{
    private string targetFilePath;

    public ConfigurationManager()
    {
        DotEnv.Load();
        targetFilePath = Environment.GetEnvironmentVariable("TARGET_FILE_PATH");

        if (string.IsNullOrEmpty(targetFilePath))
        {
            Console.WriteLine("Environment variable TARGET_FILE_PATH is not set. Exiting program.");
            Environment.Exit(1);
        }

        if (!File.Exists(targetFilePath))
        {
            Console.WriteLine("File does not exist at the specified path. Exiting program.");
            Environment.Exit(1);
        }
    }

    public string GetTargetFilePath()
    {
        return targetFilePath;
    }
}
