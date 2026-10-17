namespace BLL;

public record GameConfiguration(int BoardWidth, int BoardHeight, bool IsOthello = false, bool HaveWalls = false)
{

    public GameConfiguration()
        : this(8, 8)
    {
    }

    public GameConfiguration(bool isOthello)
        : this(8, 8, isOthello)
    {
    }
}