namespace BLL;

public class GameState
{
    private readonly ECellPiece[,] _board;
    public bool IsCurrentPlayerDark { get; private set; } = true;

    public GameState(int boardWidth, int boardHeight)
    {
        _board =  new ECellPiece[boardWidth, boardHeight];
    }

    public void PlacePiece(int column, int row, ECellPiece piece)
    {
        _board[column, row] = piece;
    }

    public ECellPiece GetCellState(int column, int row)
    {
        return _board[column, row];
    }

    public ECellPiece[,] GetBoard()
    {
        var boardCopy = new ECellPiece[_board.GetLength(0), _board.GetLength(1)];
        Array.Copy(_board, boardCopy,  _board.Length);
        return boardCopy;
    }

    public (int boardWidth, int boardHeight) GetBoardSize()
    {
        return (_board.GetLength(0), _board.GetLength(1));
    }

    public bool IsBoardFull()
    {
        for (var i = 0; i < _board.GetLength(0); i++)
        {
            for (var j = 0; j < _board.GetLength(1); j++)
            {
                if (_board[i, j] == ECellPiece.Empty)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public HashSet<(int, int)> GetEmptyCellCoordinates()
    {
        var emptyCells = new HashSet<(int, int)>();
        
        for (var i = 0; i < _board.GetLength(0); i++)
        {
            for (var j = 0; j < _board.GetLength(1); j++)
            {
                if (_board[i, j] == ECellPiece.Empty)
                {
                    emptyCells.Add((i, j));
                }
            }
        }
        return emptyCells;
    }
    
    public void SwitchPlayer()
    {
        IsCurrentPlayerDark = !IsCurrentPlayerDark;
    }
}