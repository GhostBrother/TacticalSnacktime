using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField]
    float speed;

    [SerializeField]
    Camera mainCamera;

    [SerializeField]
    float howLongIsPan;

    private Vector3 currentLocation;
    private Vector3 defaultCameraLocation;
    private Vector3 desiredLocation;
    private Vector3 velocity;

    private bool iSFreePanModeOn;

   
    
    void Start()
    {
        defaultCameraLocation = this.gameObject.transform.position;
        desiredLocation = defaultCameraLocation;
      
    }


    private void Update()
    {
        mainCamera.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, desiredLocation, (speed * Time.deltaTime));
        if (mainCamera.gameObject.transform.position == desiredLocation ) { iSFreePanModeOn = true; }
    }

    
    public void PanToLocation(Vector3 targetLocation)
    {
        iSFreePanModeOn = false;
        desiredLocation = new Vector3(targetLocation.x, targetLocation.y, mainCamera.transform.position.z);

    }

    public void PanCamera(Vector3 targetLocation , Vector3 tileSize)
    {

        // The 3 should be some kind of variable that scales as the map changes size.
        if (iSFreePanModeOn)
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

    private void panToDefaultLocation()
    {
        PanToLocation(defaultCameraLocation);
    }
}
