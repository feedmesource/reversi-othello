namespace BLL;

public class GameConfiguration
{
    public int BoardWidth { get; set; }
    public int BoardHeight { get; set; }
    public int WinCondition { get; set; }
    public bool IsCylindrical { get; set; }

    public GameConfiguration()
        : this(7, 6, 4)
    {
    }

    public GameConfiguration(bool isCylindrical)
        : this(7, 6, 4, isCylindrical)
    {
    }

    public GameConfiguration(int width, int height, int winCondition, bool isCylindrical = false)
    {
        BoardWidth = width;
        BoardHeight = height;
        WinCondition = winCondition;
        IsCylindrical = isCylindrical;
    }
}