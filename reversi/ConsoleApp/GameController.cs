using BLL;
using BLL.Contracts;
using DAL.EF;
using DAL.JSON;
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

    public GameController(GameBrain brain)
    {
        Brain = brain;
        Config = brain.State.Config;
    }

    public void GameLoop()
    {
        var errorMessage = "";

        while (true)
        {
            ConsoleUi.DrawBoard(Brain.GetBoard());
            Console.WriteLine(errorMessage);
            errorMessage = "";

            Brain.GameEndCheckMethods();
            var isWon = Brain.IsGameOver();

            if (!isWon && !Brain.IsValidMoves() && !Brain.IsInitialReversiBoard())
            {
                Console.WriteLine("No valid moves!");
                Console.Write("This turn will be skipped. \nPress Enter:");
                Console.Read();
                Brain.SwitchPlayer();
                continue;
            }

            if (!isWon)
            {
                ConsoleUi.ShowNextPlayer(Brain.IsCurrentPlayerDark);
            }
            else
            {
                var winner = GameBrain.GetWinner(Brain.GameStatus);

                Console.WriteLine(GetWinnerString(winner));
                Console.Write("Press enter to exit the game:");
                Console.Read();
                break;
            }

            bool userInput;
            (int, int) coordinates;

            try
            {
                userInput = GetUserInput(out coordinates);
            }
            catch (ArgumentException e)
            {
                errorMessage = e.Message;
                continue;
            }
            catch (FormatException)
            {
                break;
            }

            if (!userInput)
            {
                errorMessage = "Not valid input";
                continue;
            }

            var (column, row) = coordinates;

            try
            {
                Brain.PlacePiece(column, row);
                // ConsoleUi.AnimateMove(Brain.LastMove, Brain.Board);
            }
            catch (ArgumentException e)
            {
                errorMessage = e.Message;
            }

            IGameRepository gameRepo = new EfGameRepository();
            gameRepo.Save(Brain.State);
        }
    }

    private bool GetUserInput(out (int, int) coordinates)
    {
        var providerFactory = new MoveProviderFactory();
        var currentPlayer = Brain.GetCurrentPlayer();
        var moveProvider = providerFactory.GetProvider(currentPlayer);

        var coordinateChoice = moveProvider.GetMove(currentPlayer, Brain.GetAllValidMoves());

        coordinates = default;

        if (coordinateChoice != null)
        {
            coordinates = ((int, int))coordinateChoice;
            return true;
        }

        return false;
    }

    private string GetWinnerString(ECellPiece winnerPiece)
    {
        var winner = "";
        switch (winnerPiece)
        {
            case ECellPiece.Dark:
                winner = "dark";
                break;
            case ECellPiece.Light:
                winner = "light";
                break;
            case ECellPiece.Empty:
                return "Game was tied!";
        }

        return $"Game over! Winner is {winner}!";
    }
}