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
        Brain = new GameBrain(Config, [new HumanPlayer("Player 1"), new AiPlayer()]);
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
            
            if (!isWon && !Brain.IsValidMoves())
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

            if (!GetUserInput(out var coordinates))
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
        }
    }

    private bool GetUserInput(out (int, int) coordinates)
    {
        var currentPlayer = Brain.GetCurrentPlayer();

        var coordinateChoice = currentPlayer.GetMove(Brain.GetAllValidMoves());
        
        coordinates = default;

        if (coordinateChoice != null)
        {
            coordinates = ((int, int)) coordinateChoice;
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