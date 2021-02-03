using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Action onStopMoving;

    const int CameraZcoordinate = -2;

    [SerializeField]
    float CameraSpeed;

    [SerializeField]
    Camera mainCamera;

    iCameraState playerControlled;
 
    iCameraState panningToCharacter;

    iCameraState cameraFollowPlayer;

    iCameraState frozenCamera; 

    iCameraState curCameraState; 

    Vector3 currentLocation;
    Vector3 defaultCameraLocation;
    Vector3 desiredLocation;

    // Kind of hacky as only one of the states uses this information;
    public CharacterCoaster characterToFollow;

    public Vector3 positionOfCharacterToFollow { get { return new Vector3(characterToFollow.transform.position.x, characterToFollow.transform.position.y, CameraZcoordinate) ; } private set {; } }


    public void Init(Vector3 tileSize)
    {
        defaultCameraLocation = this.gameObject.transform.position;
        desiredLocation = defaultCameraLocation;
        playerControlled = new PlayerControlledCamera(this, mainCamera, tileSize);
        panningToCharacter = new PanningToCharacter(this, mainCamera);
        cameraFollowPlayer = new Following(this, mainCamera);
        frozenCamera = new Frozen(this, mainCamera);
    }

    public void SwitchToPlayerControlled()
    {
        curCameraState = playerControlled;
    }

    public void SwitchToPanCamera()
    {
        curCameraState = panningToCharacter;
    }

    public void SwitchToFollowMode()
    {
        curCameraState = cameraFollowPlayer;
    }

    public void SwitchToFrozenMode()
    {
        curCameraState = frozenCamera;
    }

    private void Update()
    {
        curCameraState.MoveCamera(this.gameObject.transform.position, new Vector3 (desiredLocation.x,desiredLocation.y, CameraZcoordinate), CameraSpeed);
    }

    public void cameraFollowChracter(CharacterCoaster characterToFollow) 
    {
        this.characterToFollow = characterToFollow;
        PanCamera(characterToFollow.transform.position);
    }
   
    public void PanCamera(Vector3 desiredPosition)
    {
        this.desiredLocation = curCameraState.PanCamera(mainCamera.transform.position ,desiredPosition);
    }

}
