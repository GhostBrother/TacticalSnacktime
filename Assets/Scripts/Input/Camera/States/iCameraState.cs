using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iCameraState 
{
    Vector3 MoveCamera(Vector3 targetLocation, Vector3 desiredLocation ,Vector3 tileSize);
}
