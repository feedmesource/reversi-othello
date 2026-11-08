using MenuSystem;

namespace ConsoleApp.Menus;

public static class SettingsMenu
{
    private static readonly Func<string> DummyMethod = Dummies.DummyMethod;
    
    public static string Menu()
    {
        var menu1 = new Menu("Settings", EMenuLevel.Second);
        menu1.AddMenuItem(new MenuItem("c", "game configuration", ConfigurationsMenu.Menu));
        menu1.AddMenuItem(new MenuItem("2", "setting 2", DummyMethod));
        menu1.AddMenuItem(new MenuItem("3", "setting 3", DummyMethod));
        var returnValue = menu1.Run();

        return returnValue;
    }
}