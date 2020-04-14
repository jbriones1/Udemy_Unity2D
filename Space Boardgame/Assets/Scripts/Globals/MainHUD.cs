using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Resources;

public class MainHUD : MonoBehaviour
{
    public GUISkin interfaceSkin;
    public GUISkin nextTurnButton;

    private const int SCOREBOARD_BAR_WIDTH = 450, INTERFACE_BAR_HEIGHT = 300; // Background

    #region Awake, Start, Update, OnGUI
    void Start()
    {

    }

    void OnGUI()
    {
        GUI.depth = 1;

        DrawScoreboardBar();
        DrawInterfaceBar();
    }
    #endregion

    #region Draw Main HUD
    // Left HUD bar
    private void DrawScoreboardBar()
    {
        GUI.skin = interfaceSkin;
        GUI.contentColor = Color.black;

        // Draw HUD Background
        // Rect(starting x, starting y, end x, end y), starts drawing at top left
        GUI.BeginGroup(new Rect(0, 0, SCOREBOARD_BAR_WIDTH, Screen.height - INTERFACE_BAR_HEIGHT));
        GUI.Box(new Rect(0, 0, SCOREBOARD_BAR_WIDTH, Screen.height - INTERFACE_BAR_HEIGHT), "Scoreboard");
        GUI.EndGroup();
    }

    // Bottom HUD bar
    private void DrawInterfaceBar()
    {
        GUI.skin = interfaceSkin;
        GUI.BeginGroup(new Rect(0, Screen.height - INTERFACE_BAR_HEIGHT, Screen.width, INTERFACE_BAR_HEIGHT));
        GUI.Box(new Rect(0, 0, Screen.width, INTERFACE_BAR_HEIGHT), "Interface");
        GUI.EndGroup();
    }
    #endregion
}
