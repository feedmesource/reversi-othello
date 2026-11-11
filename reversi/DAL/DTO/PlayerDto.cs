namespace DAL.DTO;

public class PlayerDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = "";
    public string PlayerType { get; set; } = "";

    public Guid GameConfigDtoId { get; set; }
}