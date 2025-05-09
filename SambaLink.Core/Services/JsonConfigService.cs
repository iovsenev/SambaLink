using SambaLink.Common;
using SambaLink.Core.Interfaces;
using SambaLink.Shared.Models;
using System.Text.Json;

namespace SambaLink.Core.Services;
public class JsonConfigService : IConfigureService
{
    private static readonly string _confPath = ConfigPaths.GetConfigPath();
    private static readonly string _confDirectory = Path.GetDirectoryName(_confPath);
    public async  Task<SyncConfig> LoadConfigAsync(CancellationToken token)
    {
        if (!File.Exists(_confPath))
            throw new FileNotFoundException("not config file");

         var str = await File.ReadAllTextAsync(_confPath, token);

        var conf = JsonSerializer.Deserialize<SyncConfig>(str);

        return conf;
    }

    public async Task SaveConfigAsync(SyncConfig config, CancellationToken token)
    {
        if (!Directory.Exists(_confDirectory))
            Directory.CreateDirectory(_confDirectory);

        File.Create(_confPath);
        var str = JsonSerializer.Serialize(config);

        await File.WriteAllTextAsync(_confPath, str, token);
    }
}
