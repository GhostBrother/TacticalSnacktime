using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlledCamera : iCameraState
{

    public CameraController cameraController { get; private set; }

    Camera camera;
    Vector3 tileSize;

    public PlayerControlledCamera(CameraController cameraController, Camera camera , Vector3 tileSize)
    {
        this.cameraController = cameraController;
        this.camera = camera;
        this.tileSize = tileSize;
    }

    public void MoveCamera(Vector3 curPosition, Vector3 desiredPosition, float cameraSpeed)
    {
        camera.gameObject.transform.position = Vector3.MoveTowards(curPosition, desiredPosition, (cameraSpeed * Time.deltaTime));
    }


    public Vector3 PanCamera(Vector3 curPosition, Vector3 desiredPosition)
    {
        if (curPosition.x > (tileSize.x * 4 + desiredPosition.x))
        {

            desiredPosition.x += tileSize.x;
        }

        if (curPosition.x < (desiredPosition.x - tileSize.x * 4))
        {
            desiredPosition.x -= tileSize.x;
        }

        if (curPosition.y > (tileSize.y * 2 + desiredPosition.y))
        {
            desiredPosition.y += tileSize.y;
        }

        if (curPosition.y < (desiredPosition.y - tileSize.y * 2))
        {
            desiredPosition.y -= tileSize.y;
        }

        return desiredPosition;
    }
}
