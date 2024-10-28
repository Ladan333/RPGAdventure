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
    public int level { get; set; }
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
    public Inventory? inventory { get; set; } = new Inventory();
}
