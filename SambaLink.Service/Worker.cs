namespace SambaLink.Service;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation($"SambaLink started at : {DateTimeOffset.Now}");

        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("SumbaLink Service is running at: {time}", DateTimeOffset.Now);
            await Task.Delay(1000, stoppingToken);
        }
        _logger.LogInformation($"SambaLink stopped at : {DateTimeOffset.Now}");
    }
}
