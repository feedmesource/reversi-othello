using System.Text.Json;
using BLL;
using BLL.Contracts;

namespace DAL.JSON;

public class JsonConfigRepository : IConfigRepository
{
    public void Save(GameConfiguration config)
    {
        var fileName = FileSystemHelpers.GetConfigFileName(config.Id);
        var json = JsonSerializer.Serialize(config);
        
        Console.WriteLine(json);
        
        // FileSystemHelpers.WriteAtomic(fileName, json);
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