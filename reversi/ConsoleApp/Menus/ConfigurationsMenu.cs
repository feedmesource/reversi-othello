using BLL;
using BLL.Contracts;
using DAL.JSON;
using MenuSystem;

namespace ConsoleApp.Menus;

public static class ConfigurationsMenu
{
    private static readonly Func<string> DummyMethod = Dummies.DummyMethod;
    private static readonly IConfigRepository ConfigRepo = new JsonConfigRepository();

    public static string Menu()
    {
        var menu2 = new Menu("Game Configuration", EMenuLevel.Deep);

        menu2.AddMenuItem(new MenuItem("l", "load configurations",
            LoadConfigurations));

        menu2.AddMenuItem(new MenuItem("a", "add configuration",
            AddConfiguration));

        menu2.AddMenuItem(new MenuItem("d", "delete configuration",
            DeleteConfiguration));

        return menu2.Run();
    }

    private static string LoadConfigurations()
    {
        var menu = new Menu(
            $"Game configuration: {(Variables.Config.IsOthello ? "Othello" : "Reversi")}: {Variables.Config.BoardWidth}x{Variables.Config.BoardHeight}{(Variables.Config.HaveWalls ? " - with walls" : "")}",
            EMenuLevel.Deep);

        menu.AddMenuItem(new MenuItem("1", "Classical Reversi: 8x8", () =>
        {
            Variables.Config = new GameConfiguration(Variables.Players);
            return "b";
        }));

        menu.AddMenuItem(new MenuItem("2", "Mini Reversi: 6x6", () =>
        {
            Variables.Config = new GameConfiguration(6, 6, Variables.Players);
            return "b";
        }));

        menu.AddMenuItem(new MenuItem("3", "Classical Othello: 8x8", () =>
        {
            Variables.Config = new GameConfiguration(Variables.Players, true);
            return "b";
        }));

        menu.AddMenuItem(new MenuItem("4", "Mini Othello: 6x6", () =>
        {
            Variables.Config = new GameConfiguration(6, 6, Variables.Players, true);
            return "b";
        }));

        LoadFromRepository(menu);

        menu.Run();

        return "";
    }

    private static void LoadFromRepository(Menu menu)
    {
        var gameConfigs = ConfigRepo.LoadAll();

        var count = menu.GetMenuItems().Count;

        foreach (var config in gameConfigs)
        {
            count++;

            menu.AddMenuItem(
                new MenuItem(count.ToString(), config.ToString(), () =>
                {
                    Variables.Config = config;
                    return "b";
                })
            );
        }
    }

    private static string AddConfiguration()
    {
        Console.Clear();

        var width = ValidateInputWidthHeight("width");
        var height = ValidateInputWidthHeight("height");

        var reversiOrOthello = ValidateTextInputs("reversi/othello (r/o)", ["r", "o"]);
        var hasWallsOrNot = ValidateTextInputs("has walls (y/n)", ["y", "n"]);

        var isOthello = reversiOrOthello switch
        {
            "r" => false,
            "o" => true,
            _ => throw new ArgumentException("""input not "r" or "o" """)
        };

        var hasWalls = hasWallsOrNot switch
        {
            "y" => true,
            "n" => false,
            _ => throw new ArgumentException("""input not "y" or "n" """)
        };

        var config = new GameConfiguration(width, height, Variables.Players, isOthello, hasWalls);

        ConfigRepo.Save(config);

        Console.Write("saved!");
        Console.ReadLine();

        return "";

        int ValidateInputWidthHeight(string promptString)
        {
            string? errorMessage = default;
            int parsedInt;

            while (true)
            {
                if (errorMessage != null)
                {
                    Console.WriteLine(errorMessage);
                }

                Console.Write($"{promptString}: ");
                var userInput = Console.ReadLine();

                var ifParsed = int.TryParse(userInput, out parsedInt);

                if (!ifParsed)
                {
                    errorMessage = "input must be an integer number!";
                    continue;
                }

                if (parsedInt is < 4 or > 98)
                {
                    errorMessage = "board dimensions must be between 4 and 98!";
                    continue;
                }

                if (parsedInt % 2 != 0)
                {
                    errorMessage = "board dimensions must both be even number!";
                    continue;
                }

                break;
            }

            return parsedInt;
        }

        string ValidateTextInputs(string promptString, string[] validInputs)
        {
            string? errorMessage = default;

            while (true)
            {
                if (errorMessage != null)
                {
                    Console.WriteLine(errorMessage);
                }

                Console.Write($"{promptString}: ");
                var userInput = Console.ReadLine();
                var valid = true;

                foreach (var validInput in validInputs)
                {
                    if (userInput == validInput)
                    {
                        valid = true;
                        break;
                    }
                    else
                    {
                        errorMessage = $"input must be one of the following: {string.Join(", ", validInputs)}!";
                        valid = false;
                    }
                }

                if (valid)
                {
                    return userInput ?? "";
                }
            }
        }
    }

    private static string DeleteConfiguration()
    {
        var menu = new Menu(
            $"Delete configuration",
            EMenuLevel.Deep);

        LoadFromRepositoryToDelete(menu);

        menu.Run();
        return "";
    }

    private static void LoadFromRepositoryToDelete(Menu menu)
    {
        var gameConfigs = ConfigRepo.LoadAll();

        var count = menu.GetMenuItems().Count;

        foreach (var config in gameConfigs)
        {
            count++;

            menu.AddMenuItem(
                new MenuItem(count.ToString(), config.ToString(), () =>
                {
                    ConfigRepo.Delete(config.Id);
                    return "b";
                })
            );
        }
    }
}