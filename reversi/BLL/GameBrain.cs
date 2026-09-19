namespace BLL;

public class GameBrain
{
    private GameConfiguration GameConfig { get; set; }
    public ECellPiece[,] Board { get; set; }
    public ECellWinState[,] WinStateBoard { get; set; }
    public bool IsNextPlayerDark { get; private set; } = true;
    public (int column, int columnHeight) LastMove { get; private set; }
    private ECellPiece Winner { get; set; }

    public GameBrain()
        : this(new GameConfiguration())
    {
    }

    public GameBrain(GameConfiguration config)
    {
        GameConfig = config;
        Board = new ECellPiece[GameConfig.BoardWidth, GameConfig.BoardHeight];
        WinStateBoard = new ECellWinState[GameConfig.BoardWidth, GameConfig.BoardHeight];
    }

    public string PlacePiece(int column, int row)
    {
        const string errorMessage = "Invalid move";

        if (!IsValidCellPosition(column, row))
        {
            return "No such coordinate";
        }
        // LastMove = (column, row);

        var moveMade = MakeMove(column, row);
        if (!moveMade) return errorMessage;
        
        if (!IsValidMoves() || IsBoardFull())
        {
            Winner = CheckWinner();
            HighlightWinningTiles(Winner);
            
            // TODO: Check tie too
            return $"Game over! Winner is {(Winner == ECellPiece.Dark ? "Dark" : "Light")}";
        }

        SwitchPlayer();
        return "";
    }

    private void SwitchPlayer()
    {
        IsNextPlayerDark = !IsNextPlayerDark;
    }

    private static (int x, int y) GetDirection(int directionIndex)
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

    private static (int x, int y) FlipDirection((int x, int y) direction)
    {
        return (-direction.x, -direction.y);
    }

    private bool IsValidCellPosition(int x, int y)
    {
        return x >= 0 && x < GameConfig.BoardWidth && y >= 0 && y < GameConfig.BoardHeight;
    }


    public bool MakeMove(int column, int row)
    {
        var currentPLayer = !IsNextPlayerDark ? ECellPiece.Dark : ECellPiece.Light;

        var isMoveValid = false;

        var positions = new List<(int, int)>();
        
        for (var directionIndex = 0; directionIndex < 4; directionIndex++)
        {
            var (dirX, dirY) = GetDirection(directionIndex);
            
            for (var i = 0; i < 2; i++) // both directions: 2 times
            {
                var nextX = column;
                var nextY = row;

                do
                {
                    if (!IsValidCellPosition(nextX, nextY))
                    {
                        break;
                    }

                    if (Board[nextX, nextY] == FlipPlayer(currentPLayer))
                    {
                        positions.Add((nextX, nextY));
                    }
                    // else
                    // {
                    //     break;
                    // }

                    nextX += dirX;
                    nextY += dirY;
                } while (Board[nextX, nextY] != currentPLayer || Board[nextX, nextY] != ECellPiece.Empty);

                if (Board[nextX, nextY] == currentPLayer)
                {
                    FlipPieces(positions);
                    isMoveValid = true;
                }

                positions.Clear();
                
                (dirX, dirY) = FlipDirection((dirX, dirY));
            }
        }

        return isMoveValid;
    }
    
    private bool IsValidMoves()
    {
        throw new NotImplementedException();
    }

    private bool IsBoardFull()
    {
        throw new NotImplementedException();
    }
    
    private void FlipPieces(List<(int, int)> positions)
    {
        foreach (var (x, y) in positions)
        {
            Board[x, y] = FlipPlayer(Board[x, y]);
        }
    }

    private static ECellPiece FlipPlayer(ECellPiece piece)
    {
        if (piece == ECellPiece.Dark)
        {
            return ECellPiece.Light;
        }

        if (piece == ECellPiece.Light)
        {
            return ECellPiece.Dark;
        }

        if (piece == ECellPiece.Empty)
        {
            throw new ArgumentException("Pieces to flip shouldn't include Empty cell.");
        }

        throw new ArgumentException("Pieces to flip shouldn't include winning cell.");
    }

    public ECellPiece CheckWinner()
    {
        var darkCount = 0;
        var lightCount = 0;

        foreach (var cell in Board)
        {
            switch (cell)
            {
                case ECellPiece.Dark:
                    darkCount++;
                    break;
                case ECellPiece.Light:
                    lightCount++;
                    break;
                case ECellPiece.Empty:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        if (darkCount > lightCount)
        {
            return ECellPiece.Dark;
        }

        if (lightCount > darkCount)
        {
            return ECellPiece.Light;
        }

        return ECellPiece.Empty;
    }

    private void HighlightWinningTiles(ECellPiece winner)
    {
        for (var i = 0; i < WinStateBoard.GetLength(0); i++)
        {
            for (var j = 0; j < WinStateBoard.GetLength(1); j++)
            {
                if (winner == ECellPiece.Dark && Board[i, j] == ECellPiece.Dark || winner == ECellPiece.Light && Board[i, j] == ECellPiece.Light)
                {
                    WinStateBoard[i, j] = ECellWinState.Win;
                }
            }
        }
    }
}