using System.Text.Json;
using System.Text.Json.Serialization;

namespace DAL;

public static class DalHelpers
{
    private static string GetRootPath()
    {
        var path = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            ".icd0008",
            "reversi-othello");
        return path;
    }

    public static string GetConfigsPath() => Path.Combine(GetRootPath(), "configs");
    public static string GetGamesPath() => Path.Combine(GetRootPath(), "games");

    public static string GetConfigFileName(Guid id) => Path.Combine(GetConfigsPath(), $"config-{id}.json");
    public static string GetGameFileName(Guid id) => Path.Combine(GetGamesPath(), $"game-{id}.json");

    public static void WriteAtomic(string path, string content)
    {
        var temp = path + ".tmp";
        File.WriteAllText(temp, content);
        File.Move(temp, path);
    }

    public static readonly JsonSerializerOptions Options = new JsonSerializerOptions
    {
        WriteIndented = true, // pretty print files a human will open
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // "boardWidth" instead of "BoardWidth"
        PropertyNameCaseInsensitive = true, // accept either when reading
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, // skip properties that are null
        AllowTrailingCommas = true, // tolerate hand-edited files
        Converters = { new JsonStringEnumConverter() }, // enums as "X", not 1
    };

    public static void CreateDirectoryIfNotExists(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }
}