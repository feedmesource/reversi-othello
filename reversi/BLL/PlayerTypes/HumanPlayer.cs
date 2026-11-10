using BLL.Contracts;

namespace BLL.PlayerTypes;

public class HumanPlayer : IPlayer
{
    public string Name { get; }

    public HumanPlayer(string name)
    {
        Name = name;
    }
}