using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class GamePiece : MonoBehaviour
{
    [SerializeField] GameBoard gameBoard;

    #region Awake, Start, Update, OnGUI
    void Awake()
    {
        gameBoard = FindObjectOfType<GameBoard>();
    }

    void Start()
    {
        TileUpdate();
    }
    #endregion

    #region Game State
    public Vector2 CheckPosition()
    {
        Vector2 gridPos = Camera.main.ScreenToWorldPoint(transform.position);
        Vector2 gamePiecePosition = Geometry.GridFromPoint(gridPos);
        return gamePiecePosition;
    }

    public void TileUpdate()
    {
        if (gameBoard.tilesStates.ContainsKey(transform.position))
        {
            gameBoard.tilesStates[transform.position] = false;
        }
    }
    #endregion

    #region OLD CODE
    #endregion
}
