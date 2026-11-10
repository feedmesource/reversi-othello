using BLL.Contracts;
using DAL.JSON;
using MenuSystem;

namespace ConsoleApp.Menus;

public static class DeleteGameMenu
{
    public static string DeleteMenu()
    {
        var menu = new Menu("Delete game", EMenuLevel.Deep);

        IGameRepository gameRepo = new JsonGameRepository();
        var games = gameRepo.LoadAll();

        var counter = 1;
        foreach (var gameState in games)
        {
            menu.AddMenuItem(new MenuItem($"{counter}", $"{gameState.SavedAtUtc.ToLocalTime()}", () =>
            {
                gameRepo.Delete(gameState.Id);
                return "b";
            }));
            counter++;
        }

        return menu.Run();
    }
}