using System.Text.Json;
using BLL;
using BLL.Contracts;

namespace DAL.JSON;

public class JsonConfigRepository : IConfigRepository
{
    private static readonly JsonSerializerOptions Options = DalHelpers.JsonSerializerOptions;

    public void Save(GameConfiguration config)
    {
        DalHelpers.CreateDirectoryIfNotExists(DalHelpers.GetConfigsPath());

        var fileName = DalHelpers.GetConfigFileName(config.Id);
        var json = JsonSerializer.Serialize(config, Options);

        DalHelpers.WriteAtomic(fileName, json);
    }

    public GameConfiguration? Load(Guid id)
    {
        DalHelpers.CreateDirectoryIfNotExists(DalHelpers.GetConfigsPath());

        var path = DalHelpers.GetConfigsPath();
        var files = Directory.GetFiles(path, $"config-{id}.json");

        var file = files.SingleOrDefault();

        if (file == null) return null;

        var gameConfig = JsonSerializer.Deserialize<GameConfiguration>(file, Options);

        return gameConfig;
    }

    public List<GameConfiguration> LoadAll()
    {
        DalHelpers.CreateDirectoryIfNotExists(DalHelpers.GetConfigsPath());

        var gameConfigs = new List<GameConfiguration>();

        var path = DalHelpers.GetConfigsPath();
        var files = Directory.GetFiles(path, "config-*.json");
        foreach (var file in files)
        {
            var fileContent = File.ReadAllText(file);
            var config = JsonSerializer.Deserialize<GameConfiguration>(fileContent, Options);
            if (config != null) gameConfigs.Add(config);
        }

        return gameConfigs;
    }

    public void Delete(Guid id)
    {
        DalHelpers.CreateDirectoryIfNotExists(DalHelpers.GetConfigsPath());

        var path = DalHelpers.GetConfigsPath();
        var files = Directory.GetFiles(path, $"config-{id}.json");

        var file = files.SingleOrDefault();

        if (file != null) File.Delete(file);
    }
}