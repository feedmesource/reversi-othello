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
        ECellState winner = default;

        while (true)
        {
            ConsoleUi.DrawBoard(Brain.Board);
            Console.WriteLine(errorMessage);
            errorMessage = "";
            if (!isWon)
            {
                ConsoleUi.ShowNextPlayer(Brain.IsNextPlayerRed);
                Console.Write("Choose column: ");
            }
            else
            {
                switch (winner)
                {
                    case ECellState.RedWin:
                        Console.WriteLine("RED WON!");
                        break;
                    case ECellState.YellowWin:
                        Console.WriteLine("YELLOW WON!");
                        break;
                }

                Console.WriteLine();
                Console.Write("Press enter to exit the game:");
                Console.Read();
                break;
            }

            var columnChoice = Console.ReadLine();
            if (columnChoice == null)
            {
                continue;
            }

            if (!int.TryParse(columnChoice, out int columnValue))
            {
                continue;
            }

            var returnedError = Brain.PlacePiece(columnValue);
            if (returnedError != "")
            {
                errorMessage = returnedError;
            }
            else
            {
                winner = Brain.CheckWin(Brain.LastMove);
                ConsoleUi.AnimateMove(Brain.LastMove, Brain.Board);
            }

            if (winner != ECellState.Empty)
            {
                isWon = true;
            }
        }
    }
}