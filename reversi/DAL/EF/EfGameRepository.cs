using BLL;
using BLL.Contracts;
using DAL.DTO;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF;

public class EfGameRepository : IGameRepository
{
    public void Save(GameState gameState)
    {
        using var db = new AppDbContext();

        var gameStateDto = db.GameStates
            .FirstOrDefault(c => c.Id == gameState.Id);

        if (gameStateDto == null)
        {
            gameStateDto = GameStateMapper.ToDto(gameState);
            db.GameStates.Add(gameStateDto);
        }
        else
        {
            GameStateMapper.UpdateDto(gameStateDto, gameState);
        }

        db.SaveChanges();
    }

    public GameState? Load(Guid id)
    {
        using var db = new AppDbContext();
        var gameStateDto = db.GameStates.FirstOrDefault(c => c.Id == id)
                        ?? throw new KeyNotFoundException($"Game '{id}' not found.");

        var config = GameStateMapper.ToDomain(gameStateDto);
        return config;
    }

    public List<GameState> LoadAll()
    {
        using var db = new AppDbContext();
        var gameStateDtos = db.GameStates
            .Include(x => x.Config)
            .ThenInclude(x => x!.Players).ToList();
        return gameStateDtos.Select(x => GameStateMapper.ToDomain(x)).ToList();
    }

    public void Delete(Guid id)
    {
        using var db = new AppDbContext();
        db.GameStates.Where(c => c.Id == id).ExecuteDelete();
    }
}