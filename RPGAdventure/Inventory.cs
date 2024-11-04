using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGAdventure;
internal class Inventory
{
    public int gold { get; set; } = 0;

    public int potions { get; set; } = 0;

    public static void CheckInventory(PlayerData player)
    {
        bool inInventory = true;
        do
        {
            Console.Clear();
            Console.WriteLine($"{player.name}'s inventory:\n\n" +
                $"Gold: {player.inventory!.gold}\n" +
                $"Potions {player.inventory.potions}\n\n" +
                $"[1] Use potion (HP: {player.currentHealth}/{player.maxHealth})\n" +
                $"[2] Exit");

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1: UsePotion(player); break;
                case ConsoleKey.D2: inInventory = false; break;
            }
        } while (inInventory);

    }

    static void UsePotion(PlayerData player)
    {
        if (player.inventory!.potions > 0)
        {
            player.inventory!.potions--;
            player.currentHealth += Convert.ToInt32(player.maxHealth * 0.25);
            if (player.currentHealth > player.maxHealth)
                player.currentHealth = player.maxHealth;
        }
    }
}
