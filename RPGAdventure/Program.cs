using System.Runtime.InteropServices;

namespace RPGAdventure;

internal class Program
{
    static void Main(string[] args)
    {
        PlayerData gameData = new PlayerData();
        gameData = MainMenu();
        GameLogic.InGameMenu(gameData);
    }

    static PlayerData MainMenu()
    {
        PlayerData gameData = new PlayerData();

        Console.Clear();
        Console.WriteLine($"[1] New game");
        Console.WriteLine($"[2] Load game");
        Console.WriteLine($"[3] Themes");
        Console.WriteLine($"[4] Exit");

        switch (Console.ReadKey().Key)
        {
            case ConsoleKey.D1: gameData = GameLogic.NewGameStart(); break;
            case ConsoleKey.D2: gameData = GameLogic.LoadGame(); break;
            default: break;
        }

        return gameData;
    }
}
