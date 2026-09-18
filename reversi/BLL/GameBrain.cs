namespace BLL;

public class GameBrain
{
    private GameConfiguration GameConfig { get; set; }
    public ECellState[,] Board { get; set; }
    private int[] ColumnHeights { get; set; }
    public bool IsNextPlayerRed { get; private set; } = true;
    public (int column, int columnHeight) LastMove { get; private set; }

    public GameBrain()
        : this(new GameConfiguration())
    {
    }

    public GameBrain(GameConfiguration config)
    {
        GameConfig = config;
        Board = new ECellState[GameConfig.BoardWidth, GameConfig.BoardHeight];
        ColumnHeights = new int[GameConfig.BoardWidth];
    }

    public string PlacePiece(int column)
    {
        column--;

        if (column >= Board.GetLength(0) || column < 0)
        {
            return "No such column exists!";
        }

        var columnHeight = ColumnHeights[column];

        if (columnHeight == Board.GetLength(1))
        {
            return "This column is full!";
        }

        ColumnHeights[column]++;

        switch (IsNextPlayerRed)
        {
            case true:
                Board[column, columnHeight] = ECellState.Red;
                break;
            case false:
                Board[column, columnHeight] = ECellState.Yellow;
                break;
        }

        LastMove = (column, columnHeight);

        SwitchPlayer();
        return "";
    }

    private void SwitchPlayer()
    {
        IsNextPlayerRed = !IsNextPlayerRed;
    }

    private (int x, int y) GetDirection(int directionIndex)
    {
        return directionIndex switch
        {
            0 => (-1, 0), // horizontal (left)
            1 => (-1, -1), // diagonal up-left
            2 => (0, -1), // vertical (up)
            3 => (1, -1), // diagonal up-right
            _ => (0, 0)
        };
    }

    private (int x, int y) FlipDirection((int x, int y) direction)
    {
        return (-direction.x, -direction.y);
    }

    private bool IsValidCellPosition(int x, int y)
    {
        return x >= 0 && x < GameConfig.BoardWidth && y >= 0 && y < GameConfig.BoardHeight;
    }

    private bool IsValidCellPositionCylindrical(int y)
    {
        return y >= 0 && y < GameConfig.BoardHeight;
    }

    private bool IsWrappingAroundEdge(int x)
    {
        return !(x >= 0 && x < GameConfig.BoardWidth);
    }

    public ECellState CheckWin((int column, int columnHeight) lastMove)
    {
        var isCylindrical = GameConfig.IsCylindrical;

        var currentPLayer = !IsNextPlayerRed ? ECellState.Red : ECellState.Yellow;

        var (x, y) = lastMove;


        for (var directionIndex = 0; directionIndex < 4; directionIndex++)
        {
            var (dirX, dirY) = GetDirection(directionIndex);

            var countInRow = 1;

            var positions = new List<(int, int)>();
            positions.Add((x, y));

            for (var i = 0; i < 2; i++) // both directions: 2 times
            {
                var nextX = x;
                var nextY = y;

                while (countInRow < GameConfig.WinCondition)
                {
                    nextX += dirX;
                    nextY += dirY;

                    if (isCylindrical)
                    {
                        if (!IsValidCellPositionCylindrical(nextY))
                        {
                            break;
                        }

                        if (IsWrappingAroundEdge(nextX))
                        {
                            if (nextX < 0)
                            {
                                nextX = GameConfig.BoardWidth + nextX; // I add because nextX is negative
                            }
                            else if (nextX >= GameConfig.BoardWidth)
                            {
                                nextX -= GameConfig.BoardWidth;
                            }

                            Console.WriteLine($"X: {nextX}");
                        }
                    }
                    else
                    {
                        if (!IsValidCellPosition(nextX, nextY))
                        {
                            break;
                        }
                    }

                    if (Board[nextX, nextY] == currentPLayer)
                    {
                        countInRow++;
                        positions.Add((nextX, nextY));
                    }
                    else
                    {
                        break;
                    }
                }

                if (countInRow == GameConfig.WinCondition)
                {
                    DrawOutWinningTiles(positions);
                    return currentPLayer == ECellState.Red ? ECellState.RedWin : ECellState.YellowWin;
                }

                (dirX, dirY) = FlipDirection((dirX, dirY));
            }
        }

        return ECellState.Empty;
    }

    private void DrawOutWinningTiles(List<(int, int)> positions)
    {
        foreach (var (x, y) in positions)
        {
            if (Board[x, y] == ECellState.Red)
            {
                Board[x, y] = ECellState.RedWin;
            }
            else if (Board[x, y] == ECellState.Yellow)
            {
                Board[x, y] = ECellState.YellowWin;
            }
        }
    }
}