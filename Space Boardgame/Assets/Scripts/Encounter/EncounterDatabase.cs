using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Resources;

public class EncounterDatabase : MonoBehaviour
{
    // Item effects are changed within Inventory
    public List<Item> items = new List<Item>();

    void Start()
    {
        // Name, ID, Description, Item type, Damage type*, Power*, Cost (* are ignorable)
        // All item effects are stored in the Inventory class

        // Consumable (XX)
        items.Add(new Item("Repair Kit", 0, "+1 level to Ship Hull", ItemType.Consumable, 10));
        items.Add(new Item("Fusion Cell", 1, "+1 Energy this turn", ItemType.Consumable, 20));
        items.Add(new Item("Weapon Refit", 2, "+1 level to Weapons", ItemType.Consumable, 10));

        // Weapon (1XX)
        items.Add(new Item("Laser1", 100, "+1 laser damage", ItemType.Weapon, DamageType.Laser, 1, 10));
        items.Add(new Item("Laser2", 101, "+2 laser damage", ItemType.Weapon, DamageType.Laser, 2, 15));
        items.Add(new Item("Laser3", 102, "+3 laser damage", ItemType.Weapon, DamageType.Laser, 3, 10));

        // Auxiliary (2XX)
        items.Add(new Item("Shield", 200, "-1 laser damage taken from all sources", ItemType.Auxiliary, 30));
    }
}
