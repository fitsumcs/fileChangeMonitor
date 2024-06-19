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
            Environment.Exit(1);  // Exit if environment variable is missing
        }

        if (!File.Exists(targetFilePath))
        {
            Console.WriteLine("File does not exist at the specified path. Exiting program.");
            Environment.Exit(1); // Exit if file does not exist
        }
    }

    /// <summary>
    /// Retrieves the target file path from configuration.
    /// </summary>
    /// <returns>The target file path.</returns>
    public string GetTargetFilePath()
    {
        return targetFilePath;
    }
}
