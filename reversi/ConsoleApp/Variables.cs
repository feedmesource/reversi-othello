using BLL;
using ConsoleApp.PlayerTypes;

namespace ConsoleApp;

public static class Variables
{
    public static GameConfiguration Config;
    public static readonly IPlayer[] Players;

    static Variables()
    {
        Players = [new HumanPlayerConsole("Player 1"), new AiPlayerConsole()];
        Config = new GameConfiguration(Players);
    }
}