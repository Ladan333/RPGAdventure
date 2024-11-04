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

    public static int TotalHealthPlayer(PlayerData player)
    {
        int totalHealth = Convert.ToInt32((player.vitality * 0.8) + (player.strength * 0.4) + (player.dexterity * 0.2));
        return totalHealth;
    }

    public static int TotalHealthEnemy(int vitality, int strength, int dexterity)
    {
        int totalHealth = Convert.ToInt32((vitality * 0.8) + (strength * 0.4) + (dexterity * 0.2));
        return totalHealth;
    }
    public static void PhysicalAttackPlayer(PlayerData player, EnemyData enemy, CombatInfo info)
    {
        enemy.currentHealth = Convert.ToInt32((enemy.currentHealth - (player.strength * 0.3 + player.dexterity * 0.1)));
        info.logMessages!.Add($"Player turn {info.playerTurnCount}: {player.name} dealt {Convert.ToInt32(player.strength * 0.3 + player.dexterity * 0.1)} physical damage to {enemy.name}!\n" +
            $"{enemy.name} now has {enemy.currentHealth} HP remaining.");
        info.logID.Add(1);
    }
    public static void PhysicalAttackEnemy(PlayerData player, EnemyData enemy, CombatInfo info)
    {
        player.currentHealth = Convert.ToInt32((player.currentHealth - (enemy.strength * 0.3 + enemy.dexterity * 0.1)));
        info.logMessages!.Add($"Enemy turn {info.enemyTurnCount}: {enemy.name} dealt {Convert.ToInt32(enemy.strength * 0.3 + enemy.dexterity * 0.1)} physical damage to {player.name}!\n" +
            $"{player.name} now has {player.currentHealth} HP remaining.");
        info.logID.Add(2);
    }
    public static void RangedAttackPlayer(PlayerData player, EnemyData enemy, CombatInfo info)
    {
        enemy.currentHealth = Convert.ToInt32((enemy.currentHealth - (player.strength * 0.1 + player.dexterity * 0.3)));
        info.logMessages!.Add($"Player turn {info.playerTurnCount}: {player.name} dealt {Convert.ToInt32(player.strength * 0.1 + player.dexterity * 0.3)} ranged damage to {enemy.name}!\n" +
            $"{enemy.name} now has {enemy.currentHealth} HP remaining.");
        info.logID.Add(1);
    }
    public static void RangedAttackEnemy(PlayerData player, EnemyData enemy, CombatInfo info)
    {
        player.currentHealth = Convert.ToInt32((player.currentHealth - (enemy.strength * 0.1 + enemy.dexterity * 0.3)));
        info.logMessages!.Add($"Enemy turn {info.enemyTurnCount}: {enemy.name} dealt {Convert.ToInt32(enemy.strength * 0.1 + enemy.dexterity * 0.3)} ranged damage to {player.name}!\n" +
            $"{player.name} now has {player.currentHealth} HP remaining.");
        info.logID.Add(2);
    }
    public static void MagicAttackPlayer(PlayerData player, EnemyData enemy, CombatInfo info)
    {
        enemy.currentHealth = Convert.ToInt32((enemy.currentHealth - (player.intelligence * 0.3 + player.speed * 0.1)));
        info.logMessages!.Add($"Player turn {info.playerTurnCount}: {player.name} dealt {Convert.ToInt32(player.intelligence * 0.3 + player.speed * 0.1)} magic damage to {enemy.name}!\n" +
            $"{enemy.name} now has {enemy.currentHealth} HP remaining.");
        info.logID.Add(1);
    }
    public static void MagicAttackEnemy(PlayerData player, EnemyData enemy, CombatInfo info)
    {
        player.currentHealth = Convert.ToInt32((player.currentHealth - (enemy.intelligence * 0.3 + enemy.speed * 0.1)));
        info.logMessages!.Add($"Enemy turn {info.enemyTurnCount}: {enemy.name} dealt {Convert.ToInt32(enemy.intelligence * 0.3 + enemy.speed * 0.1)} magic damage to {player.name}!\n" +
            $"{player.name} now has {player.currentHealth} HP remaining.");
        info.logID.Add(2);
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
