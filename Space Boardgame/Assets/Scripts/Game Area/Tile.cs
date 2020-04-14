using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Tile : MonoBehaviour
{
    [SerializeField] GameBoard gameBoard;
    [SerializeField] TurnManager turnManager;
    [SerializeField] Sprite mouseOverTileGreen;
    [SerializeField] Sprite mouseOverTileRed;
    [SerializeField] Sprite normalTile;

    GamePiece gamePiece;

    void Awake()
    {
        // Add objects
        gameBoard = FindObjectOfType<GameBoard>();
        turnManager = FindObjectOfType<TurnManager>();;
    }

    void Update()
    {
    }

    // ########## MOUSEOVER STUFF ##########
    void OnMouseOver()
    {
        if (gameBoard.tilesStates[transform.position])
        {
            transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite = mouseOverTileGreen;
        }
        else
        {
            transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite = mouseOverTileRed;
        }
    }

    void OnMouseExit()
    {
        transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite = normalTile;
    }

    // ############### OLD CODE ###############
    // ########## MOVEMENT ##########
    //private void OnMouseUpAsButton()
    //{
    //    AttemptMove();
    //}

    //private void AttemptMove()
    //{
    //    if (gameBoard.tilesState[transform.position]==false)
    //    {
    //        Debug.Log("Occupied");
    //    }
    //    else
    //    {
    //        Move();
    //    }
    //}

    //private void Move()
    //{
    //    turnManager.GetActivePlayer().transform.position = transform.position;

    //}

    //private bool CheckTileState(Vector2 tileSpot)
    //{
    //    return false;
    //}

    // OLD MOVEMENT V1
    //public Vector2 GetSquareClicked()
    //{
    //    Vector2 clickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    //    Vector2 worldPos = Camera.main.ScreenToWorldPoint(clickPos);
    //    Vector2 gridPos = SnapToGrid(worldPos);
    //    return gridPos;
    //}

    ////convert to grid position
    //private Vector2 SnapToGrid(Vector2 rawWorldPos)
    //{
    //    float newX = Mathf.RoundToInt(rawWorldPos.x);
    //    float newY = Mathf.RoundToInt(rawWorldPos.y);
    //    return new Vector2(newX, newY);
    //}

    // OLD MOVEMENT V2

    //private void Move()
    //{
    //    turnManager.GetActivePlayer().transform.position = transform.position;

    //}

    //private bool CheckTileState(Vector2 tileSpot)
    //{
    //    return false;
    //}

    //private void ColorTest() // REMOVE
    //{
    //    if (gameBoard.tilesStates[transform.position])
    //    {
    //        transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite = mouseOverSpriteGreen;
    //    }
    //    else if (!gameBoard.tilesStates[transform.position])
    //    {
    //        transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite = mouseOverSpriteRed;
    //    }
    //}


}
