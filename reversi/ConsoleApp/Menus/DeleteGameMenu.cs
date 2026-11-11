using BLL.Contracts;
using DAL.EF;
using DAL.JSON;

namespace ConsoleApp.Menus;

public static class DeleteGameMenu
{
    public static string DeleteMenu()
    {
        IGameRepository gameRepo = new EfGameRepository();

        return GameMenus.LoadOrDeleteMenu("Delete game", gameState =>
        {
            gameRepo.Delete(gameState.Id);
            return "b";
        });
    }
}