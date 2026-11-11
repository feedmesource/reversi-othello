using BLL;
using BLL.Contracts;
using DAL.JSON;

namespace ConsoleApp.Menus;

public static class LoadGameMenu
{
    public static string LoadGame()
    {
        IGameRepository gameRepo = new JsonGameRepository();

        return GameMenus.LoadOrDeleteMenu("Load Game", gameRepo, gameState =>
        {
            var brain = new GameBrain(gameState);
            var controller = new GameController(brain);
            controller.GameLoop();
            return "b";
        });
    }
}