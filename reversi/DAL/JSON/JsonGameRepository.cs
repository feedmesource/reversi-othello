using System.Text.Json;
using BLL;
using BLL.Contracts;
using DAL.JSON.DTO;

namespace DAL.JSON;

public class JsonGameRepository : IGameRepository
{
    private static readonly JsonSerializerOptions Options = DalHelpers.Options;

    public void Save(GameState gameState)
    {
        DalHelpers.CreateDirectoryIfNotExists(DalHelpers.GetGamesPath());

        var fileName = DalHelpers.GetGameFileName(gameState.Id);

        var dto = GameStateMapper.ToDto(gameState);
        var json = JsonSerializer.Serialize(dto, Options);

        DalHelpers.WriteAtomic(fileName, json);
    }

    public GameState? Load(Guid id)
    {
        DalHelpers.CreateDirectoryIfNotExists(DalHelpers.GetGamesPath());

        var path = DalHelpers.GetGamesPath();
        var files = Directory.GetFiles(path, $"game-{id}.json");

        var file = files.SingleOrDefault();

        if (file == null) return null;

        var dto = JsonSerializer.Deserialize<GameStateDto>(file, Options)
                  ?? throw new JsonException("Configuration JSON was null.");

        var state = GameStateMapper.ToDomain(dto);

        return state;
    }

    public List<GameState> LoadAll()
    {
        DalHelpers.CreateDirectoryIfNotExists(DalHelpers.GetGamesPath());

        var games = new List<GameState>();

        var path = DalHelpers.GetGamesPath();
        var files = Directory.GetFiles(path, "game-*.json");
        foreach (var file in files)
        {
            var fileContent = File.ReadAllText(file);
            var dto = JsonSerializer.Deserialize<GameStateDto>(fileContent, Options)
                      ?? throw new JsonException("Configuration JSON was null.");
            var state = GameStateMapper.ToDomain(dto);
            games.Add(state);
        }

        return games;
    }

    public void Delete(Guid id)
    {
        DalHelpers.CreateDirectoryIfNotExists(DalHelpers.GetGamesPath());

        var path = DalHelpers.GetGamesPath();
        var files = Directory.GetFiles(path, $"game-{id}.json");

        var file = files.SingleOrDefault();

        if (file != null) File.Delete(file);
    }
}