using BLL.Contracts;
using DAL.JSON;

namespace ConsoleApp.Menus;

public static class DeleteGameMenu
{
    public static string DeleteMenu()
    {
        IGameRepository gameRepo = new JsonGameRepository();

        return GameMenus.LoadOrDeleteMenu("Delete game", gameRepo, gameState =>
        {
            gameRepo.Delete(gameState.Id);
            return "b";
        });
    }
}