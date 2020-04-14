using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Resources;

public class EncounterManager : MonoBehaviour
{
    [SerializeField] GameBoard gameBoard;

    int encounterNumber;
    int boardDimx;
    int boardDimy;

    public Dictionary<Vector2, bool> encounterStates = new Dictionary<Vector2, bool>();

    void Awake()
    {
        gameBoard = FindObjectOfType<GameBoard>();
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateEncounters();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateEncounters()
    {
        encounterNumber = gameBoard.tilesStates.Count;

        for (int x = 0; x < boardDimx; x++)
        {
            for (int y = 0; y < boardDimy; y++)
            {
                Vector2 tileCoordinate = new Vector2(x, y);
                encounterStates.Add(tileCoordinate, true);
            }
        }
    }
}
