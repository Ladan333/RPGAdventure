using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RPGAdventure;
internal class GameLogic
{
    #region Game start

    private static Random SeedGen = new Random();
    private static int SeedGenerator()
    {
        int min = (int)Math.Pow(10, 9 - 1);
        int max = (int)Math.Pow(10, 9) - 1;

        return SeedGen.Next(min, max + 1);
    }

    private static Random gameRNG = new Random(SeedGenerator());

    public static PlayerData NewGameStart()
    {
        int seed = SeedGenerator();

        Console.Clear();
        Console.WriteLine("Please enter your name:");
        string input = Console.ReadLine()!;

        string filePath1 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RandomName1.txt");
        string filePath2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RandomName2.txt");

        string[] randomName1 = File.ReadAllLines(filePath1);
        string[] randomName2 = File.ReadAllLines(filePath2);

        int namePart1 = gameRNG.Next(1, randomName1.Count() - 1);
        int namePart2 = gameRNG.Next(1, randomName2.Count() - 1);

        string name = ($"{input} {randomName1[namePart1]}-{randomName2[namePart2]}");

        int level = 1;
        int experience = 0;
        int vitality = 10;
        int strength = 10;
        int dexterity = 10;
        int intelligence = 10;
        int speed = 10;
        int points = 10;
        int gameLevel = 1;
        bool pickingStats = true;

        while (pickingStats)
        {
            Console.Clear();
            Console.WriteLine("Choose your base stats");
            Console.WriteLine($"Remaining points: {points}");
            Console.WriteLine($"[1] Vitality: {vitality}");
            Console.WriteLine($"[2] Strength: {strength}");
            Console.WriteLine($"[3] Dexterity: {dexterity}");
            Console.WriteLine($"[4] Intelligence: {intelligence}");
            Console.WriteLine($"[5] Speed: {speed}");

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1: vitality++; points--; break;
                case ConsoleKey.D2: strength++; points--; break;
                case ConsoleKey.D3: dexterity++; points--; break;
                case ConsoleKey.D4: intelligence++; points--; break;
                case ConsoleKey.D5: speed++; points--; break;
                default: break;
            }

            if (points <= 0)
            {
                Console.Clear();
                Console.WriteLine("Continue with these stats?");
                Console.WriteLine($"Vitality: {vitality}, Strength: {strength}, Dexterity: {dexterity}, Intelligence: {intelligence}, Speed: {speed}");
                Console.WriteLine();
                Console.WriteLine($"[1] Continue");
                Console.WriteLine($"[2] Reset");

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1: pickingStats = false; break;
                    case ConsoleKey.D2: vitality = 10; strength = 10; dexterity = 10; intelligence = 10; speed = 10; points = 10; break;
                }
            }

        }

        PlayerData player = new PlayerData()
        {
            seed = seed,
            name = name,
            level = level,
            experience = experience,
            vitality = vitality,
            strength = strength,
            dexterity = dexterity,
            intelligence = intelligence,
            speed = speed,
            gameLevel = gameLevel,
            enemyCount = 0,
            itemCount = 0,
            currentHealth = StatAbilityCalcs.TotalHealthPlayer(vitality, strength, dexterity),
        };

        return player;
    }

    public static PlayerData LoadGame()
    {
        PlayerData? data = null;
        string? filePath = null;

        bool picking = true;
        do
        {
            Console.Clear();
            Console.WriteLine("Select save data to load from:");
            Console.WriteLine(" ");
            Console.WriteLine("[1]");
            Console.WriteLine("[2]");
            Console.WriteLine("[3]");

            char? input = Console.ReadKey().KeyChar;
            switch (input)
            {
                case '1': filePath = ValidateFilepath(input.ToString()!); picking = false; break;
                case '2': filePath = ValidateFilepath(input.ToString()!); picking = false; break;
                case '3': filePath = ValidateFilepath(input.ToString()!); picking = false; break;
                default: break;
            }
        } while (picking);
        string readData = File.ReadAllText(filePath!);
        return data = JsonSerializer.Deserialize<PlayerData>(readData)!;
    }

    #endregion

    #region Game menu
    public static void SaveGame(PlayerData data)
    {
        string? chosenPath = null;
        bool inSaveMenu = true;

        do
        {
            Console.Clear();
            Console.WriteLine("Pick a slot to save to:");
            Console.WriteLine();
            Console.WriteLine("[1]");
            Console.WriteLine("[2]");
            Console.WriteLine("[3]");
            Console.WriteLine("Press any other key to return");

            char input = Console.ReadKey().KeyChar;

            switch (input)
            {
                case '1': chosenPath = ValidateFilepath(input.ToString()); break;
                case '2': chosenPath = ValidateFilepath(input.ToString()); break;
                case '3': chosenPath = ValidateFilepath(input.ToString()); break;
                default: inSaveMenu = false; break;
            };

            if (input == '1' || input == '2' || input == '3')
            {
                Console.Clear();
                Console.WriteLine($"Are you sure you want to overwrite this save file?\n");
                Console.WriteLine($"[1] Yes\n");
                Console.WriteLine("Press any other key to go back");

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1:
                        inSaveMenu = false;
                        string writtenData = JsonSerializer.Serialize(data);
                        File.WriteAllText(ValidateFilepath(input.ToString()), writtenData);
                        Console.Clear();
                        Console.WriteLine("Your data was saved!");
                        Console.WriteLine(" ");
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        break;
                    default: break;

                }
            }
        } while (inSaveMenu);
    }

    public static string ValidateFilepath(string selection)
    {
        if (!File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Save{selection}.json")))
             File.Create(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Save{selection}.json"));

        return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Save{selection}.json");
    }

    public static void InGameMenu(PlayerData data)
    {
        bool inMenu = true;

        do
        {
            Console.Clear();
            Console.WriteLine($"{data.name}, Level: {data.level}, XP: {data.experience}");
            Console.WriteLine("What do you want to do?");
            Console.WriteLine(" ");
            Console.WriteLine("[1] Continue");
            Console.WriteLine("[2] Stats");
            Console.WriteLine("[3] Save game");

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1: inMenu = Encounter(data); break;
                case ConsoleKey.D2: CheckStats(data); break;
                case ConsoleKey.D3: SaveGame(data); break;
            }
        } while (inMenu);
    }

    #endregion

    #region stat handling
    static void ExperienceGain(PlayerData player, EnemyData enemy)
    {
        double? xpToNextLvl = 10 * (player.level * 1.5);
        int? gainedXP = 1 * enemy.level;

        player.experience = player.experience + gainedXP;
        if (player.experience >= xpToNextLvl)
        {
            player.level++;
            player.experience = 0;
            StatGain(player);
        }
    }

    static void StatGain(PlayerData player)
    {
        player.vitality++;
        player.strength++;
        player.dexterity++;
        player.intelligence++;
        player.speed++;
        int? additionalStats = 3;


        for (int i = 3; i > 0; i--)
        {
            Console.Clear();
            Console.WriteLine($"You are now level {player.level}!");
            Console.WriteLine(" ");
            Console.WriteLine("Select what stats to upgrade:");
            Console.WriteLine();
            Console.WriteLine($"Remaining points: {additionalStats}");
            Console.WriteLine($"[1] Vitality: {player.vitality}");
            Console.WriteLine($"[2] Strength: {player.strength}");
            Console.WriteLine($"[3] Dexterity: {player.dexterity}");
            Console.WriteLine($"[4] Intelligence: {player.intelligence}");
            Console.WriteLine($"[5] Speed: {player.speed}");

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1: player.vitality++; additionalStats--; break;
                case ConsoleKey.D2: player.strength++; additionalStats--; break;
                case ConsoleKey.D3: player.dexterity++; additionalStats--; break;
                case ConsoleKey.D4: player.intelligence++; additionalStats--; break;
                case ConsoleKey.D5: player.speed++; additionalStats--; break;
                default: i++; break;
            }
        }
    }

    static void CheckStats(PlayerData player)
    {
        Console.Clear();
        Console.WriteLine($"{player.name}");
        Console.WriteLine($"Level: {player.level}");
        Console.WriteLine($"VIT: {player.vitality}");
        Console.WriteLine($"STR: {player.strength}");
        Console.WriteLine($"DEX: {player.dexterity}");
        Console.WriteLine($"INT: {player.intelligence}");
        Console.WriteLine($"SPE: {player.speed}");
        Console.WriteLine(" ");
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }

    static void CheckStats(EnemyData enemy)
    {
        Console.Clear();
        Console.WriteLine($"{enemy.name}");
        Console.WriteLine($"Level: {enemy.level}");
        Console.WriteLine($"VIT: {enemy.vitality}");
        Console.WriteLine($"STR: {enemy.strength}");
        Console.WriteLine($"DEX: {enemy.dexterity}");
        Console.WriteLine($"INT: {enemy.intelligence}");
        Console.WriteLine($"SPE: {enemy.speed}");
        Console.WriteLine(" ");
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }

    #endregion

    #region Combat logic

    static bool Encounter(PlayerData player)
    {
        bool alive = true;
        EnemyData enemy = EnemyData.GenerateEnemy(player);

        int playerTurnCount = 1;
        int enemyTurnCount = 1;
        int playerTimer = StatAbilityCalcs.SpeedCalc(player);
        int enemyTimer = StatAbilityCalcs.SpeedCalc(enemy);

        bool inBattle = true;

        do
        {
            do
            {
                playerTimer--;
                enemyTimer--;
            } while ((playerTimer != 0) && (enemyTimer != 0));

            if (playerTimer <= 0)
            {
                CombatOptions(player, enemy, playerTimer, enemyTimer, playerTurnCount, enemyTurnCount);
                playerTimer = StatAbilityCalcs.SpeedCalc(player);
                playerTurnCount++;
            }
            if (enemyTimer <= 0)
            {
                EnemyTurn(player, enemy);
                enemyTimer = StatAbilityCalcs.SpeedCalc(enemy);
                enemyTurnCount++;
            }
            if (enemy.currentHealth <= 0)
                inBattle = false;

            if (player.currentHealth <= 0)
            {
                alive = false;
                inBattle = false;
            }

            ExperienceGain(player, enemy);

        } while (inBattle);
        return alive;
    }
    static void CombatOptions(PlayerData player, EnemyData enemy, int playerTurnSpeed, int enemyTurnSpeed, int playerTurnCount, int enemyTurnCount)
    {
        bool pickingOption = true;

        do
        {
            Console.Clear();
            Console.WriteLine($"{player.name} vs {enemy.name} [Enemy ID: {enemy.enemyID}]");
            Console.WriteLine($"Player health: {player.currentHealth}. Enemy health: {enemy.currentHealth}.\n" +
                $"Player turn: {playerTurnCount}. Enemy turn: {enemyTurnCount}\n\n");
            Console.WriteLine("[1] Physical attack");
            Console.WriteLine("[2] Ranged attack");
            Console.WriteLine("[3] Magic attack");
            Console.WriteLine("[4] Abilities");
            Console.WriteLine("[5] Player stats");
            Console.WriteLine("[6] Enemy stats");

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1: StatAbilityCalcs.PhysicalAttackPlayer(player, enemy); pickingOption = false; break;
                case ConsoleKey.D2: StatAbilityCalcs.RangedAttack(player, enemy); pickingOption = false; break;
                case ConsoleKey.D3: StatAbilityCalcs.MagicAttack(player, enemy); pickingOption = false; break;
                case ConsoleKey.D4: pickingOption = false; break;
                case ConsoleKey.D5: CheckStats(player); break;
                case ConsoleKey.D6: CheckStats(enemy); break;
                default: break;
            }
        } while (pickingOption);
    }
    static void EnemyTurn(PlayerData player, EnemyData enemy)
    {
        StatAbilityCalcs.PhysicalAttackEnemy(player, enemy);
    }
    #endregion
}
