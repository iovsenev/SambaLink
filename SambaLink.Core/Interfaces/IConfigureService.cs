using SambaLink.Shared.Models;

namespace SambaLink.Core.Interfaces;
public interface IConfigureService
{
    Task<SyncConfig> LoadConfigAsync(CancellationToken token);
    Task SaveConfigAsync(SyncConfig config, CancellationToken token);
}
