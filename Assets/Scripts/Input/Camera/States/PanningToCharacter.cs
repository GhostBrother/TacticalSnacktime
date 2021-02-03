using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanningToCharacter : iCameraState
{
    public CameraController cameraController { get; private set; }
    Camera camera;


    public PanningToCharacter(CameraController cameraController, Camera camera)
    {
        this.cameraController = cameraController;
        this.camera = camera; 
    }

    public void MoveCamera(Vector3 curPosition, Vector3 desiredPosition, float cameraSpeed)
    {
        camera.gameObject.transform.position = Vector3.MoveTowards(curPosition, desiredPosition, (cameraSpeed * Time.deltaTime));
        if (desiredPosition == camera.gameObject.transform.position)
        {
            checkIfCameraHasStopped();
        }

    }

    void checkIfCameraHasStopped()
    {
        if (cameraController.onStopMoving != null)
        {
            cameraController.onStopMoving.Invoke();
            cameraController.onStopMoving = null;
        }
    }

    public Vector3 PanCamera(Vector3 curPosition, Vector3 desiredPosition)
    {
        return cameraController.positionOfCharacterToFollow;
    }
}
