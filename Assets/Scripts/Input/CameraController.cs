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

    private Vector3 defaultCameraLocation;
    private Vector3 desiredLocation;
    private Vector3 velocity;
    private const float boundary = 20;
    
    void Start()
    {
        defaultCameraLocation = this.transform.position;
        desiredLocation = defaultCameraLocation;
    }
    private void Update()
    {
        mainCamera.transform.position = Vector3.SmoothDamp(this.transform.position, desiredLocation, ref velocity, howLongIsPan);

        if(Input.mousePosition.x > Screen.width - boundary)
        {
            desiredLocation.x += speed * Time.deltaTime;
        }

        if (Input.mousePosition.x < 0 + boundary)
        {
            desiredLocation.x -= speed * Time.deltaTime;
        }

        if (Input.mousePosition.y > Screen.height - boundary)
        {
            desiredLocation.y += speed * Time.deltaTime;
        }


        if (Input.mousePosition.y < 0 + boundary)
        {
            desiredLocation.y -= speed * Time.deltaTime;
        }
    }

    public void PanToLocation(Vector3 targetLocation)
    {
        desiredLocation = new Vector3(targetLocation.x, defaultCameraLocation.y, defaultCameraLocation.z);
    }

    private void panToDefaultLocation()
    {
        PanToLocation(defaultCameraLocation);
    }
}
