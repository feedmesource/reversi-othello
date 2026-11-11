using BLL;
using BLL.Contracts;
using MenuSystem;

namespace ConsoleApp.Menus;

public static class GameMenus
{
    public static string LoadOrDeleteMenu(string title, IGameRepository gameRepo, Func<GameState, string> onSelect)
    {
        var menu = new Menu(title, EMenuLevel.Second);
        var games = gameRepo.LoadAll();

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