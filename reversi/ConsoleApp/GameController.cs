using BLL;
using Ui;

namespace ConsoleApp;

public class GameController
{
    private GameBrain Brain { get; set; }
    private GameConfiguration Config { get; set; }

    public GameController(GameConfiguration config)
    {
        Config = config;
        Brain = new GameBrain(Config);
    }

    public void GameLoop()
    {
        var errorMessage = "";
        var isWon = false;
        ECellPiece winner = default;

        while (true)
        {
            ConsoleUi.DrawBoard(Brain.Board);
            Console.WriteLine(errorMessage);
            errorMessage = "";
            if (!isWon)
            {
                ConsoleUi.ShowNextPlayer(Brain.IsNextPlayerDark);
                Console.Write("Choose column: ");
            }
            else
            {
                switch (winner)
                {
                    case ECellPiece.Dark:
                        Console.WriteLine("DARK WON!");
                        break;
                    case ECellPiece.Light:
                        Console.WriteLine("LIGHT WON!");
                        break;
                }

                Console.WriteLine();
                Console.Write("Press enter to exit the game:");
                Console.Read();
                break;
            }
            
            if (GetUserInput(out var coordinates)) continue;

            var (column, row) = coordinates;
            
            var returnedError = Brain.PlacePiece(column, row);
            if (returnedError != "")
            {
                errorMessage = returnedError;
            }
            else
            {
                winner = Brain.CheckWinner();
                // ConsoleUi.AnimateMove(Brain.LastMove, Brain.Board);
            }

            if (winner != ECellPiece.Empty)
            {
                isWon = true;
            }
        }
    }

    private static bool GetUserInput(out (int, int) coordinates)
    {
        coordinates = default;
        
        var coordinateChoice = Console.ReadLine();
        if (coordinateChoice == null)
        {
            return false;
        }
        
        //TODO: validate input
        var columnRow = coordinateChoice.Split(",", StringSplitOptions.TrimEntries);
        
        if (!int.TryParse(columnRow[0], out var column))
        {
            return false;
        }
        
        if (!int.TryParse(columnRow[1], out var row))
        {
            return false;
        }
        
        coordinates = (column, row);

        return true;
    }
}