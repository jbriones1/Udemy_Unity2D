using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    // Parameters
    [SerializeField] TurnManager turnManager;
    [SerializeField] public int boardDimx;
    [SerializeField] int boardDimy;
    [SerializeField] Tile gameTile;
    
    [Header("Tile Sprites")]
    [SerializeField] Sprite mouseOverTileGreen;
    [SerializeField] Sprite mouseOverTileRed;
    [SerializeField] Sprite normalTile;

    public Dictionary<Vector2, bool> tilesStates = new Dictionary<Vector2, bool>();
    public Dictionary<GamePiece,Vector2> gamePiecesPos = new Dictionary<GamePiece, Vector2>();
    Vector2 mousePos;

    #region Awake, Start, Update, OnGUI
    void Awake()
    {
        CreateTiles();
    }

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            mousePos = Geometry.GridFromPoint(Input.mousePosition);
            if (tilesStates.ContainsKey(mousePos))
            {
                AttemptMove(mousePos);
            }
        }
        
    }
    #endregion

    #region Gameboard Creation
    private void CreateTiles()
    {
        for (int x = 0; x<boardDimx; x++)
        {
            for(int y = 0; y<boardDimy;y++)
            {
                Vector2 tileCoordinate = new Vector2(x, y);
                Tile tile = Instantiate(
                    gameTile, tileCoordinate, transform.rotation)
                    as Tile;
                tile.transform.parent = transform;
                //add to dictionary
                tilesStates.Add(tileCoordinate, true);
            }
        }
    }
    #endregion

    #region Movement
    private void AttemptMove(Vector2 destination)
    {
        // checks if spot is empty and is on the game board
        if (tilesStates[destination] && tilesStates.ContainsKey(destination))
        {
            Move(destination);
        }
        else
        {
            Debug.Log("Occupied");
        }
    }

    private void Move(Vector2 destination)
    {
        Player currentActivePlayer = turnManager.GetActivePlayer();
        Vector2 currentActivePlayerPos = currentActivePlayer.transform.position;

        // Before moving
        tilesStates[currentActivePlayerPos] = true;

        // After moving
        currentActivePlayer.transform.position = destination;
        currentActivePlayer.GetComponent<GamePiece>().TileUpdate(); // updates dict entry
    }
    #endregion





    #region OLD CODE
    // convert to grid position, now using Geometry.cs
    //private Vector2 SnapToGrid(Vector2 rawWorldPos)
    //{
    //    float newX = Mathf.RoundToInt(rawWorldPos.x);
    //    float newY = Mathf.RoundToInt(rawWorldPos.y);
    //    return new Vector2(newX, newY);
    //}

    //public Vector2Int GetGridClicked()
    //{
    //    Vector2 clickPos = Input.mousePosition;
    //    Vector2Int gridPos = Geometry.GridFromPoint(clickPos); // converts to integer grid points
    //    return gridPos;
    //}

    //public bool[,] tilesStatesArray = new bool[9,5]; // array that states whether a spot is empty or not

    //void SetColliderSize()
    //{
    //    this.GetComponent<BoxCollider2D>().size   = new Vector2(boardDimx, boardDimy);
    //    this.GetComponent<BoxCollider2D>().offset = new Vector2((boardDimx*0.5f)-0.5f, (boardDimy*0.5f)-0.5f);
    //}
    #endregion
}
