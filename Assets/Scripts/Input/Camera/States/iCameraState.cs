using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iCameraState
{
    CameraController cameraController {get; }

    void MoveCamera(Vector3 curPosition, Vector3 desiredPosition, float cameraSpeed);

    Vector3 PanCamera( Vector3 curPosition, Vector3 desiredPosition);
}
