// See https://aka.ms/new-console-template for more information

using MenuSystem;

var menu = new Menu("Reversi - Othello", EMenuLevel.First);

var menuItems = ConsoleApp.Menus.MainMenuItems.MenuItems;

foreach (var item in menuItems)
{
    menu.AddMenuItem(item);
}

menu.Run();