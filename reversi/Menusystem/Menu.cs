namespace MenuSystem;

public class Menu
{
    private string Title { get; set; }
    private EMenuLevel Level { get; set; }

    public Menu(string title, EMenuLevel level)
    {
        Title = title;
        Level = level;
    }

    private Dictionary<string, MenuItem> MenuItems { get; set; } = [];

    public void AddMenuItem(MenuItem item)
    {
        if (!MenuItems.ContainsKey(item.Label))
        {
            MenuItems.Add(item.Label, item);
        }
        else
        {
            throw new ArgumentException($"MenuItem {item.Label} is already in the menu");
        }
    }

    public List<MenuItem> GetMenuItems()
    {
        return MenuItems.Values.ToList();
    }

    public string Run()
    {
        AddDefaultMenuItems();

        var errorMessage = "";
        var userChoice = "";

        var isMenuDone = false;
        while (!isMenuDone)
        {
            Console.Clear();
            DisplayMenu();

            Console.WriteLine(errorMessage);
            errorMessage = "";
            Console.Write("Choose: ");
            userChoice = Console.ReadLine()?.ToLower();

            // do something with the choice

            if (userChoice == null || !MenuItems.ContainsKey(userChoice))
            {
                errorMessage = $"Invalid choice: {userChoice}";
                continue;
            }

            var returnValue = MenuItems[userChoice].MethodToRun.Invoke();
            switch (returnValue)
            {
                case "x":
                    return "x";
                case "m" when Level != EMenuLevel.First:
                    return "m";
                case "b":
                    return "";
            }
        }
        return userChoice ?? "";
    }

    private void DisplayMenu()
    {
        Console.WriteLine(Title);
        Console.WriteLine("--------------");

        foreach (var item in GetMenuItems())
        {
            Console.WriteLine(item.ToString());
        }
    }

    private void AddDefaultMenuItems()
    {
        var returnBackMenuItem = new MenuItem("b", "back to previous menu", () => "b");
        var returnMainMenuItem = new MenuItem("m", "return to main", () => "m");
        var exitMenuItem = new MenuItem("x", "exit", () => "x");
        switch (Level)
        {
            case EMenuLevel.First:
                AddMenuItem(exitMenuItem);
                break;

            case EMenuLevel.Second:
                AddMenuItem(returnMainMenuItem);
                AddMenuItem(exitMenuItem);
                break;

            case EMenuLevel.Deep:
                AddMenuItem(returnBackMenuItem);
                AddMenuItem(returnMainMenuItem);
                AddMenuItem(exitMenuItem);
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}