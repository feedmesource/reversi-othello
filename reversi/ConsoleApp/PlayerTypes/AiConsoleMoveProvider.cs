using BLL.Contracts;

namespace ConsoleApp.PlayerTypes;

public class AiConsoleMoveProvider : IMoveProvider
{
    private readonly Random _random = new();
    
    public (int column, int row)? GetMove(IPlayer player, HashSet<(int x, int y)> allowedMoves)
    {

        Console.WriteLine("AI turn to make move");
        Console.Write("Let's see what he does? ");
        Console.Read();

        if (allowedMoves.Count == 0)
        {
            return null;
        }

        return allowedMoves.ToArray()[_random.Next(allowedMoves.Count)];
    }
}