using BLL.Contracts;
using BLL.PlayerTypes;

namespace DAL.DTO;

public static class PlayerMapper
{
    public static PlayerDto ToDto(IPlayer player)
    {
        return new PlayerDto
        {
            Name = player.Name,
            PlayerType = player is AiPlayer ? "AI" : "Human"
        };
    }

    public static IPlayer ToDomain(PlayerDto dto)
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