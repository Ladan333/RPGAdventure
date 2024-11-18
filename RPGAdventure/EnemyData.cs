using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RPGAdventure;
internal class EnemyData
{
    public string? name { get; set; }
    public int level { get; set; }
    public int vitality { get; set; }
    public int strength { get; set; }
    public int dexterity { get; set; }
    public int intelligence { get; set; }
    public int speed { get; set; }
    public int enemyID { get; set; }
    public int currentHealth { get; set; }

    public static EnemyData GenerateEnemy(PlayerData player, string enemyRaw)
    {
        EnemyData? enemy = null;
        string enemyName = enemyRaw;
        string filePath = $"..\\..\\..\\EnemyBases\\{enemyName}Base.json";
        string readData = File.ReadAllText(filePath!);
        enemy = JsonSerializer.Deserialize<EnemyData>(readData)!;

        int? points = 8 + player.level;
        player.enemyCount++;
        enemy.enemyID = (player.seed + player.enemyCount) ?? default(int);

        Random enemyGen = new Random(enemy.enemyID);

        do
        {
            switch (enemyGen.Next(1, 6))
            {
                case 1: enemy.vitality++; points--; break;
                case 2: enemy.strength++; points--; break;
                case 3: enemy.dexterity++; points--; break;
                case 4: enemy.intelligence++; points--; break;
                case 5: enemy.speed++; points--; break;
            }
        } while (points > 0);

        string title = EnemyTitle(enemy.vitality, enemy.strength, enemy.dexterity, enemy.intelligence, enemy.speed);
        enemy.name = $"{title} {enemy.name}";

        enemy.currentHealth = StatAbilityCalcs.TotalHealthEnemy(enemy.vitality, enemy.strength, enemy.dexterity);

        return enemy;
    }

    private static string EnemyTitle(int _vit, int _str, int _dex, int _int, int _spe)
    {
        int[] stats = new int[] { _vit, _str, _dex, _int, _spe };
        Array.Sort(stats);
        string enemyTitle = "Default";
        int highStat = stats[4];

        if (highStat == _vit)
            enemyTitle = "Bulky";
        else if (highStat == _str)
            enemyTitle = "Aggressive";
        else if (highStat == _dex)
            enemyTitle = "Nimble";
        else if (highStat == _int)
            enemyTitle = "Clever";
        else if (highStat == _spe)
            enemyTitle = "Quick";

        return enemyTitle;
    }
}
