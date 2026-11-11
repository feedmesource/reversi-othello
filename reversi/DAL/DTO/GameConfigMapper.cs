using BLL;

namespace DAL.DTO;

public static class GameConfigMapper
{
    public static GameConfigDto ToDto(GameConfiguration config) => new()
    {
        Id = config.Id,
        BoardWidth = config.BoardWidth,
        BoardHeight = config.BoardHeight,
        Players = config.Players.Select(x => PlayerMapper.ToDto(x)).ToArray(),
        IsOthello = config.IsOthello,
        HaveWalls = config.HaveWalls
    };

    public static GameConfiguration ToDomain(GameConfigDto dto) => new GameConfiguration
    (
        dto.Id,
        dto.BoardWidth,
        dto.BoardHeight,
        dto.Players.Select(x => PlayerMapper.ToDomain(x)).ToArray(),
        dto.IsOthello,
        dto.HaveWalls
    );
}