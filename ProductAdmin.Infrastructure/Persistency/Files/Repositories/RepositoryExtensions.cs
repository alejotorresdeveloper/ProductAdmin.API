namespace ProductAdmin.Infrastructure.Persistency.Files.Repositories;

using Microsoft.Extensions.Configuration;
using System.Text.Json;

internal static class RepositoryExtensions
{
    #region Private methods

    public static string GetFilePath(string folder, string fileName)
    {
        string filePath = Path.Combine(folder, fileName);

        return filePath;
    }

    public static List<T> GetData<T>(string filePath) where T : class
    {
        if (!File.Exists(filePath))
            return new();

        string jsonData = File.ReadAllText(filePath);

        ArgumentNullException.ThrowIfNull(jsonData, nameof(jsonData));

        JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true, IncludeFields = true };

        return JsonSerializer.Deserialize<List<T>>(jsonData, options) ?? new();
    }

    public static int LastId<T>(string filePath) where T : class
    {
        List<T> data = GetData<T>(filePath);

        if (data.Count == 0)
            return 1;

        return data.Count + 1;
    }

    public static async ValueTask SaveData<T>(List<T> savedData, string filePath) where T : class
    {
        string jsonData = JsonSerializer.Serialize(savedData, new JsonSerializerOptions { WriteIndented = true });

        await File.WriteAllTextAsync(filePath, jsonData);
    }

    public static string GetFolder()
    {
        string folder = Path.Combine(Directory.GetCurrentDirectory(), "Files", "Data");
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        return folder;
    }

    public static string GetConfigurationValue(IConfiguration configuration, string key)
    {
        ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));

        if (configuration.GetSection(key).Exists())
        {
            return configuration.GetSection(key).Value;
        }

        throw new ArgumentException($"Configuration key '{key}' not found.");
    }

    #endregion Private methods
}