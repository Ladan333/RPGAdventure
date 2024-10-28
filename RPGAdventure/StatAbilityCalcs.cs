using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace RPGAdventure;
internal class StatAbilityCalcs
{
    #region base stats and attacks
    public static int TotalHealthPlayer(int vitality, int strength, int dexterity)
    {
        int totalHealth = Convert.ToInt32((vitality * 0.8) + (strength * 0.4) + (dexterity * 0.2));
        return totalHealth;
    }

    public static int TotalHealthEnemy(int vitality, int strength, int dexterity)
    {
        int totalHealth = Convert.ToInt32((vitality * 0.8) + (strength * 0.4) + (dexterity * 0.2));
        return totalHealth;
    }
    public static void PhysicalAttackPlayer(PlayerData player, EnemyData enemy)
    {
        enemy.currentHealth = Convert.ToInt32((enemy.currentHealth - (player.strength * 0.3 + player.dexterity * 0.1)));
    }
    public static void PhysicalAttackEnemy(PlayerData player, EnemyData enemy)
    {
        player.currentHealth = Convert.ToInt32((player.currentHealth - (enemy.strength * 0.3 + enemy.dexterity * 0.1)));
    }
    public static void RangedAttack(PlayerData player, EnemyData enemy)
    {
    }
    public static void MagicAttack(PlayerData player, EnemyData enemy)
    {

    }

    public static int SpeedCalc(PlayerData player)
    {
        int turnSpeed = Convert.ToInt32(20 - player.speed * 0.5);
        return turnSpeed;
    }
    public static int SpeedCalc(EnemyData enemy)
    {
        int turnSpeed = Convert.ToInt32(20 - enemy.speed * 0.5);
        return turnSpeed;
    }

    #endregion
}
