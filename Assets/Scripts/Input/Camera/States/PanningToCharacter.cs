using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanningToCharacter : iCameraState
{
    public Vector3 MoveCamera(Vector3 targetLocation, Vector3 desiredLocation, Vector3 tileSize)
    {
       return new Vector3(targetLocation.x, targetLocation.y, 0); // mainCamera.transform.position.z
    }
}
