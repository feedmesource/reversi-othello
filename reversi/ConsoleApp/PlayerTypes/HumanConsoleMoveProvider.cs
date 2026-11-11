using BLL.Contracts;

namespace ConsoleApp.PlayerTypes;

public class HumanConsoleMoveProvider : IMoveProvider
{
    public (int column, int row)? GetMove(IPlayer player, HashSet<(int x, int y)> allowedMoves)
    {
        Console.Write("Choose coordinates (x,y): ");

        var coordinateChoice = Console.ReadLine();

        if (coordinateChoice == "exit")
        {
            throw new FormatException("Exit");
        }

        if (coordinateChoice == null)
            return null;

        var columnRow = coordinateChoice.Split(",", StringSplitOptions.TrimEntries);

        if (columnRow.Length != 2)
            return null;

        if (!int.TryParse(columnRow[0], out var column))
            return null;

        if (!int.TryParse(columnRow[1], out var row))
            return null;

        var coordinates = (--column, --row);

        if (!allowedMoves.Contains(coordinates))
            throw new ArgumentException("Invalid move");

        return coordinates;
    }
}