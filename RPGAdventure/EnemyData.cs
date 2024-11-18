using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace RPGAdventure;
internal class EnemyData
{
    public string? name { get; set; }
    public int level { get; set; }
    public int? vitality { get; set; }
    public int? strength { get; set; }
    public int? dexterity { get; set; }
    public int? intelligence { get; set; }
    public int? speed { get; set; }
    public int? enemyID { get; set; }
    public int? currentHealth { get; set; }

    public static EnemyData GenerateEnemy(PlayerData player)
    {
        string name = "Default";
        int level = player.level;
        int vitality = 5;
        int strength = 5;
        int dexterity = 5;
        int intelligence = 5;
        int speed = 5;
        int? points = 8 + player.level;
        player.enemyCount++;
        int enemyID = (player.seed + player.enemyCount) ?? default(int);

        Random enemyGen = new Random(enemyID);

        do
        {
            switch (enemyGen.Next(1, 6))
            {
                case 1: vitality++; points--; break;
                case 2: strength++; points--; break;
                case 3: dexterity++; points--; break;
                case 4: intelligence++; points--; break;
                case 5: speed++; points--; break;
            }
        } while (points > 0);

        int[] stats = new int[] { vitality, strength, dexterity, intelligence, speed };
        Array.Sort(stats);
        int highStat = stats[4];
        if (highStat == vitality)
            name = "Bulky goblin";
        else if (highStat == strength)
            name = "Aggressive goblin";
        else if (highStat == dexterity)
            name = "Nimble goblin";
        else if (highStat == intelligence)
            name = "Clever goblin";
        else if (highStat == speed)
            name = "Quick goblin";

        EnemyData enemy = new EnemyData()
        {
            name = name,
            level = level,
            vitality = vitality,
            strength = strength,
            dexterity = dexterity,
            intelligence = intelligence,
            speed = speed,
            enemyID = enemyID,
            currentHealth = StatAbilityCalcs.TotalHealthEnemy(vitality, strength, dexterity),
        };

        return enemy;
    }
}
