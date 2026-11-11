namespace DAL.DTO;

public class GameConfigDto
{
    public Guid Id { get; init; }
    public int BoardWidth{ get; init; }
    public int BoardHeight { get; init; }
    public required PlayerDto[] Players { get; init; }
    public bool IsOthello { get; init; }
    public bool HaveWalls { get; init; }
}