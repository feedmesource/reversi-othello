using BLL;

namespace ConsoleApp;

public class AiPlayerConsole : IPlayer
{
    public string Name { get; } = "AI player";
    private readonly Random _random = new();
    
    public (int column, int row)? GetMove(HashSet<(int x, int y)> allowedMoves)
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