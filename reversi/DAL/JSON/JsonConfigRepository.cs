using System.Text.Json;
using System.Text.Json.Serialization;
using BLL;
using BLL.Contracts;

namespace DAL.JSON;

public class JsonConfigRepository : IConfigRepository
{
    private static readonly JsonSerializerOptions Options = new JsonSerializerOptions
    {
        WriteIndented = true, // pretty print files a human will open
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // "boardWidth" instead of "BoardWidth"
        PropertyNameCaseInsensitive = true, // accept either when reading
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, // skip properties that are null
        AllowTrailingCommas = true, // tolerate hand-edited files
        Converters = { new JsonStringEnumConverter() }, // enums as "X", not 1
    };

    public void Save(GameConfiguration config)
    {
        var dir = FileSystemHelpers.GetConfigsPath();
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        var fileName = FileSystemHelpers.GetConfigFileName(config.Id);
        var json = JsonSerializer.Serialize(config, Options);

        FileSystemHelpers.WriteAtomic(fileName, json);
    }

    public GameConfiguration Load(string id)
    {
        throw new NotImplementedException();
    }

    public List<GameConfiguration> LoadAll()
    {
        throw new NotImplementedException();
    }

    public void Delete(string id)
    {
        throw new NotImplementedException();
    }
}