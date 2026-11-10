using BLL.Contracts;

namespace BLL;

public class GameBrain
{
    public readonly GameState State;
    public bool IsCurrentPlayerDark => State.IsCurrentPlayerDark;
    private int CurrentPlayerIndex { get; set; }
    private int NoValidMovesCount { get; set; }
    private (int, int)[] ValidFirst4Moves { get; } = new (int, int)[4];
    public EGameStatus GameStatus { get; private set; }

    public GameBrain(GameConfiguration config)
        : this(new GameState(config.BoardWidth, config.BoardHeight, config))
    {
    }

    public GameBrain(GameState state)
    {
        State = state;
        CurrentPlayerIndex = IsCurrentPlayerDark ? 0 : 1;

        var positionX = State.Config.BoardWidth / 2 - 1; // -1 because of 0-indexing
        var positionY = State.Config.BoardHeight / 2 - 1; // -1 because of 0-indexing
        ValidFirst4Moves[0] = (positionX + 1, positionY);
        ValidFirst4Moves[1] = (positionX, positionY);
        ValidFirst4Moves[2] = (positionX, positionY + 1);
        ValidFirst4Moves[3] = (positionX + 1, positionY + 1);

        if (State.Config.IsOthello && !IsPiecesCountAtLeast4())
        {
            foreach (var (column, row) in ValidFirst4Moves)
            {
                PlacePiece(column, row);
            }
        }
    }

    public bool IsInitialReversiBoard()
    {
        return !State.Config.IsOthello && !IsPiecesCountAtLeast4();
    }

    public void PlacePiece(int column, int row)
    {
        if (!IsCellCoordinateOnBoard(column, row)) throw new ArgumentException("No such coordinate");
        // LastMove = (column, row);

        if (State.GetCellState(column, row) != ECellPiece.Empty)
            throw new ArgumentException("There is a piece already in that position");

        var moveMade = TryMakeMove(column, row);
        if (!moveMade) throw new ArgumentException("Invalid move");

        SwitchPlayer();
    }


    private static (int x, int y) GetDirection(int directionIndex)
    {
        return directionIndex switch
        {
            0 => (-1, 0), // horizontal (left)
            1 => (-1, -1), // diagonal up-left
            2 => (0, -1), // vertical (up)
            3 => (1, -1), // diagonal up-right
            4 => (1, 0), // horizontal (right)
            5 => (1, 1), // diagonal down-right
            6 => (0, 1), // vertical (down)
            7 => (-1, 1), // diagonal down-left
            _ => (0, 0)
        };
    }


    private bool IsCellCoordinateOnBoard(int x, int y)
    {
        return x >= 0 && x < State.Config.BoardWidth && y >= 0 && y < State.Config.BoardHeight;
    }


    private bool TryMakeMove(int column, int row)
    {
        var currentPLayer = State.IsCurrentPlayerDark ? ECellPiece.Dark : ECellPiece.Light;

        var isMoveValid = false;

        var positions = new List<(int, int)>();

        if (!IsPiecesCountAtLeast4())
        {
            return Initial4CenterCellsPlacement(column, row, currentPLayer);
        }

        for (var directionIndex = 0; directionIndex < 8; directionIndex++)
        {
            var (dirX, dirY) = GetDirection(directionIndex);

            var nextX = column + dirX;
            var nextY = row + dirY;

            if (!IsCellCoordinateOnBoard(nextX, nextY)) continue;

            do
            {
                if (State.GetCellState(nextX, nextY) == FlipPlayer(currentPLayer))
                    positions.Add((nextX, nextY));
                else
                    break;

                nextX += dirX;
                nextY += dirY;

                if (!IsCellCoordinateOnBoard(nextX, nextY))
                    break;
            } while (State.GetCellState(nextX, nextY) != currentPLayer ||
                     State.GetCellState(nextX, nextY) != ECellPiece.Empty);


            if (IsCellCoordinateOnBoard(nextX, nextY) && State.GetCellState(nextX, nextY) == currentPLayer)
            {
                if (positions.Count > 0)
                {
                    ChangeBoard(column, row, currentPLayer, positions);
                    isMoveValid = true;
                }
            }

            positions.Clear();
        }

        return isMoveValid;
    }

    private void ChangeBoard(int column, int row, ECellPiece currentPLayer, List<(int, int)> positions)
    {
        State.PlacePiece(column, row, currentPLayer);
        FlipPieces(positions);
    }

    private bool Initial4CenterCellsPlacement(int column, int row, ECellPiece currentPLayer)
    {
        if (ValidFirst4Moves.Contains((column, row)))
        {
            State.PlacePiece(column, row, currentPLayer);
            return true;
        }

        return false;
    }

    private bool IsValidMove(int column, int row)
    {
        ECellPiece currentPLayer = State.IsCurrentPlayerDark ? ECellPiece.Dark : ECellPiece.Light;

        var positions = new List<(int, int)>();

        for (var directionIndex = 0; directionIndex < 8; directionIndex++)
        {
            var (dirX, dirY) = GetDirection(directionIndex);

            var nextX = column;
            var nextY = row;

            nextX += dirX;
            nextY += dirY;
            if (!IsCellCoordinateOnBoard(nextX, nextY)) continue;

            do
            {
                if (State.GetCellState(nextX, nextY) == FlipPlayer(currentPLayer))
                    positions.Add((nextX, nextY));
                else
                    break;

                nextX += dirX;
                nextY += dirY;

                if (!IsCellCoordinateOnBoard(nextX, nextY)) break;
            } while (State.GetCellState(nextX, nextY) == FlipPlayer(currentPLayer));


            if (IsCellCoordinateOnBoard(nextX, nextY) && State.GetCellState(nextX, nextY) == currentPLayer)
            {
                if (positions.Count > 0)
                    return true;
            }

            positions.Clear();
        }

        return false;
    }

    public bool IsGameOver()
    {
        return GameStatus != EGameStatus.InProgress;
    }

    public bool IsValidMoves()
    {
        foreach (var (column, row) in GetEmptyCellCoordinates().ToArray())
        {
            var cell = State.GetCellState(column, row);
            if (cell == ECellPiece.Empty && IsValidMove(column, row))
            {
                NoValidMovesCount = 0;
                return true;
            }
        }

        NoValidMovesCount++;
        return false;
    }

    public HashSet<(int, int)> GetAllValidMoves()
    {
        var validMoves = new HashSet<(int, int)>();

        if (!IsPiecesCountAtLeast4())
        {
            return AllValidMovesInCenter4Cells();
        }

        return AllValidMovesDefault();

        HashSet<(int, int)> AllValidMovesInCenter4Cells()
        {
            foreach (var (column, row) in ValidFirst4Moves)
            {
                if (State.GetCellState(column, row) == ECellPiece.Empty)
                {
                    validMoves.Add((column, row));
                }
            }

            return validMoves;
        }

        HashSet<(int, int)> AllValidMovesDefault()
        {
            foreach (var (column, row) in GetEmptyCellCoordinates().ToArray())
            {
                if (State.GetCellState(column, row) == ECellPiece.Empty && IsValidMove(column, row))
                {
                    validMoves.Add((column, row));
                }
            }

            return validMoves;
        }
    }


    private void FlipPieces(List<(int, int)> positions)
    {
        foreach (var (x, y) in positions)
        {
            State.PlacePiece(x, y, FlipPlayer(State.GetCellState(x, y)));
        }
    }

    private static ECellPiece FlipPlayer(ECellPiece piece)
    {
        return piece switch
        {
            ECellPiece.Dark => ECellPiece.Light,
            ECellPiece.Light => ECellPiece.Dark,
            ECellPiece.Empty => throw new ArgumentException("Pieces to flip shouldn't include Empty cell."),
            _ => throw new ArgumentException("This should never run.")
        };
    }

    private bool IsPiecesCountAtLeast4()
    {
        var piecesCount = 0;

        foreach (var cell in State.GetBoard())
        {
            if (cell != ECellPiece.Empty)
                piecesCount++;

            if (piecesCount >= 4) return true;
        }

        return false;
    }

    private EGameStatus GetWinStatus()
    {
        var darkCount = 0;
        var lightCount = 0;

        foreach (var cell in State.GetBoard())
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

        if (darkCount > lightCount) return EGameStatus.DarkWon;

        if (lightCount > darkCount) return EGameStatus.LightWon;

        return EGameStatus.Draw;
    }

    public static ECellPiece GetWinner(EGameStatus status)
    {
        return status switch
        {
            EGameStatus.DarkWon => ECellPiece.Dark,
            EGameStatus.LightWon => ECellPiece.Light,
            _ => ECellPiece.Empty
        };
    }

    public void GameEndCheckMethods()
    {
        if (IsPiecesCountAtLeast4())
        {
            if (NoValidMovesCount == 2 || IsBoardFull())
            {
                GameStatus = GetWinStatus();
            }
        }
    }

    /*private void HighlightWinningTiles(ECellPiece winner)
    {
        for (var i = 0; i < WinStateBoard.GetLength(0); i++)
        {
            for (var j = 0; j < WinStateBoard.GetLength(1); j++)
            {
                if (winner == ECellPiece.Dark && Board[i, j] == ECellPiece.Dark ||
                    winner == ECellPiece.Light && Board[i, j] == ECellPiece.Light)
                {
                    WinStateBoard[i, j] = ECellWinState.Win;
                }
                else if (winner == ECellPiece.Empty)
                {
                    WinStateBoard[i, j] = ECellWinState.Tie;
                }
            }
        }
    }*/


    public void SwitchPlayer()
    {
        State.SwitchPlayer();
        CurrentPlayerIndex = ++CurrentPlayerIndex % 2;
    }

    public IPlayer GetCurrentPlayer()
    {
        return State.Config.Players[CurrentPlayerIndex];
    }

    public ECellPiece[,] GetBoard() => State.GetBoard();
    private bool IsBoardFull() => State.IsBoardFull();

    private HashSet<(int, int)> GetEmptyCellCoordinates() => State.GetEmptyCellCoordinates();
}