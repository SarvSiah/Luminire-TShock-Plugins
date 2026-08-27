using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Luminire.Core;

/// <summary>
/// Base class for JSON configs with automatic load/save and thread-safe defaults.
/// </summary>
/// <typeparam name="T">Concrete config type</typeparam>
public abstract class ConfigBase<T> where T : ConfigBase<T>, new()
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
    };

    /// <summary>
    /// Path where config is stored. Override in derived class if needed.
    /// </summary>
    public virtual string ConfigPath => Path.Combine(TShockAPI.TShock.SavePath, $"{typeof(T).Name}.json");

    /// <summary>
    /// Load or create config.
    /// </summary>
    public static T Load(string? path = null)
    {
        var instance = new T();
        var filePath = path ?? instance.ConfigPath;

        try
        {
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                var loaded = JsonSerializer.Deserialize<T>(json, JsonOptions);
                if (loaded != null)
                {
                    loaded.ConfigPath = filePath;
                    return loaded;
                }
            }
        }
        catch (Exception ex)
        {
            TShockAPI.TShock.Log.ConsoleError($"[Luminire.Core] Failed to load config {filePath}: {ex.Message}");
        }

        // Create default
        instance.Save(filePath);
        return instance;
    }

    public void Save(string? path = null)
    {
        var filePath = path ?? ConfigPath;
        try
        {
            var dir = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var json = JsonSerializer.Serialize(this, GetType(), JsonOptions);
            File.WriteAllText(filePath, json);
        }
        catch (Exception ex)
        {
            TShockAPI.TShock.Log.ConsoleError($"[Luminire.Core] Failed to save config {filePath}: {ex.Message}");
        }
    }
}
