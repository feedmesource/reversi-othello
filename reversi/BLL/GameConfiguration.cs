namespace BLL;

public class GameConfiguration
{
    public int BoardWidth { get; init; }
    public int BoardHeight { get; init; }
    public bool IsOthello { get; set; }
    public bool HaveWalls { get; set; }

    public GameConfiguration()
        : this(8, 8)
    {
    }

    public GameConfiguration(bool isOthello)
        : this(8, 8, isOthello)
    {
    }

    public GameConfiguration(int width, int height, bool isOthello = false, bool haveWalls = false)
    {
        BoardWidth = width;
        BoardHeight = height;
        IsOthello = isOthello;
        HaveWalls = haveWalls;
    }
}