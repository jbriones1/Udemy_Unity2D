using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextTurnButton : MonoBehaviour
{
    public void OnMouseDown()
    {
        GetComponentInParent<TurnManager>().NextTurn();
    }
}
