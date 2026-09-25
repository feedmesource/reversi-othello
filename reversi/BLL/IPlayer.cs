namespace BLL;

public interface IPlayer
{
    public string Name { get; }
    (int column, int row)? GetMove(HashSet<(int x, int y)> getAllowedMoves);
}