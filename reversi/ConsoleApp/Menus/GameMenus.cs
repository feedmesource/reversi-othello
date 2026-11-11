using BLL;
using BLL.Contracts;
using DAL.EF;
using MenuSystem;

namespace ConsoleApp.Menus;

public static class GameMenus
{
    private static readonly IGameRepository GameRepo = new EfGameRepository();

    public static string LoadOrDeleteMenu(string title, Func<GameState, string> onSelect)
    {
        var menu = new Menu(title, EMenuLevel.Second);
        var games = GameRepo.LoadAll();

        var counter = 1;
        foreach (var gameState in games)
        {
            menu.AddMenuItem(new MenuItem(
                $"{counter}",
                $"{gameState.SavedAtUtc.ToLocalTime()}",
                () => onSelect(gameState)
            ));
            counter++;
        }

        return menu.Run();
    }
}