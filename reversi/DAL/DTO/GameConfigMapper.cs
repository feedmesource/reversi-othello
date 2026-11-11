using BLL;
using BLL.Contracts;
using BLL.PlayerTypes;

namespace DAL.DTO;

public static class GameConfigMapper
{
    public static GameConfigDto ToDto(GameConfiguration config) => new()
    {
        Id = config.Id,
        BoardWidth = config.BoardWidth,
        BoardHeight = config.BoardHeight,
        Players = config.Players.Select(x => ToPlayerDto(x)).ToArray(),
        IsOthello = config.IsOthello,
        HaveWalls = config.HaveWalls
    };

    public static GameConfiguration ToDomain(GameConfigDto dto) => new GameConfiguration
    (
        dto.Id,
        dto.BoardWidth,
        dto.BoardHeight,
        dto.Players.Select(x => ToIPlayer(x)).ToArray(),
        dto.IsOthello,
        dto.HaveWalls
    );

    private static PlayerDto ToPlayerDto(IPlayer player)
    {
        return new PlayerDto
        {
            Name = player.Name,
            PlayerType = player is AiPlayer ? "AI" : "Human"
        };
    }

    private static IPlayer ToIPlayer(PlayerDto dto)
    {
            return dto.PlayerType switch
            {
                "AI" => new AiPlayer(),
                "Human" => new HumanPlayer(dto.Name),
                _ => throw new InvalidOperationException(
                    $"Unknown player type: {dto.PlayerType}")
            };
    }
}