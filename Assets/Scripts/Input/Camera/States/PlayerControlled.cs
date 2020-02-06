using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlled : iCameraState
{
    public Vector3 MoveCamera(Vector3 targetLocation, Vector3 desiredLocation, Vector3 tileSize)
    {

        if (targetLocation.x > (tileSize.x * 4 + desiredLocation.x))
        {
            desiredLocation.x += tileSize.x;
        }

        if (targetLocation.x < (desiredLocation.x - tileSize.x * 4))
        {
            desiredLocation.x -= tileSize.x;
        }

        if (targetLocation.y > (tileSize.y * 2 + desiredLocation.y))
        {
            desiredLocation.y += tileSize.y;
        }

        if (targetLocation.y < (desiredLocation.y - tileSize.y * 2))
        {
            desiredLocation.y -= tileSize.y;
        }

        return desiredLocation;
    }


}
