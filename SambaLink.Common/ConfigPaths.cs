namespace SambaLink.Common;
public static class ConfigPaths
{
    private const string ConfigPath = @"{0}\SambaLink\conf.json";

    public static string GetConfigPath()
    {
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        return string.Format(ConfigPath, appData);
    }
}
