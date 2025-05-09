using SambaLink.Core.Interfaces;
using Serilog;
using System.IO.Pipes;
using System.Text;

namespace SambaLink.Service;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly ISyncService _serviece;
    private NamedPipeServerStream _pipeServer;

    public Worker(
        ILogger<Worker> logger, 
        ISyncService serviece)
    {
        _logger = logger;
        _serviece = serviece;
        _pipeServer = new NamedPipeServerStream("SambaLinkPipe", PipeDirection.In);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            _logger.LogInformation($"SambaLink started at : {DateTimeOffset.Now}");
            await _pipeServer.WaitForConnectionAsync(stoppingToken);

            byte[] buffer = new byte[1024];

            while (!stoppingToken.IsCancellationRequested)
            {
                int bytesRead = await _pipeServer.ReadAsync(buffer, 0, buffer.Length, stoppingToken);
                if (bytesRead > 0)
                {
                    string command = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    await HandleCommand(command, stoppingToken);
                }
            }
            _logger.LogInformation($"SambaLink stopped at : {DateTimeOffset.Now}");
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "An error occurred while listening for commands.");
        }
        finally
        {
            _pipeServer?.Dispose();
        }
    }

    private async Task HandleCommand(string command, CancellationToken token)
    {
        if (command == "Start")
        {
            // Логика запуска сервиса
            await _serviece.RunSync();
            Log.Information("Service started.");
        }
        else if (command == "Stop")
        {
            // Логика остановки сервиса
            Log.Information("Service stopped.");
            StopAsync(token).Wait();
        }
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        // Логика для остановки работы сервиса
        Log.Information("Worker is stopping.");
        return base.StopAsync(cancellationToken);
    }
}
