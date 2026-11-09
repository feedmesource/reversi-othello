namespace BLL;

public record GameConfiguration(
    Guid Id,
    int BoardWidth,
    int BoardHeight,
    bool IsOthello = false,
    bool HaveWalls = false)
{
    public GameConfiguration()
        : this(false)
    {
    }

    public GameConfiguration(int height, int width)
        : this(Guid.NewGuid(), height, width)
    {
    }

    public GameConfiguration(int height, int width, bool IsOthello)
        : this(Guid.NewGuid(), height, width, IsOthello)
    {
    }

    public GameConfiguration(int height, int width, bool IsOthello, bool HaveWalls)
        : this(Guid.NewGuid(), height, width, IsOthello, HaveWalls)
    {
    }

    public GameConfiguration(bool isOthello, bool haveWalls = false)
        : this(Guid.NewGuid(), 8, 8, isOthello, haveWalls)
    {
    }

    public override string ToString()
    {
        return $"{(IsOthello ? "Othello" : "Reversi")} {BoardWidth}x{BoardHeight} {(HaveWalls ? "with walls" : "")}";
    }
}