using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frozen : iCameraState
{
    public CameraController cameraController { get; private set; }
    Camera camera; 

    public Frozen(CameraController cameraController, Camera camera)
    {
        this.cameraController = cameraController;
        this.camera = camera;
    }
    public void MoveCamera(Vector3 curPosition, Vector3 desiredPosition, float cameraSpeed)
    {
       
    }

    public Vector3 PanCamera(Vector3 curPosition, Vector3 desiredPosition)
    {
        return camera.transform.position;
    }
}
