using BLL;
using BLL.Contracts;
using BLL.PlayerTypes;

namespace ConsoleApp;

public static class Variables
{
    public static GameConfiguration Config;
    public static IPlayer[] Players;

    static Variables()
    {
        Players = [new HumanPlayer("Player 1"), new AiPlayer()];
        Config = new GameConfiguration(Players);
    }
}