using System.ComponentModel.DataAnnotations;

namespace DAL.DTO;

public class PlayerDto
{
    public Guid Id { get; set; }
    [MaxLength(128)]
    public string Name { get; set; } = "";
    [MaxLength(128)]
    public string PlayerType { get; set; } = "";
    public Guid GameConfigDtoId { get; set; }
}