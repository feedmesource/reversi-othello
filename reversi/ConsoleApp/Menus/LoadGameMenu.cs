using BLL;
using BLL.Contracts;
using DAL.JSON;
using MenuSystem;

namespace ConsoleApp.Menus;

public static class LoadGameMenu
{
    public static string LoadGame()
    {
        var menu = new Menu("Load Game", EMenuLevel.Second);

        IGameRepository gameRepo = new JsonGameRepository();
        var games = gameRepo.LoadAll();

        var counter = 1;
        foreach (var gameState in games)
        {
            menu.AddMenuItem(new MenuItem($"{counter}", $"{gameState.SavedAtUtc.ToLocalTime()}", () =>
            {
                var brain = new GameBrain(gameState);
                var controller = new GameController(brain);
                controller.GameLoop();
                return "b";
            }));
            counter++;
        }

        return menu.Run();
    }
}