using BLL;
using ConsoleApp.PlayerTypes;

namespace ConsoleApp;

public static class Variables
{
    public static GameConfiguration Config;
    public static IPlayer[]  Players;

    static Variables()
    {
        Config = new GameConfiguration();
        Players = [new HumanPlayerConsole("Player 1"), new AiPlayerConsole()];
    }
}