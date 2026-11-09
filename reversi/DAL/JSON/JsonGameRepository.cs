using BLL;
using BLL.Contracts;

namespace DAL.JSON;

public class JsonGameRepository : IGameRepository
{
    public void Save(GameConfiguration config)
    {
        DalHelpers.CreateDirectoryIfNotExists(DalHelpers.GetGamesPath());

        throw new NotImplementedException();
    }

    public GameConfiguration? Load(Guid id)
    {
        DalHelpers.CreateDirectoryIfNotExists(DalHelpers.GetGamesPath());

        throw new NotImplementedException();
    }

    public List<GameConfiguration> LoadAll()
    {
        DalHelpers.CreateDirectoryIfNotExists(DalHelpers.GetGamesPath());

        throw new NotImplementedException();
    }

    public void Delete(Guid id)
    {
        DalHelpers.CreateDirectoryIfNotExists(DalHelpers.GetGamesPath());

        throw new NotImplementedException();
    }
}