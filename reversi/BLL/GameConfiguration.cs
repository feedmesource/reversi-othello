namespace BLL;

public class GameConfiguration
{
    public int BoardWidth { get; set; }
    public int BoardHeight { get; set; }
    public bool IsOthello { get; set; }

    public GameConfiguration()
        : this(8, 8)
    {
    }

    public GameConfiguration(bool isOthello)
        : this(8, 8, isOthello)
    {
    }

    public GameConfiguration(int width, int height, bool isOthello = false)
    {
        BoardWidth = width;
        BoardHeight = height;
        IsOthello = isOthello;
    }
}