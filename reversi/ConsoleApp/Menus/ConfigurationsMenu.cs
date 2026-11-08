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

    private static string AddConfiguration()
    {
        Console.Clear();

        var width = ValidateInputWidthHeight("width");
        var height = ValidateInputWidthHeight("height");

        var reversiOrOthello = ValidateTextInputs("reversi/othello (r/o)", ["r", "o"]);
        var hasWallsOrNot = ValidateTextInputs("has walls (y/n)", ["y", "n"]);

        bool isOthello;
        bool hasWalls;

        isOthello = reversiOrOthello switch
        {
            "r" => false,
            "o" => true,
            _ => throw new ArgumentException("""input not "r" or "o" """)
        };

        hasWalls = hasWallsOrNot switch
        {
            "y" => true,
            "n" => false,
            _ => throw new ArgumentException("""input not "y" or "n" """)
        };

        var config = new GameConfiguration(width, height, isOthello, hasWalls);

        ConfigRepo.Save(config);

        Console.Write("saved!");
        Console.ReadLine();

        return "b";

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

                if (parsedInt is < 4 or > 999)
                {
                    errorMessage = "board dimensions must be between 4 and 999!";
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
            string? userInput;

            while (true)
            {
                if (errorMessage != null)
                {
                    Console.WriteLine(errorMessage);
                }

                Console.Write($"{promptString}: ");
                userInput = Console.ReadLine();
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
}