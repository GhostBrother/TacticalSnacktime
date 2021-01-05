using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Following : iCameraState
{
    public CameraController cameraController { get; private set; }
    Camera camera;

    public Following(CameraController cameraController, Camera camera)
    {
        this.cameraController = cameraController;
        this.camera = camera;
    }

    public void MoveCamera(Vector3 curPosition, Vector3 desiredPosition, float cameraSpeed)
    {
        camera.gameObject.transform.position = Vector3.MoveTowards(curPosition, cameraController.positionOfCharacterToFollow, (cameraSpeed * Time.deltaTime));
    }

    public Vector3 PanCamera(Vector3 curPosition, Vector3 desiredPosition)
    {
        return desiredPosition;
    }
}
