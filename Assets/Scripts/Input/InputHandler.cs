﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class InputHandler : MonoBehaviour
{

    bool isLeftMousePressed;

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
        scanForMouseDown();
        scanForInteractable();
    }

    private void scanForMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && !isLeftMousePressed)
        {
            isLeftMousePressed = true;
        }

        else if (Input.GetMouseButtonUp(0) && isLeftMousePressed)
        {
            isLeftMousePressed = false;
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
                if (pointerEvent.selectedObject.GetComponent<InventorySlot>() != null)
                {
                    if (isLeftMousePressed)
                    {
                        pointerEvent.selectedObject.GetComponent<InventorySlot>().OnSlotClick(Input.mousePosition);
                        return;
                    }
                }

                if (pointerEvent.selectedObject.GetComponent<ActionButton>() != null)
                {
                    if (isLeftMousePressed)
                    {
                        pointerEvent.selectedObject.GetComponent<ActionButton>().ExecuteStoredCommand();
                        isLeftMousePressed = false;
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
                    Gm.CameraController.PanCamera(c.transform.position);

                    if (isLeftMousePressed)
                    {
                        Gm.ActivateTile(c.gameObject.GetComponent<Tile>());
                        isLeftMousePressed = false;
                    }

                    else if (isRightMousePressed)
                    {
                        isRightMousePressed = false;
                    }
                }
            }

        }

    }
}
