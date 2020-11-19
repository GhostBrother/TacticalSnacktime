using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Action onStopMoving { private get; set; }

    [SerializeField]
    float CameraSpeed;

    [SerializeField]
    Camera mainCamera;

    [SerializeField]
    float howLongIsPan;

    iCameraState _playerControlled;
    // panning, camera is moving on it's own and cannot be moved by the player. 

    // Following, the camera is following a moving object, it cannot be moved by the player

    // Freeze, camera is not moving nor can the player move it. 

    // Curent camera state

    private Vector3 currentLocation;
    private Vector3 defaultCameraLocation;
    private Vector3 desiredLocation;

    private CharacterCoaster targetCharacter;

    private bool isFreePanModeOn;

    private bool isFollowMode;


    void Start()
    {
        defaultCameraLocation = this.gameObject.transform.position;
        desiredLocation = defaultCameraLocation;
    }


    private void Update()
    {
      
        if (isFollowMode)
        {
            mainCamera.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, new Vector3 (targetCharacter.transform.position.x, targetCharacter.transform.position.y, mainCamera.transform.position.z), (CameraSpeed * Time.deltaTime));
        }

        else if (mainCamera.gameObject.transform.position == desiredLocation  && !isFreePanModeOn)
        {
            isFreePanModeOn = true;
            if (onStopMoving != null)
            {
                onStopMoving.Invoke();
            }
        }

        else
            mainCamera.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, desiredLocation, (CameraSpeed * Time.deltaTime));

    }

    public void cameraFreeMode()
    {
        isFollowMode = false;
    }

    public void cameraFollowChracter(CharacterCoaster characterToFollow)
    {
        isFreePanModeOn = false;
        isFollowMode = true;
        targetCharacter = characterToFollow;
    }
    
    public void PanToLocation(Vector3 targetLocation)
    {
        isFreePanModeOn = false;
        desiredLocation = new Vector3(targetLocation.x, targetLocation.y, mainCamera.transform.position.z);

    }

    public void PanCamera(Vector3 targetLocation , Vector3 tileSize)
    {

        if (isFreePanModeOn)
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
        }

    }

}
