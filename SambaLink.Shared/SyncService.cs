using Microsoft.Extensions.Logging;

namespace SambaLink.Shared;

public class SyncService
{
    private readonly ILogger<SyncService> _logger;

    public SyncService(ILogger<SyncService> logger)
    {
        _logger = logger;
    }


}
