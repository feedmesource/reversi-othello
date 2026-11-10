namespace BLL;

public record GameConfiguration(
    Guid Id,
    int BoardWidth,
    int BoardHeight,
    IPlayer[] Players,
    bool IsOthello = false,
    bool HaveWalls = false)
{
    public GameConfiguration()
        : this(new IPlayer[2])
    {
    }

    public GameConfiguration(int height, int width, IPlayer[] players)
        : this(Guid.NewGuid(), height, width, players)
    {
    }

    public GameConfiguration(int height, int width, IPlayer[] players, bool IsOthello)
        : this(Guid.NewGuid(), height, width, players, IsOthello)
    {
    }

    public GameConfiguration(int height, int width, IPlayer[] players, bool IsOthello, bool HaveWalls)
        : this(Guid.NewGuid(), height, width, players, IsOthello, HaveWalls)
    {
    }

    public GameConfiguration(IPlayer[] players, bool isOthello = false, bool haveWalls = false)
        : this(Guid.NewGuid(), 8, 8, players, isOthello, haveWalls)
    {
    }

    public override string ToString()
    {
        return $"{(IsOthello ? "Othello" : "Reversi")} {BoardWidth}x{BoardHeight} {(HaveWalls ? "with walls" : "")}";
    }
}