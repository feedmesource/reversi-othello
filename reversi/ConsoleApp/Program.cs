// See https://aka.ms/new-console-template for more information

using DAL.DTO;
using DAL.EF;
using MenuSystem;
using Microsoft.EntityFrameworkCore;

using var db = new AppDbContext();
db.Database.Migrate();

// Create
/*db.Configs.Add(new GameConfigDto
{
    BoardWidth = 7,
    BoardHeight = 6,
    IsOthello = false,
    HaveWalls = false,
    Players =
    [
        new PlayerDto{ Name = "AI Player", PlayerType = "AI"},
        new PlayerDto{ Name = "Player 2", PlayerType = "Human"}
    ]
});
db.SaveChanges();
*/


var menu = new Menu("Reversi - Othello", EMenuLevel.First);

var menuItems = ConsoleApp.Menus.MainMenuItems.MenuItems;

foreach (var item in menuItems)
{
    menu.AddMenuItem(item);
}

menu.Run();


// Read
/*
var all = db.Configs
    .Include(c => c.Players)
    .ToList();

foreach (var c in all)
{
    Console.WriteLine(
        $"{c.Id}: " +
        $"{c.BoardWidth}x{c.BoardHeight} " +
        $"{(c.IsOthello ? "Othello" : "Reversi")} " +
        $"{(c.HaveWalls ? "with walls" : "")}" +
        $"Players: {string.Join(", ", c.Players.Select(p => p.PlayerType))}");
}
*/


//var widthSeven = db.Configurations.First(c => c.BoardWidth == 17);

// Update — change a tracked entity, then save
/* widthSeven.BoardWidth = 17;
db.SaveChanges();
*/

// Delete
/*
db.Configurations.Remove(widthSeven);
db.SaveChanges();
*/