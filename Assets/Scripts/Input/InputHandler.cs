using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class InputHandler : MonoBehaviour {

    bool isMousePressed;

    bool isRightMousePressed;

    [SerializeField]
    TileSelector tileSelector;

    [SerializeField]
    GameManager Gm;


    private void Update()
    {
        ScanForInput();
    }

    public void ScanForInput()
    {
        scanForInteractable();
        scanForMouseDown();
    }

    private void scanForMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && !isMousePressed)
        {
            isMousePressed = true;
        }

        else if (Input.GetMouseButtonDown(1))
        {
            isRightMousePressed = true;
        }
    }


    private void scanForInteractable()
    {

        // this handles two kinds of input, I want to find a way where this only handles one kind of input or can interperet both kinds. 
        Vector2 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (EventSystem.current.IsPointerOverGameObject() )
          {
            PointerEventData pointerEvent = new PointerEventData(EventSystem.current);
            pointerEvent.position = v;

           if( pointerEvent.selectedObject != null)
            {
                if(pointerEvent.selectedObject.GetComponent<ActionButton>() != null)
                {
                    if (isMousePressed)
                    {
                        pointerEvent.selectedObject.GetComponent<ActionButton>().ExecuteStoredCommand();
                        isMousePressed = false;
                        return;
                    }
                }
            }
   
         }
      
        Collider2D[] col = Physics2D.OverlapPointAll(v);

        
        if (col.Length > 0)
        {
            foreach (Collider2D c in col)
            {
                if (c.GetComponent<Tile>() != null)
                {
                    tileSelector.MoveToPosition(c.transform.position);
                    Gm.CameraController.PanCamera(c.transform.position, c.bounds.size);

                    if (isMousePressed)
                    {
                        Gm.ActivateTile(c.gameObject.GetComponent<Tile>());
                        isMousePressed = false;
                    }

                    else if (isRightMousePressed)
                    {
                        Gm.RightClick(c.gameObject.GetComponent<Tile>());
                        isRightMousePressed = false;
                    }
                }
            }
        }

    }
}
