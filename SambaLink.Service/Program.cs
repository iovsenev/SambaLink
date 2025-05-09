using SambaLink.Core.Interfaces;
using SambaLink.Service;
using SambaLink.Sync;
using Serilog;

Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information() // Минимальный уровень логирования
            .WriteTo.Console() // Логирование в консоль
            .WriteTo.File("Logs/service-log.txt", rollingInterval: RollingInterval.Day) // Логирование в файл
            .CreateLogger();
try
{

    IHost host = Host.CreateDefaultBuilder(args)
        .UseWindowsService() // Это делает из приложения именно Windows-сервис
        .UseSerilog()
        .ConfigureServices(services =>
        {
            services.AddHostedService<Worker>();
            services.AddTransient<ISyncService, SyncService>();
        })
        .Build();

    host.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "SambaLink service terminated unexpectedly!");
}
finally
{
    Log.CloseAndFlush();
}