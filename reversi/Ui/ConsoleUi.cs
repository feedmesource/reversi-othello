namespace Ui;

using BLL;

public static class ConsoleUi
{
    public static void DrawBoard(ECellPiece[,] board)
    {
        Console.Clear();
        DrawColumnNumbers(board);
        
        for (var y = 0; y < board.GetLength(1); y++)
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
                // TODO: change background color if cell has win state
                var cell = "| " + DrawCell(board, x, y) + " ";
                Console.Write(cell);
            }

            Console.WriteLine("|");
        }

        DrawLineSeparator(board);
    }

    private static void DrawColumnNumbers(ECellPiece[,] board)
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

    private static string DrawCell(ECellPiece[,] board, int x, int y)
    {
        var cell = board[x, y] switch
        {
            ECellPiece.Empty => " ",
            ECellPiece.Dark => "○",
            ECellPiece.Light => "●",
            _ => "?"
        };
        return cell;
    }

    private static void DrawLineSeparator(ECellPiece[,] board)
    {
        Console.Write("    ");

        for (int i = 0; i < board.GetLength(0); i++)
        {
            Console.Write("----");
        }

        Console.WriteLine("-");
    }

    public static void ShowNextPlayer(bool isNextPlayerDark)
    {
        Console.WriteLine("Next player: " + (isNextPlayerDark ? "dark" : "light"));
    }
    
}