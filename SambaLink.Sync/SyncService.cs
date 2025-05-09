using Microsoft.Extensions.Logging;
using SambaLink.Core.Interfaces;

namespace SambaLink.Sync;

public class SyncService : ISyncService
{
    private readonly ILogger<SyncService> _logger;

    public SyncService(ILogger<SyncService> logger)
    {
        _logger = logger;
    }

    public async Task RunSync()
    {
        _logger.LogInformation("start sync from sync project");
    }
}
