// See https://aka.ms/new-console-template for more information

using BLL;
using ConsoleApp;
using MenuSystem;

var menu = new Menu("Hyper Connect x", EMenuLevel.First);
var config = new GameConfiguration();

string DummyMethod()
{
    return ""; // TODO: remove later
}

var menuItems = new List<MenuItem>()
{
    new MenuItem("n", "new game", () =>
        {
            var controller = new GameController(config);
            controller.GameLoop();
            return "";
        }
    ),
    new MenuItem("l", "load game", DummyMethod),
    new MenuItem("s", "settings", () =>
    {
        var menu1 = new Menu("Settings", EMenuLevel.Second);
        menu1.AddMenuItem(new MenuItem("c", "Game configuration", () =>
        {
            var menu2 = new Menu($"Game configuration: {(config.IsOthello ? "Othello" : "Reversi")}: {config.BoardWidth}x{config.BoardHeight}{(config.HaveWalls ? " - with walls" : "")}", EMenuLevel.Deep);
            menu2.AddMenuItem(new MenuItem("1", "Classical Reversi: 8x8", () =>
            {
                config = new GameConfiguration();
                return "b";
            }));
            menu2.AddMenuItem(new MenuItem("2", "Mini Reversi: 6x6", () =>
            {
                config = new GameConfiguration(6, 6);
                return "b";
            }));
            menu2.AddMenuItem(new MenuItem("3", "Classical Othello: 8x8", () =>
            {
                config = new GameConfiguration(true);
                return "b";
            }));
            menu2.AddMenuItem(new MenuItem("6", "Mini Othello: 8x8", () =>
            {
                config = new GameConfiguration(6, 6, true);
                return "b";
            }));
            var returnValue = menu2.Run();

            return returnValue;
        }));
        menu1.AddMenuItem(new MenuItem("2", "setting 2", DummyMethod));
        menu1.AddMenuItem(new MenuItem("3", "setting 3", DummyMethod));
        var returnValue = menu1.Run();

        return returnValue;
    }),
};

foreach (var item in menuItems)
{
    menu.AddMenuItem(item);
}

menu.Run();