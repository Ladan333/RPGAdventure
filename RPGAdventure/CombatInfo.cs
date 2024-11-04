using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGAdventure;
internal class CombatInfo
{
    public int playerTimer { get; set; }
    public int enemyTimer { get; set; }
    public int playerTurnCount { get; set; } = 1;
    public int enemyTurnCount { get; set; } = 1;
    public List<string>? logMessages { get; set; } = [" ", " ", " ", " ", " ", " "];
    public List<int> logID { get; set; } = [0, 0, 0, 0, 0, 0];
    // 1 for player turn, 2 for enemy turn
    public bool inCombat { get; set; } = true;

    public static void CombatLog(CombatInfo info)
    {
        Console.WriteLine("-----------------------------\n");
        //ColourID(info.logID[info.logID.Count - 6]);
        //Console.WriteLine($"{info.logMessages![info.logMessages.Count - 6]}\n");
        //ColourID(info.logID[info.logID.Count - 5]);
        //Console.WriteLine($"{info.logMessages![info.logMessages.Count - 5]}\n");
        ColourID(info.logID[info.logID.Count - 4]);
        Console.WriteLine($"{info.logMessages![info.logMessages.Count - 4]}\n");
        ColourID(info.logID[info.logID.Count - 3]);
        Console.WriteLine($"{info.logMessages![info.logMessages.Count - 3]}\n");
        ColourID(info.logID[info.logID.Count - 2]);
        Console.WriteLine($"{info.logMessages![info.logMessages.Count - 2]}\n");
        ColourID(info.logID[info.logID.Count - 1]);
        Console.WriteLine($"{info.logMessages![info.logMessages.Count - 1]}\n");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("-----------------------------\n");
    }

    public static void ColourID(int ID)
    {
        switch (ID)
        {
            case 1: Console.ForegroundColor = ConsoleColor.Green; break;
            case 2: Console.ForegroundColor = ConsoleColor.Red; break;
        }
    }
}
