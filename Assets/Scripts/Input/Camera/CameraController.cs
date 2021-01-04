using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO abstract states out to share an abstract base class
public class CameraController : MonoBehaviour {

    public Action onStopMoving;

    const int CameraZcoordinate = -2;

    [SerializeField]
    float CameraSpeed;

    [SerializeField]
    Camera mainCamera;

    iCameraState playerControlled;
    // panning, camera is moving on it's own and cannot be moved by the player. 
    iCameraState panningToCharacter;
    // Following, the camera is following a moving object, it cannot be moved by the player
    iCameraState cameraFollowPlayer;
    // Freeze, camera is not moving nor can the player move it. 
    iCameraState frozenCamera; 

    // Curent camera state
    iCameraState curCameraState; 

    Vector3 currentLocation;
    Vector3 defaultCameraLocation;
    Vector3 desiredLocation;

    // Kind of hacky as only one of the states uses this information;
    CharacterCoaster characterToFollow;
    public Vector3 positionOfCharacterToFollow { get { return new Vector3(characterToFollow.transform.position.x, characterToFollow.transform.position.y, CameraZcoordinate) ; } private set {; } }

    void Start()
    {
        defaultCameraLocation = this.gameObject.transform.position;
        desiredLocation = defaultCameraLocation;
        playerControlled = new PlayerControlledCamera(this, mainCamera);
        panningToCharacter = new PanningToCharacter(this, mainCamera);
        cameraFollowPlayer = new Following(this, mainCamera);
        frozenCamera = new Frozen(this, mainCamera);

        curCameraState = playerControlled;
    }

    public void SwitchToPlayerControlled()
    {
        curCameraState = playerControlled;
        Debug.Log("Switched To Player Controlled");
    }

    public void SwitchToPanCamera()
    {
        curCameraState = panningToCharacter;
        Debug.Log("Switched to pan camera");
    }

    public void SwitchToFollowMode()
    {
        curCameraState = cameraFollowPlayer;
        Debug.Log("Switched to follow");
    }

    public void SwitchToFrozenMode()
    {
        curCameraState = frozenCamera;
        Debug.Log("Switched to frozen");
    }


    private void Update()
    {
        curCameraState.MoveCamera(this.gameObject.transform.position, new Vector3 (desiredLocation.x,desiredLocation.y, CameraZcoordinate), CameraSpeed);
    }


    public void cameraFollowChracter(CharacterCoaster characterToFollow)
    {
        this.characterToFollow = characterToFollow;
        PanToLocation(positionOfCharacterToFollow);
    }
    
    public void PanToLocation(Vector3 targetLocation)
    {
        this.desiredLocation = new Vector3(targetLocation.x, targetLocation.y, mainCamera.transform.position.z);
    }

    public void PanCamera(Vector3 desiredPosition, Vector3 tileSize)
    {
       this.desiredLocation = curCameraState.PanCamera(mainCamera.transform.position ,desiredPosition, tileSize);
    }

}
