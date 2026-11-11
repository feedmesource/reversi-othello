using BLL;
using BLL.Contracts;
using DAL.DTO;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF;

public class EfConfigRepository : IConfigRepository
{
    public void Save(GameConfiguration config)
    {
        using var db = new AppDbContext();

        var configDto = db.Configurations
            .FirstOrDefault(c => c.Id == config.Id);

        if (configDto == null)
        {
            configDto = GameConfigMapper.ToDto(config);
            db.Configurations.Add(configDto);
        }
        else
        {
            GameConfigMapper.UpdateDto(configDto, config);
        }

        db.SaveChanges();
    }

    public GameConfiguration Load(Guid id)
    {
        using var db = new AppDbContext();
        var configDto = db.Configurations.FirstOrDefault(c => c.Id == id)
                        ?? throw new KeyNotFoundException($"Configuration '{id}' not found.");

        var config = GameConfigMapper.ToDomain(configDto);
        return config;
    }

    public List<GameConfiguration> LoadAll()
    {
        using var db = new AppDbContext();
        var configDtos = db.Configurations.ToList();
        return configDtos.Select(x => GameConfigMapper.ToDomain(x)).ToList();
    }

    public void Delete(Guid id)
    {
        using var db = new AppDbContext();
        db.Configurations.Where(c => c.Id == id).ExecuteDelete();
    }
}