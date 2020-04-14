using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Resources;

[System.Serializable]
public class Item
{
    public string itemName;
    public int itemID;
    public string itemDescription;
    public Texture2D itemIcon;

    public ItemType itemType;
    public DamageType itemDamageType;
    public int itemPower;
    public int itemCost;
    public int itemSellCost;

    public bool equipped;

    void Start()
    {
        equipped = false;
    }

    // Weapon constructor
    public Item(string name, int id, string description, ItemType type, DamageType damType, int power, int cost)
    {
        itemName = name;
        itemID = id;
        itemDescription = description;
        itemIcon = UnityEngine.Resources.Load<Texture2D>("Icons/Items/TempIcons/"+itemName);

        itemType = type;
        itemDamageType = damType;
        itemPower = power;
        itemCost = cost;
        itemSellCost = cost / 2;
    }

    // For non-weapons
    public Item(string name, int id, string description, ItemType type, int cost)
    {
        itemName = name;
        itemID = id;
        itemDescription = description;
        itemIcon = UnityEngine.Resources.Load<Texture2D>("Icons/Items/TempIcons/" + itemName);

        itemType = type;
        itemCost = cost;
        itemSellCost = Mathf.FloorToInt(cost / 2);
    }

    public Item() // for null items
    {

    }
}
