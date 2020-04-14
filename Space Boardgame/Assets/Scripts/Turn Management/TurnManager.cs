using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    [SerializeField] int playerNumber;
    [SerializeField] Player[] players;

    // Game state
    public int currentPlayerTurn;
    Player currentActivePlayer;

    #region Awake, Start, Update, OnGUI
    void Awake()
    {
        CountPlayers();
        AssignPlayerNumber();
        ChooseStartPlayer();
    }
    #endregion

    #region Initialization
    private void CountPlayers()
    {
        players = FindObjectsOfType<Player>();
        playerNumber = players.Length;
    }

    private void ChooseStartPlayer()
    {
        currentPlayerTurn = UnityEngine.Random.Range(0, playerNumber);
        currentActivePlayer = players[currentPlayerTurn];
        currentActivePlayer.activePlayer = true;
    }

    private void AssignPlayerNumber()
    {
        for (int i = 0; i<players.Length;i++)
        {
            players[i].AssignPlayerNumber(i + 1);
        }
    }
    #endregion

    #region Game State
    public void NextTurn()
    {
        IncrementTurn();
        SetActivePlayer();
    }

    private void IncrementTurn()
    {
        currentPlayerTurn += 1;
        if (currentPlayerTurn > playerNumber - 1)
        {
            currentPlayerTurn = 0;
        }
        Debug.Log("Player " + (currentPlayerTurn+1) + "'s turn.");
    }

    private void SetActivePlayer()
    {
        // set active player
        foreach (Player player in players)
        {
            player.activePlayer = false;
        }
        currentActivePlayer = players[currentPlayerTurn];
        currentActivePlayer.activePlayer = true;
    }

    public Player GetActivePlayer()
    {
        return currentActivePlayer;
    }
    #endregion
}
