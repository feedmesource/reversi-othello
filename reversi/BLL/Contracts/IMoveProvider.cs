namespace BLL.Contracts;

public interface IMoveProvider
{
    (int column, int row)? GetMove(IPlayer player, HashSet<(int x, int y)> getAllowedMoves);
}