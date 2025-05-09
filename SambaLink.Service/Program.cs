using SambaLink.Core.Interfaces;
using SambaLink.Service;
using SambaLink.Sync;
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