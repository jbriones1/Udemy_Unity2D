using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Resources;
using System;

public class HUD : MonoBehaviour
{
    public GUISkin interfaceSkin;
    public GUISkin nextTurnButton;
    public Texture2D[] systemImages;
    public Texture2D[] playerResourceImages;

    private const int SCOREBOARD_BAR_WIDTH = 500, INTERFACE_BAR_HEIGHT = 300, // Background
        ICON_WIDTH = 32, ICON_HEIGHT = 32, TEXT_WIDTH = 128, TEXT_HEIGHT = 32; // Resources

    private Dictionary<Systems, int> systems, systemsLimits;
    private Dictionary<Systems, Texture2D> systemIcons;

    private Dictionary<PlayerResources, int> playerResources;
    private Dictionary<PlayerResources, Texture2D> playerResourceIcons;

    private Player player;
    private int playerNumber;
    private Texture2D playerAvatar;

    #region Awake, Start, Update, OnGUI
    void Awake()
    {
        player = GetComponentInParent<Player>();

    }

    void Start()
    {
        // Personalization
        playerAvatar = player.avatar;

        // Value dictionaries
        systems             = new Dictionary<Systems, int>();
        systemsLimits       = new Dictionary<Systems, int>();
        playerResources     = new Dictionary<PlayerResources, int>();

        // Icon Dictionaries
        systemIcons         = new Dictionary<Systems, Texture2D>();
        playerResourceIcons = new Dictionary<PlayerResources, Texture2D>();

        // Initialization
        InitializeDictionaryEntries();
        playerNumber = player.GetPlayerNumber();
    }

    // Active player's HUD gets darker, find fix
    void OnGUI()
    {
        GUI.depth = 0;
        DrawScoreBoardValues();
    }
    #endregion

    #region MainHUDDrawers
    private void DrawScoreBoardValues()
    {
        GUI.skin = interfaceSkin;
        GUI.contentColor = Color.black;

        // Draw HUD Background
        // Rect(starting x, starting y, end x, end y), starts drawing at top left
        GUI.BeginGroup(new Rect(0, 0, SCOREBOARD_BAR_WIDTH, Screen.height - INTERFACE_BAR_HEIGHT));

        // Draw systems and resource values
        DrawScoreValues(playerNumber);

        GUI.EndGroup();
    }
    #endregion

    #region Icon and Text Drawers
    // ICON AND TEXT DRAWERS
    private void DrawScoreValues(int playerNumber)
    {
        int textLeft, iconLeft, topPos;
        GUI.contentColor = Color.black;
        switch (playerNumber)
        {
            case 1:
                textLeft = 100; iconLeft = 50; topPos = 100; // text left offset, icon top offset, text down offset
                GUI.Label(new Rect(textLeft, topPos, TEXT_WIDTH, TEXT_HEIGHT), "Player "+playerNumber);
                GUI.DrawTexture(new Rect(iconLeft, topPos, ICON_WIDTH, ICON_HEIGHT), playerAvatar);
                topPos += TEXT_HEIGHT;
                foreach (KeyValuePair<PlayerResources, int> type in playerResources)
                {
                    DrawResourceIcon(type.Key, iconLeft, textLeft, topPos);
                    topPos += TEXT_HEIGHT;
                }
                foreach (KeyValuePair<Systems, int> type in systems)
                {
                    DrawSystemIcon(type.Key, iconLeft, textLeft, topPos);
                    topPos += TEXT_HEIGHT;
                }
                break;
            case 2:
                textLeft = 300; iconLeft = 250; topPos = 100; // text left offset, icon top offset, text down offset
                GUI.Label(new Rect(textLeft, topPos, TEXT_WIDTH, TEXT_HEIGHT), "Player " + playerNumber);
                GUI.DrawTexture(new Rect(iconLeft, topPos, ICON_WIDTH, ICON_HEIGHT), playerAvatar);
                topPos += TEXT_HEIGHT;
                foreach (KeyValuePair<PlayerResources, int> type in playerResources)
                {
                    DrawResourceIcon(type.Key, iconLeft, textLeft, topPos);
                    topPos += TEXT_HEIGHT;
                }
                foreach (KeyValuePair<Systems, int> type in systems)
                {
                    DrawSystemIcon(type.Key, iconLeft, textLeft, topPos);
                    topPos += TEXT_HEIGHT;
                }
                break;
            case 3:
                textLeft = 100; iconLeft = 50; topPos = 400; // text left offset, icon top offset, text down offset
                GUI.Label(new Rect(textLeft, topPos, TEXT_WIDTH, TEXT_HEIGHT), "Player " + playerNumber);
                GUI.DrawTexture(new Rect(iconLeft, topPos, ICON_WIDTH, ICON_HEIGHT), playerAvatar);
                topPos += TEXT_HEIGHT;
                foreach (KeyValuePair<PlayerResources, int> type in playerResources)
                {
                    DrawResourceIcon(type.Key, iconLeft, textLeft, topPos);
                    topPos += TEXT_HEIGHT;
                }
                foreach (KeyValuePair<Systems, int> type in systems)
                {
                    DrawSystemIcon(type.Key, iconLeft, textLeft, topPos);
                    topPos += TEXT_HEIGHT;
                }
                break;
            case 4:
                textLeft = 300; iconLeft = 250; topPos = 400; // text left offset, icon top offset, text down offset
                //GUI.contentColor = Color.black;
                GUI.Label(new Rect(textLeft, topPos, TEXT_WIDTH, TEXT_HEIGHT), "Player " + playerNumber);
                GUI.DrawTexture(new Rect(iconLeft, topPos, ICON_WIDTH, ICON_HEIGHT), playerAvatar);
                topPos += TEXT_HEIGHT;
                foreach (KeyValuePair<PlayerResources, int> type in playerResources)
                {
                    DrawResourceIcon(type.Key, iconLeft, textLeft, topPos);
                    topPos += TEXT_HEIGHT;
                }
                foreach (KeyValuePair<Systems, int> type in systems)
                {
                    DrawSystemIcon(type.Key, iconLeft, textLeft, topPos);
                    topPos += TEXT_HEIGHT;
                }
                break;
            default: break;
        }
    }

    private void DrawSystemIcon(Systems type, int iconLeft, int textLeft, int topPos)
    {
        // Draws value
        Texture2D icon = systemIcons[type];
        GUI.DrawTexture(new Rect(iconLeft, topPos, ICON_WIDTH, ICON_HEIGHT), icon);

        // Draws text
        string text = systems[type].ToString() + "/" + systemsLimits[type].ToString();
        GUI.Label      (new Rect(textLeft, topPos, TEXT_WIDTH, TEXT_HEIGHT), text);
    }

    private void DrawResourceIcon(PlayerResources type, int iconLeft, int textLeft, int topPos)
    {
        // Draws icon
        Texture2D icon = playerResourceIcons[type]; 
        GUI.DrawTexture(new Rect(iconLeft, topPos, ICON_WIDTH, ICON_HEIGHT), icon);

        // Draws value
        string text = playerResources[type].ToString();
        GUI.Label      (new Rect(textLeft, topPos, TEXT_WIDTH, TEXT_HEIGHT), text);
    }

    private void DrawResourceValues(int textLeft, int iconLeft, int topOffset)
    {
        GUI.contentColor = Color.black;
        topOffset += TEXT_HEIGHT;
        foreach (KeyValuePair<PlayerResources, int> type in playerResources)
        {
            DrawResourceIcon(type.Key, iconLeft, textLeft, topOffset);
            topOffset += TEXT_HEIGHT;
        }
        foreach (KeyValuePair<Systems, int> type in systems)
        {
            DrawSystemIcon(type.Key, iconLeft, textLeft, topOffset);
            topOffset += TEXT_HEIGHT;
        }
    }
    #endregion

    #region System/Resource Management
    // ########## SYSTEM/RESOURCE MANAGEMENT ##########
    public void SetSystemValues(Dictionary<Systems, int> systems, 
        Dictionary<Systems, int> systemsLimits)
    {
        this.systems = systems;
        this.systemsLimits = systemsLimits;
    }

    public void SetPlayerResourceValues(Dictionary<PlayerResources, int> playerResources)
    {
        this.playerResources = playerResources;
    }

    private void InitializeDictionaryEntries()
    {
        for (int i = 0; i < systemImages.Length; i++)
        {
            switch (systemImages[i].name)
            {
                case "ShipHull":
                    systemIcons.   Add(Systems.ShipHull, systemImages[i]);
                    systems.       Add(Systems.ShipHull, 0);
                    systemsLimits. Add(Systems.ShipHull, 0);
                    break;
                case "Scanner":
                    systemIcons.   Add(Systems.Scanner, systemImages[i]);
                    systems.       Add(Systems.Scanner, 0);
                    systemsLimits. Add(Systems.Scanner, 0);
                    break;
                case "Auxiliary":
                    systemIcons.   Add(Systems.Auxiliary, systemImages[i]);
                    systems.       Add(Systems.Auxiliary, 0);
                    systemsLimits. Add(Systems.Auxiliary, 0);
                    break;
                case "Weapon":
                    systemIcons.   Add(Systems.Weapon, systemImages[i]);
                    systems.       Add(Systems.Weapon, 0);
                    systemsLimits. Add(Systems.Weapon, 0);
                    break;
                case "Engine":
                    systemIcons.   Add(Systems.Engine, systemImages[i]);
                    systems.       Add(Systems.Engine, 0);
                    systemsLimits. Add(Systems.Engine, 0);
                    break;
                default: break;
            }
        }
        for (int i = 0; i < playerResourceImages.Length; i++)
        {
            switch (playerResourceImages[i].name)
            {
                case "Fame":
                    playerResourceIcons. Add(PlayerResources.Fame, playerResourceImages[i]);
                    playerResources.     Add(PlayerResources.Fame, 0);
                    break;
                case "Metal":
                    playerResourceIcons.Add(PlayerResources.Metal, playerResourceImages[i]);
                    playerResources.    Add(PlayerResources.Metal, 0);
                    break;
                case "Energy":
                    playerResourceIcons. Add(PlayerResources.Energy, playerResourceImages[i]);
                    playerResources.     Add(PlayerResources.Energy, 0);
                    break;
                default: break;
            }
        }
    }
    #endregion

    #region Old Code
    // SWITCH METHOD TO DRAW HUD
    //    int textLeft, iconLeft, topPos;
    //        switch (playerNumber)
    //        {
    //            case 1:
    //                textLeft = 50; iconLeft = 10; topPos = 100; // text left offset, icon top offset, text down offset
    //                GUI.contentColor = Color.black;
    //                GUI.Label(new Rect(textLeft, topPos, TEXT_WIDTH, TEXT_HEIGHT), "Player Number");
    //                topPos += TEXT_HEIGHT;
    //                foreach (KeyValuePair<PlayerResources, int> type in playerResources)
    //                {
    //                    DrawResourceIcon(type.Key, iconLeft, textLeft, topPos);
    //    topPos += TEXT_HEIGHT;
    //                }
    //                foreach (KeyValuePair<Systems, int> type in systems)
    //                {
    //                    DrawSystemIcon(type.Key, iconLeft, textLeft, topPos);
    //topPos += TEXT_HEIGHT;
    //                }
    //                break;
    //            case 2:
    //                textLeft = 200; iconLeft = 10; topPos = 100; // text left offset, icon top offset, text down offset
    //                GUI.contentColor = Color.black;
    //                GUI.Label(new Rect(textLeft, topPos, TEXT_WIDTH, TEXT_HEIGHT), "Player Number");
    //                topPos += TEXT_HEIGHT;
    //                foreach (KeyValuePair<PlayerResources, int> type in playerResources)
    //                {
    //                    DrawResourceIcon(type.Key, iconLeft, textLeft, topPos);
    //topPos += TEXT_HEIGHT;
    //                }
    //                foreach (KeyValuePair<Systems, int> type in systems)
    //                {
    //                    DrawSystemIcon(type.Key, iconLeft, textLeft, topPos);
    //topPos += TEXT_HEIGHT;
    //                }
    //                break;
    //            case 3:
    //                textLeft = 50; iconLeft = 10; topPos = 400; // text left offset, icon top offset, text down offset
    //                GUI.contentColor = Color.black;
    //                GUI.Label(new Rect(textLeft, topPos, TEXT_WIDTH, TEXT_HEIGHT), "Player Number");
    //                topPos += TEXT_HEIGHT;
    //                foreach (KeyValuePair<PlayerResources, int> type in playerResources)
    //                {
    //                    DrawResourceIcon(type.Key, iconLeft, textLeft, topPos);
    //topPos += TEXT_HEIGHT;
    //                }
    //                foreach (KeyValuePair<Systems, int> type in systems)
    //                {
    //                    DrawSystemIcon(type.Key, iconLeft, textLeft, topPos);
    //topPos += TEXT_HEIGHT;
    //                }
    //                break;
    //            case 4:
    //                textLeft = 200; iconLeft = 10; topPos = 400; // text left offset, icon top offset, text down offset
    //                GUI.contentColor = Color.black;
    //                GUI.Label(new Rect(textLeft, topPos, TEXT_WIDTH, TEXT_HEIGHT), "Player Number");
    //                topPos += TEXT_HEIGHT;
    //                foreach (KeyValuePair<PlayerResources, int> type in playerResources)
    //                {
    //                    DrawResourceIcon(type.Key, iconLeft, textLeft, topPos);
    //topPos += TEXT_HEIGHT;
    //                }
    //                foreach (KeyValuePair<Systems, int> type in systems)
    //                {
    //                    DrawSystemIcon(type.Key, iconLeft, textLeft, topPos);
    //topPos += TEXT_HEIGHT;
    //                }
    //                break;
    //            default: break;
    //        }

    // SINGLE PLAYER SCOREBOARD
    //// Player name
    //GUI.Label(new Rect(textLeft, topOffset, TEXT_WIDTH, TEXT_HEIGHT), "Player "+playerNumber);
    //topOffset += (TEXT_HEIGHT / 2);

    //// Fame and Metal
    //foreach (KeyValuePair<PlayerResources, int> type in playerResources)
    //{
    //    DrawResourceIcon(type.Key, iconLeft, textLeft, topOffset);
    //    topOffset += TEXT_HEIGHT;
    //}

    //// System levels (Ship Hull, Scanner, Auxiliary, Weapon and Engine)
    //foreach (KeyValuePair<Systems, int> type in systems)
    //{
    //    DrawSystemIcon(type.Key, iconLeft, textLeft, topOffset);
    //    topOffset += TEXT_HEIGHT;
    //}
    #endregion
}
