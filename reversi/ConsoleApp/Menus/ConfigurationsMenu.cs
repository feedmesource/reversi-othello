using BLL;
using MenuSystem;

namespace ConsoleApp.Menus;

public static class ConfigurationsMenu
{
    private static readonly Func<string> DummyMethod = Dummies.DummyMethod;
    
    public static string Menu()
    {
        var menu2 = new Menu("Game Configuration", EMenuLevel.Deep);
            
        menu2.AddMenuItem(new MenuItem("l", "load configurations",
            LoadConfigurations));
            
        menu2.AddMenuItem(new MenuItem("a", "add configuration",
            DummyMethod));

        return menu2.Run();
    }

    private static string LoadConfigurations()
    {
        var menu = new Menu(
            $"Game configuration: {(Variables.Config.IsOthello ? "Othello" : "Reversi")}: {Variables.Config.BoardWidth}x{Variables.Config.BoardHeight}{(Variables.Config.HaveWalls ? " - with walls" : "")}",
            EMenuLevel.Deep);
        
        menu.AddMenuItem(new MenuItem("1", "Classical Reversi: 8x8", () =>
        {
            Variables.Config = new GameConfiguration();
            return "b";
        }));
        
        menu.AddMenuItem(new MenuItem("2", "Mini Reversi: 6x6", () =>
        {
            Variables.Config = new GameConfiguration(6, 6);
            return "b";
        }));
        
        menu.AddMenuItem(new MenuItem("3", "Classical Othello: 8x8", () =>
        {
            Variables.Config = new GameConfiguration(true);
            return "b";
        }));
        
        menu.AddMenuItem(new MenuItem("4", "Mini Othello: 6x6", () =>
        {
            Variables.Config = new GameConfiguration(6, 6, true);
            return "b";
        }));
        
        var returnValue = menu.Run();

        return returnValue;
    }
}