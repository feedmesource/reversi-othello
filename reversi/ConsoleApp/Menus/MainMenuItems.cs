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

        new MenuItem("l", "load game", LoadGameMenu.LoadGame),

        new MenuItem("d", "delete game", DeleteGameMenu.DeleteMenu),
        new MenuItem("s", "settings", SettingsMenu.Menu)
    ];
}