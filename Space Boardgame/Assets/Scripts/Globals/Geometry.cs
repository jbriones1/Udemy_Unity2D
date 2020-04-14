using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geometry
{
    static public Vector2 PointFromGrid(Vector2Int gridPoint)
    {
        float x = gridPoint.x;
        float y = gridPoint.y;
        return new Vector2(x, y);
    }

    static public Vector2Int GridPoint(int col, int row)
    {
        return new Vector2Int(col, row);
    }

    static public Vector2Int GridFromPoint(Vector2 position)
    {
        Vector2 point = Camera.main.ScreenToWorldPoint(position);
        int col = Mathf.RoundToInt(point.x);
        int row = Mathf.RoundToInt(point.y);
        return new Vector2Int(col, row);
    }
}
