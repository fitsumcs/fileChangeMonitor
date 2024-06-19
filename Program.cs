using Microsoft.Extensions.DependencyInjection;
using System;

public class Program
{
    static void Main(string[] args)
    {
        // Setup DI container
        var serviceProvider = new ServiceCollection()
            .AddTransient<IConfigurationManager, ConfigurationManager>()
            .AddTransient<IFileMonitor, FileMonitor>()
            .BuildServiceProvider();

        // Resolve dependencies
        var configManager = serviceProvider.GetRequiredService<IConfigurationManager>();
        var fileMonitor = serviceProvider.GetRequiredService<IFileMonitor>();

        // Start monitoring
        fileMonitor.StartMonitoring();

        Console.WriteLine("Monitoring file changes. Press Enter to exit.");
        Console.ReadLine();
    }
}
