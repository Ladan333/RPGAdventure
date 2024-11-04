using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGAdventure;
internal class Shop
{
    public int hpPotions { get; set; } = 0;
    public int vitPotions { get; set; } = 0;
    public int strPotions { get; set; } = 0;
    public int dexPotions { get; set; } = 0;
    public int intPotions { get; set; } = 0;
    public int spePotions { get; set; } = 0;
    public static void ShopUI(PlayerData player)
    {
        bool inShop = true;
        Shop shop = new Shop();
        GenerateStock(shop);
        do
        {

            Console.Clear();
            Console.WriteLine($"Welcome to the shop!\n\n" +
            $"Inventory:\n" +
            $"Gold: {player.inventory!.gold}\n" +
            $"Potions {player.inventory.potions}\n\n" +
            $"Stock:\n" +
            $"[1] Health potions: {shop.hpPotions} (5g)\n" +
            $"[2] Iron potions: {shop.vitPotions} (8g) (+VIT)\n" +
            $"[3] Rage potions: {shop.strPotions} (8g) (+STR)\n" +
            $"[4] Reflex potions: {shop.dexPotions} (8g) (+DEX)\n" +
            $"[5] Focus potions: {shop.intPotions} (8g) (+INT)\n" +
            $"[6] Nimble potions: {shop.spePotions} (8g) (+SPE)\n\n" +
            $"[7] Exit shop");

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                    if (player.inventory.gold >= 5 && shop.hpPotions > 0)
                    {
                        player.inventory.gold -= 5; shop.hpPotions--; player.inventory.potions++; break;
                    }
                    else
                    {
                        break;
                    }
                case ConsoleKey.D2:
                    if (player.inventory.gold >= 8 && shop.vitPotions > 0)
                    {
                        player.inventory.gold -= 8; player.vitality++; shop.vitPotions--; break;
                    }
                    else
                    {
                        break;
                    }
                case ConsoleKey.D3:
                    if (player.inventory.gold >= 8 && shop.strPotions > 0)
                    {
                        player.inventory.gold -= 8; player.strength++; shop.strPotions--; break;
                    }
                    else
                    {
                        break;
                    }
                case ConsoleKey.D4:
                    if (player.inventory.gold >= 8 && shop.dexPotions > 0)
                    {
                        player.inventory.gold -= 8; player.dexterity++; shop.dexPotions--; break;
                    }
                    else
                    {
                        break;
                    }
                case ConsoleKey.D5:
                    if (player.inventory.gold >= 8 && shop.intPotions > 0)
                    {
                        player.inventory.gold -= 8; player.intelligence++; shop.intPotions--; break;
                    }
                    else
                    {
                        break;
                    }
                case ConsoleKey.D6:
                    if (player.inventory.gold >= 8 && shop.spePotions > 0)
                    {
                        player.inventory.gold -= 8; player.speed++; shop.spePotions--; break;
                    }
                    else
                    {
                        break;
                    }
                case ConsoleKey.D7: inShop = false; break;
            }
        } while (inShop);
    }

    private static void GenerateStock(Shop shop)
    {
        int maxStock = 15;
        Random rng = new Random();
        do
        {
            switch (rng.Next(1, 7))
            {
                case 1: shop.hpPotions++; maxStock--; break;
                case 2: shop.vitPotions++; maxStock--; break;
                case 3: shop.strPotions++; maxStock--; break;
                case 4: shop.dexPotions++; maxStock--; break;
                case 5: shop.intPotions++; maxStock--; break;
                case 6: shop.spePotions++; maxStock--; break;
            }
        } while (maxStock > 0);
    }
}
