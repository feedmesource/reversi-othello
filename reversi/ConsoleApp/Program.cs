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
            var menu2 = new Menu($"Game configuration: {config.BoardWidth}x{config.BoardHeight} win:{config.WinCondition} cylindrical:{config.IsCylindrical}", EMenuLevel.Deep);
            menu2.AddMenuItem(new MenuItem("1", "Classical: 7x6, win: 4", () =>
            {
                config = new GameConfiguration(7, 6, 4);
                return "b";
            }));
            menu2.AddMenuItem(new MenuItem("2", "Classical: cylindrical", () =>
            {
                config = new GameConfiguration(true);
                return "b";
            }));
            menu2.AddMenuItem(new MenuItem("3", "5x5, win 3", () =>
            {
                config = new GameConfiguration(5, 5, 3);
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