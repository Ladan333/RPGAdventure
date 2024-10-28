using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace RPGAdventure;
internal class PlayerData
{
    public int? seed { get; set; }
    public string? name { get; set; }
    public int? level { get; set; }
    public int? experience { get; set; }
    public int? vitality { get; set; }
    public int? strength { get; set; }
    public int? dexterity { get; set; }
    public int? intelligence { get; set; }
    public int? speed { get; set; }
    public int? gameLevel { get; set; }
    public int? enemyCount { get; set; }
    public int? itemCount { get; set; }
    public int? currentHealth { get; set; }

    public override string ToString()
    {
        return $"{seed},{name},{level},{experience},{vitality},{strength},{dexterity},{intelligence},{speed},{gameLevel},{enemyCount},{itemCount},{currentHealth}";
    }

    public static PlayerData FromString(string data)
    {
        var parts = data.Split(',');

        return new PlayerData()
        {
            seed = int.Parse(parts[0]),
            name = parts[1],
            level = int.Parse(parts[2]),
            experience = int.Parse(parts[3]),
            vitality = int.Parse(parts[4]),
            strength = int.Parse(parts[5]),
            dexterity = int.Parse(parts[6]),
            intelligence = int.Parse(parts[7]),
            speed = int.Parse(parts[8]),
            gameLevel = int.Parse(parts[9]),
            enemyCount = int.Parse(parts[10]),
            itemCount = int.Parse(parts[11]),
            currentHealth = int.Parse(parts[12]),
        };
    }
}
