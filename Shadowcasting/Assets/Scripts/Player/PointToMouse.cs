using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointToMouse : MonoBehaviour
{
    Vector2 oPos;
    Vector3 mPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        Point();
    }

    // Point to mouse
    public void Point()
    {
        
        oPos = transform.position;
        mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        float angle = Utilities.AngleBetweenTwoPoints(mPos, oPos);

        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }
}
