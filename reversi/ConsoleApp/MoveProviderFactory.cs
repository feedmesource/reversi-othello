using BLL.Contracts;
using BLL.PlayerTypes;
using ConsoleApp.PlayerTypes;

namespace ConsoleApp;

public class MoveProviderFactory
{
    private readonly HumanConsoleMoveProvider _humanProvider = new();
    private readonly AiConsoleMoveProvider _aiProvider = new();

    public IMoveProvider GetProvider(IPlayer player)
    {
        return player switch
        {
            HumanPlayer => _humanProvider,
            AiPlayer => _aiProvider,
            _ => throw new NotSupportedException()
        };
    }
}