namespace DAL.DTO;

public class GameConfigDto
{
    public Guid Id { get; init; }
    public int BoardWidth{ get; set; }
    public int BoardHeight { get; set; }
    public ICollection<PlayerDto> Players { get; set; } = new List<PlayerDto>();
    public bool IsOthello { get; set; }
    public bool HaveWalls { get; set; }

    public ICollection<GameStateDto> GameStates { get; set; } = new List<GameStateDto>();
}