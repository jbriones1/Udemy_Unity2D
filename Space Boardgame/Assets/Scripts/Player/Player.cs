using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Resources;
using System;

public class Player : MonoBehaviour
{
    [Header("Objects Needed")]
    [SerializeField] TurnManager turnManager;
    [SerializeField] GameBoard gameBoard;
    [SerializeField] HUD hud;

    // personalization
    [Header("Personalization")]
    public string username;
    public Texture2D avatar;

    // game state
    [Header("Player Settings")]
    public bool activePlayer;
    public bool inMatch;
    public int playerNumber;
    public int auxiliarySlots;

    // Game Resources
    public  int systemLevelStart, systemLevelLimit, metalStart;
    public  Dictionary<Systems, int> systemLevels, systemLimits;
    private Dictionary<PlayerResources, int> playerResources;
    public  Dictionary<DamageType, int> damage;

    #region Awake, Start, Update, OnGUI
    void Awake()
    {
        // Add objects
        turnManager      = FindObjectOfType<TurnManager>();
        gameBoard        = FindObjectOfType<GameBoard>();
        hud              = GetComponentInChildren<HUD>();

        // Initialize all dictionaries
        systemLevels     = InitSystemLevels();
        systemLimits     = InitSystemLevels();
        playerResources  = InitPlayerResources();
        damage           = InitDamage();

        // Set all starting values for systems and resources
        systemLevelStart = 1;
        systemLevelLimit = 12;
        metalStart       = 10;
    }

    void Start()
    {
        SystemLevelLimits();
        StartingSystemLevels();
        StartingResourceLevel();
    }

    void Update()
    {
        hud.SetSystemValues(systemLevels, systemLimits);
        hud.SetPlayerResourceValues(playerResources);
    }
    #endregion

    #region Game State

    public void AssignPlayerNumber(int num)
    {
        playerNumber = num;
    }

    public int GetPlayerNumber()
    {
        return playerNumber;
    }
    #endregion

    #region System/Resource Managment

    #region Dictionary Initialization
    // Create dictionary of system levels
    private Dictionary<Systems, int> InitSystemLevels()
    {
        Dictionary< Systems, int > list = new Dictionary< Systems, int>();
        list.Add(Systems.ShipHull, 0);
        list.Add(Systems.Scanner, 0);
        list.Add(Systems.Auxiliary, 0);
        list.Add(Systems.Weapon, 0);
        list.Add(Systems.Engine, 0);
        return list;
    }

    private Dictionary< PlayerResources, int > InitPlayerResources()
    {
        Dictionary< PlayerResources, int > list = new Dictionary< PlayerResources, int>();
        list.Add(PlayerResources.Fame, 0);
        list.Add(PlayerResources.Metal, 0);
        list.Add(PlayerResources.Energy, 0);
        return list;
    }

    private Dictionary< DamageType, int > InitDamage()
    {
        Dictionary< DamageType, int > list = new Dictionary<DamageType, int>();
        list.Add(DamageType.Explosive, 0);
        list.Add(DamageType.Laser, 0);
        return list;

    }
    #endregion

    #region Starting Values

    // Game starting values
    private void SystemLevelLimits()
    {
        IncrementSystemLimit(Systems.ShipHull,  systemLevelLimit);
        IncrementSystemLimit(Systems.Scanner,   systemLevelLimit);
        IncrementSystemLimit(Systems.Auxiliary, systemLevelLimit);
        IncrementSystemLimit(Systems.Weapon,    systemLevelLimit);
        IncrementSystemLimit(Systems.Engine,    systemLevelLimit);
    }

    private void StartingSystemLevels()
    {
        AddSystemLevel(Systems.ShipHull,  systemLevelStart);
        AddSystemLevel(Systems.Scanner,   systemLevelStart);
        AddSystemLevel(Systems.Auxiliary, systemLevelStart);
        AddSystemLevel(Systems.Weapon,    systemLevelStart);
        AddSystemLevel(Systems.Engine,    systemLevelStart);
    }

    private void StartingResourceLevel()
    {
        AddPlayerResources(PlayerResources.Metal, metalStart);
    }

    #endregion

    #region Value changing

    private void IncrementSystemLimit(Systems type, int amount)
    {
        systemLimits[type] += amount;
    }

    // System Manipulation
    public void AddSystemLevel(Systems type, int amount)
    {
        if (systemLevels[type] + amount > systemLimits[type])
            systemLevels[type] += systemLimits[type] - systemLevels[type];
        else
            systemLevels[type] += amount;
    }

    // Resource Manipulation
    public void AddPlayerResources(PlayerResources type, int amount)
    {
        playerResources[type] += amount;
    }

    // Damage Manipulation
    public void AddDamage(DamageType type, int amount)
    {
        damage[type] += amount;
    }

    #endregion

    #endregion
}
