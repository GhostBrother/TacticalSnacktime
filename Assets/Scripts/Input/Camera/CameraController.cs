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
        //defaultCameraLocation = this.gameObject.transform.position;
        //desiredLocation = defaultCameraLocation;
        //playerControlled = new PlayerControlledCamera(this, mainCamera);
        //panningToCharacter = new PanningToCharacter(this, mainCamera);
        //cameraFollowPlayer = new Following(this, mainCamera);
        //frozenCamera = new Frozen(this, mainCamera);

        // Tile size is a bit of a bother, perhapse nay perchance can we 
        // put that in here as some sort of init?
    }

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
        PanCamera(positionOfCharacterToFollow);
    }
   
    public void PanCamera(Vector3 desiredPosition)
    {
       this.desiredLocation = curCameraState.PanCamera(mainCamera.transform.position ,desiredPosition);
    }

}
