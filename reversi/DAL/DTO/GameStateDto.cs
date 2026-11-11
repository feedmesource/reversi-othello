using BLL;

namespace DAL.DTO;

public class GameStateDto
{
    public Guid Id { get; init; }
    public ECellPiece[][] Board { get; init; } = [];
    public required GameConfiguration Config { get; init; }
    public bool IsCurrentPlayerDark { get; init; }
    public DateTime SavedAtUtc { get; init; }
}