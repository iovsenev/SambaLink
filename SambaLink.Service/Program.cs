using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SambaLink.Service;
using Serilog;

Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information() // ����������� ������� �����������
            .WriteTo.Console() // ����������� � �������
            .WriteTo.File("Logs/service-log.txt", rollingInterval: RollingInterval.Day) // ����������� � ����
            .CreateLogger();
try
{

    IHost host = Host.CreateDefaultBuilder(args)
        .UseWindowsService() // ��� ������ �� ���������� ������ Windows-������
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