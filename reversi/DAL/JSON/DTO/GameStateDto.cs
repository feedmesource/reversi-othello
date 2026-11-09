using BLL;

namespace DAL.Json;

public class GameStateDto
{
    public Guid Id { get; set; }
    public ECellPiece[][] Board { get; set; } = [];
    public required GameConfiguration Config { get; set; }
    public bool IsCurrentPlayerDark { get; set; }
    public DateTime SavedAtUtc { get; set; }
}

