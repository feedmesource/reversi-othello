namespace BLL;

public class HumanPlayer : IPlayer
{
    public string Name { get; }

    public HumanPlayer(string name)
    {
        Name = name;
    }
    public (int column, int row)? GetMove(HashSet<(int x, int y)> allowedMoves)
    {
        Console.Write("Choose coordinates (x,y): ");
        
        var coordinateChoice = Console.ReadLine();

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

        return coordinates;
    }
}