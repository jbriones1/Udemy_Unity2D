using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Resources;
using System;

public class UserInput : MonoBehaviour
{
    private Player player;

    void Start()
    {
        player = transform.root.GetComponent< Player >();
    }

    void Update()
    {
        //MoveCamera();
    }

    private void MoveCamera()
    {
        float xpos = Input.mousePosition.x;
        float ypos = Input.mousePosition.y;
        Vector3 movement = new Vector3(0, 0, 0);

        //horizontal camera movement
        if (xpos >= 0 && xpos < ResourceManager.ScrollWidth)
        {
            movement.x -= ResourceManager.ScrollSpeed;
        } else if(xpos <= Screen.width && xpos > Screen.width - ResourceManager.ScrollWidth)
        {
            movement.x += ResourceManager.ScrollSpeed;
        }

        //vertical camera movement
        if (ypos >= 0 && ypos < ResourceManager.ScrollWidth)
        {
            movement.y -= ResourceManager.ScrollSpeed;
        }
        else if (ypos <= Screen.width && ypos > Screen.width - ResourceManager.ScrollWidth)
        {
            movement.y += ResourceManager.ScrollSpeed;
        }

        //calculate camera position
        Vector3 origin = Camera.main.transform.position;
        Vector3 destination = origin;
        destination.x += movement.x;
        destination.y += movement.y;

        //detect change in position
        if (destination != origin)
        {
            Camera.main.transform.position = Vector3.MoveTowards(origin, destination, Time.deltaTime * ResourceManager.ScrollSpeed);
        }
    }
}
