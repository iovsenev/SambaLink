using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SambaLink.Service;
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