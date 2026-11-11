using BLL;

namespace DAL.DTO;

public class GameStateDto
{
    public Guid Id { get; init; }
    public ECellPiece[][] Board { get; set; } = [];
    public required GameConfigDto? Config { get; set; }
    public bool IsCurrentPlayerDark { get; set; }
    public DateTime SavedAtUtc { get; set; }
}