namespace Resources
{
    /// Adding more parameters requires adding stuff in Player.cs (RESOURCE MANAGEMENT SECTION)
    /// and HUD.cs (SWITCH SECTION)

    public enum Systems { ShipHull, Scanner, Auxiliary, Weapon, Engine }
    public enum PlayerResources { Fame, Metal, Energy }


    /// Change things in item and itemdatabase

    public enum ItemType   { Weapon, Auxiliary, Consumable }
    public enum DamageType { None,   Laser,     Explosive  }

}
