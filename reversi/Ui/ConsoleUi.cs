namespace Ui;

using BLL;

public static class ConsoleUi
{
    public static void DrawBoard(ECellState[,] board)
    {
        Console.Clear();
        DrawColumnNumbers(board);

        // cycle through backwards
        for (var y = (board.GetLength(1) - 1); y >= 0; y--)
        {
            DrawLineSeparator(board);
            switch (y)
            {
                case <= 8:
                    Console.Write("  " + (y + 1) + " ");
                    break;
                case <= 98:
                    Console.Write(" " + (y + 1) + " ");
                    break;
                case <= 998:
                    Console.Write((y + 1) + " ");
                    break;
                default:
                    throw new IndexOutOfRangeException("board width and height should be maximum 999");
            }

            for (var x = 0; x < board.GetLength(0); x++)
            {
                var cell = "| " + DrawCell(board, x, y) + " ";
                Console.Write(cell);
            }

            Console.WriteLine("|");
        }

        DrawLineSeparator(board);
    }

    private static void DrawColumnNumbers(ECellState[,] board)
    {
        Console.Write("    ");
        for (int i = 0; i < board.GetLength(0); i++)
        {
            switch (i)
            {
                case <= 8:
                    Console.Write("  " + (i + 1) + " ");
                    break;
                case <= 98:
                    Console.Write(" " + (i + 1) + " ");
                    break;
                case <= 998:
                    Console.Write((i + 1) + " ");
                    break;
                default:
                    throw new IndexOutOfRangeException("board width and height should be maximum 999");
            }
        }

        Console.WriteLine();
    }

    private static string DrawCell(ECellState[,] board, int x, int y)
    {
        var cell = board[x, y] switch
        {
            ECellState.Empty => " ",
            ECellState.Red => "r",
            ECellState.Yellow => "y",
            ECellState.RedWin => "R",
            ECellState.YellowWin => "Y",
            _ => "?"
        };
        return cell;
    }

    private static void DrawLineSeparator(ECellState[,] board)
    {
        Console.Write("    ");

        for (int i = 0; i < board.GetLength(0); i++)
        {
            Console.Write("----");
        }

        Console.WriteLine("-");
    }

    public static void ShowNextPlayer(bool isNextPlayerRed)
    {
        Console.WriteLine("Next player: " + (isNextPlayerRed ? "red" : "yellow"));
    }

    public static void AnimateMove((int column, int columnHeight) lastMove, ECellState[,] board)
    {
        var (column, columnHeight) = lastMove;
        var lastPlayer = board[column, columnHeight];

        var temporaryBoard = (ECellState[,])board.Clone();

        temporaryBoard[column, columnHeight] = ECellState.Empty;
        for (var i = (temporaryBoard.GetLength(1) - 1); i >= lastMove.columnHeight; i--)
        {
            temporaryBoard[column, i] = lastPlayer;
            DrawBoard(temporaryBoard);
            Thread.Sleep(200);
            temporaryBoard[column, i] = ECellState.Empty;
        }
    }
}