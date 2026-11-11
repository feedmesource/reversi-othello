using BLL.Contracts;
using BLL.PlayerTypes;
using MenuSystem;

namespace ConsoleApp.Menus;

public static class PlayerMenu
{
    public static string Menu()
    {
        var menu = new Menu(
            $"Choose player types! Current: {string.Join(" vs ", Variables.Players.Select(x => x.GetType().ToString()).ToArray())}",
            EMenuLevel.Deep);

        var humanPlayer = new HumanPlayer("Player 1");
        var aiPlayer = new AiPlayer();

        menu.AddMenuItem(new MenuItem("1", "human vs AI",
            ChangePlayers(humanPlayer, aiPlayer)));

        menu.AddMenuItem(new MenuItem("2", "human vs human",
            ChangePlayers(humanPlayer, humanPlayer)));

        menu.AddMenuItem(new MenuItem("3", "AI vs AI",
            ChangePlayers(aiPlayer, aiPlayer)));

        return menu.Run();
    }

    private static Func<string> ChangePlayers(IPlayer player1, IPlayer player2)
    {
        return () =>
        {
            Variables.Players[0] = player1;
            Variables.Players[1] = player2;
            return "b";
        };
    }
}