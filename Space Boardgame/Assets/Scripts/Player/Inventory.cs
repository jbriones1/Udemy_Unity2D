using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Resources;

public class Inventory : MonoBehaviour
{
    // MOVE ITEM EFFECTS TO ITEM DATABASE


    private const int ICON_WIDTH = 64, ICON_HEIGHT = 64, SLOT_OFFSET = 2, X_OFFSET = 550, Y_OFFSET = 790,
    TOOLTIP_WIDTH = 200, TOOLTIP_HEIGHT = 150, MOUSE_TOOLTIP_OFFSET = 20,
    PLAYER_RIGHT_OFFSET = 600, PLAYER_DOWN_OFFSET = 145;

    // References
    Player player;
    int playerNumber;
    private ItemDatabase itemDatabase;

    Rect slotRect;
    public GUISkin skin;

    private bool showTooltip;
    private string tooltip;

    
    private bool draggingItem;
    private Item draggedItem;
    private int prevIndex;


    // Inventory
    public int slotsX, slotsY; 
    public List< Item > inventory = new List< Item >();
    public List< Item > slots     = new List< Item >();


    void Awake()
    {
        player = GetComponentInParent< Player >();
        itemDatabase = FindObjectOfType<ItemDatabase>().GetComponent<ItemDatabase>();
    }

    void Start()
    {
        // Set player owner
        playerNumber = player.GetPlayerNumber();

        CreateSlots();

        AddItem(2);
        AddItem(2);
        AddItem(100);
        AddItem(101);
        AddItem(2);

    }

    void Update()
    {
    }

    void OnGUI()
    {
        // GUI Settings
        GUI.depth = 0;

        DrawInventory();
        ShowTooltip();
        ItemDragging();
    }


    void ShowTooltip()
    {
        if (showTooltip)
        {
            GUI.Box(new Rect(Event.current.mousePosition.x + MOUSE_TOOLTIP_OFFSET, Event.current.mousePosition.y - TOOLTIP_HEIGHT, TOOLTIP_WIDTH, TOOLTIP_HEIGHT), tooltip, skin.GetStyle("Tooltip"));
        }
        tooltip = ""; // Sets tooltip to nothing while not mousing over an item
    }

    void ItemDragging()
    {
        if (draggingItem)
        {
            GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, ICON_WIDTH, ICON_HEIGHT), draggedItem.itemIcon);
        }
    }

    // Slot creation
    void CreateSlots()
    {
        for (int i = 0; i < (slotsX * slotsY); i++)
        {
            slots.Add(new Item());
            inventory.Add(new Item());
        }
    }

    void DrawInventory()
    {
        Event e = Event.current;
        int i = 0;
        for (int y = 0; y < slotsY; y++)
        {
            for (int x = 0; x < slotsX; x++)
            {
                DrawPlayerInventory(x,y,playerNumber);

                slots[i] = inventory[i];

                // Check if there's an item in slot and draws an item icon
                if (slots[i].itemName != null)
                {
                    GUI.DrawTexture(slotRect, slots[i].itemIcon);

                    // Mouseover tooltip
                    if (slotRect.Contains(e.mousePosition))
                    {
                        showTooltip = true;
                        tooltip = CreateTooltip(slots[i]);

                        // Picking up and moving around items
                        if (e.button == 0 && e.type == EventType.MouseDrag && !draggingItem && player.activePlayer)
                        {
                            // Fills the slot with an empty slot
                            draggingItem = true;
                            prevIndex = i; // Saves the current slot the item was dragged out of
                            draggedItem = slots[i]; // Dragged item becomes the item from the current slot
                            inventory[i] = new Item(); // Slot becomes an empty slot
                            showTooltip = false; // Don't show tooltip while dragging item
                        }

                        // Swap items with each other
                        if (e.type == EventType.MouseUp && draggingItem && player.activePlayer)
                        {
                            inventory[prevIndex] = inventory[i]; // puts the swapped item into the previous slot
                            inventory[i] = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                        }

                        // Right-clicking item
                        if (e.isMouse && e.type == EventType.MouseUp && e.button == 1 && player.activePlayer)
                        {
                            // Consumable
                            if (slots[i].itemType == ItemType.Consumable)
                                UseConsumable(slots[i], i, true);

                            // Weapon Equipping
                            if (slots[i].itemType == ItemType.Weapon && !slots[i].equipped)
                                EquipWeapon(slots[i], i, slots[i].itemDamageType, slots[i].itemPower);
                            else if (slots[i].itemType == ItemType.Weapon && slots[i].equipped)
                                UnequipWeapon(slots[i], i, slots[i].itemDamageType, slots[i].itemPower);

                            // Auxiliary Equipping
                            //if (slots[i].itemType == ItemType.Auxiliary && slots[i].equipped)
                            //    EquipAuxli
                        }
                    }
                }
                else
                {
                    if (slotRect.Contains(e.mousePosition))
                    {
                        if (e.type == EventType.MouseUp && draggingItem)
                        {
                            inventory[i] = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                        }
                    }
                }

                if (tooltip == "") 
                {
                    showTooltip = false;
                }
                i++;
                //default: break;
            }
        }
    }

    string CreateTooltip(Item item)
    {
        tooltip = "<b>"+item.itemName+"</b>\n\n" + item.itemDescription;
        return tooltip;
    }

    void DrawPlayerInventory(int x, int y, int playerNumber)
    {
        switch (playerNumber)
        {
            case 1:
                slotRect = new Rect(X_OFFSET + (x * (ICON_WIDTH + SLOT_OFFSET)), Y_OFFSET + (y * (ICON_HEIGHT + SLOT_OFFSET)), ICON_WIDTH, ICON_HEIGHT); // TOP LEFT INVENTORY
                GUI.Box(slotRect, "", skin.GetStyle("ItemSlotBackground"));
                break;
            case 2:
                slotRect = new Rect(X_OFFSET + (x * (ICON_WIDTH + SLOT_OFFSET)) + PLAYER_RIGHT_OFFSET, Y_OFFSET + (y * (ICON_HEIGHT + SLOT_OFFSET)), ICON_WIDTH, ICON_HEIGHT); // TOP RIGHT
                GUI.Box(slotRect, "", skin.GetStyle("ItemSlotBackground"));
                break;
            case 3:
                slotRect = new Rect(X_OFFSET + (x * (ICON_WIDTH + SLOT_OFFSET)) , Y_OFFSET + (y * (ICON_HEIGHT + SLOT_OFFSET)) + PLAYER_DOWN_OFFSET, ICON_WIDTH, ICON_HEIGHT);
                GUI.Box(slotRect, "", skin.GetStyle("ItemSlotBackground"));
                break;
            case 4:
                slotRect = new Rect(X_OFFSET + (x * (ICON_WIDTH + SLOT_OFFSET)) + PLAYER_RIGHT_OFFSET, Y_OFFSET + (y * (ICON_HEIGHT + SLOT_OFFSET)) + PLAYER_DOWN_OFFSET, ICON_WIDTH, ICON_HEIGHT);
                GUI.Box(slotRect, "", skin.GetStyle("ItemSlotBackground"));
                break;
        }
    }

    #region Item Manipulation
    void AddItem(int id)
    {
        // i is used to find missing inventory slot, j is used to find the correct item in the database, 
        // find the item with the correct id and then set the inventory slot to that item
        for(int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].itemName == null)
            {
                for(int j = 0; j < itemDatabase.items.Count; j++)
                {
                    if(itemDatabase.items[j].itemID == id)
                    {
                        inventory[i] = itemDatabase.items[j];
                    }
                }
                break;
            }
        }
    }

    void RemoveItem(int id)
    {
        for(int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].itemID == id)
            {
                inventory[i].equipped = false;
                inventory[i] = new Item();
                break;
            }
        }
    }

    void UseConsumable(Item item, int slot, bool deleteItem)
    {
        switch (item.itemID)
        {
            case 0: // Repair Kit
                    player.AddSystemLevel(Systems.ShipHull, 1);
                    break;
            case 1: // Fusion Cell
                    player.AddPlayerResources(PlayerResources.Energy, 1);
                    break;
            default: break;
        }
        if (deleteItem)
        {
            inventory[slot] = new Item();
        }
    }

    void EquipWeapon(Item item, int slot, DamageType damageType, int power)
    {
        int equipLevel = 1 + (power - 1) * 3; // Power 1 = W1, Power 2 = W4, Power 3 = W7
        if (player.systemLevels[Systems.Weapon] >= equipLevel)
        {
            switch(item.itemID)
            {
                case (100): // Laser1
                    player.AddDamage(damageType, power);
                    print("Equipped" + item.itemName);
                    print(player.damage[damageType]);
                    break;
                case (101): // Laser2
                    player.AddDamage(damageType, power);
                    print("Equipped" + item.itemName);
                    print(player.damage[damageType]);
                    break;
                default:break;
            }

            item.equipped = true;
        }
        else
        {
            print("Cannot equip");
        }
    }

    void UnequipWeapon(Item item, int slot, DamageType damageType, int power)
    {
        int equipLevel = 1 + (power - 1) * 3; // Power 1 = W1, Power 2 = W4, Power 3 = W7
        if (player.systemLevels[Systems.Weapon] >= equipLevel)
        {
            switch (item.itemID)
            {
                case (100): // Laser1
                    player.AddDamage(damageType, -power);
                    print("Unequipped" + item.itemName);
                    print(player.damage[damageType]);
                    break;
                case (101): // Laser2
                    player.AddDamage(damageType, -power);
                    print("Unequipped" + item.itemName);
                    print(player.damage[damageType]);
                    break;
                default: break;
            }

            item.equipped = false;
        }
    }

    #endregion

    #region Inventory Checking
    bool InventoryContains(int id)
    {
        bool result = false;
        for(int i = 0; i < inventory.Count; i++)
        {
            result = inventory[i].itemID == id;
            if (result)
            {
                break;
            }
        }
        return result;
    }
    #endregion


}
