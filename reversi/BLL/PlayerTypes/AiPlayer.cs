using BLL.Contracts;

namespace BLL.PlayerTypes;

public class AiPlayer : IPlayer
{
    public string Name { get; } = "AI player";
}