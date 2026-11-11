using BLL;

namespace DAL.DTO;

public class GameStateDto
{
    public Guid Id { get; init; }
    public ECellPiece[][] Board { get; set; } = [];
    public required GameConfigDto? Config { get; init; }
    public bool IsCurrentPlayerDark { get; init; }
    public DateTime SavedAtUtc { get; init; }
}