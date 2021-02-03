using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractCameraState : iCameraState
{
    public CameraController cameraController => throw new System.NotImplementedException();

    public void MoveCamera(Vector3 curPosition, Vector3 desiredPosition, float cameraSpeed)
    {
        throw new System.NotImplementedException();
    }

    public Vector3 PanCamera(Vector3 curPosition, Vector3 desiredPosition)
    {
        throw new System.NotImplementedException();
    }
}
