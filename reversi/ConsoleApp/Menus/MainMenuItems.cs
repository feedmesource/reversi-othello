using BLL;
using BLL.Contracts;
using DAL.JSON;
using MenuSystem;

namespace ConsoleApp.Menus;

public static class MainMenuItems
{
    private static readonly Func<string> DummyMethod = Dummies.DummyMethod;

    public static readonly List<MenuItem> MenuItems =
    [
        new MenuItem("n", "new game", () =>
            {
                var controller = new GameController(Variables.Config);
                controller.GameLoop();
                return "";
            }
        ),

        new MenuItem("l", "load game", () =>
        {
            var menu = new Menu("Load Game", EMenuLevel.Second);

            IGameRepository gameRepo = new JsonGameRepository();
            var games = gameRepo.LoadAll();

            var counter = 1;
            foreach (var gameState in games)
            {
                menu.AddMenuItem(new MenuItem($"{counter}", $"{gameState.SavedAtUtc.ToLocalTime()}",
                    () =>
                    {
                        var brain = new GameBrain(gameState);
                        var controller = new GameController(brain);
                        controller.GameLoop();
                        return "b";
                    }));
                counter++;
            }

            return menu.Run();
        }),
        new MenuItem("s", "settings", SettingsMenu.Menu)
    ];
}