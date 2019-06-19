﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class InputHandler : MonoBehaviour {

    bool isMousePressed;

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
    }

    //private void scanForMouseUp()
    //{
    //    if (Input.GetMouseButtonUp(0) && isMouseHeldDown)
    //    {
    //        isMouseHeldDown = false;
    //    }
    //}

    private void scanForInteractable()
    {
        Vector2 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (EventSystem.current.IsPointerOverGameObject())
          {
            PointerEventData pointerEvent = new PointerEventData(EventSystem.current);
            pointerEvent.position = v;

           if( pointerEvent.selectedObject != null)
            {
                if(pointerEvent.selectedObject.GetComponent<ActionButton>() != null)
                {
                    pointerEvent.selectedObject.GetComponent<ActionButton>().ExecuteStoredCommand();
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

                    if (isMousePressed)
                    {
                        Gm.ActivateTile(c.gameObject.GetComponent<Tile>());
                        isMousePressed = false;
                    }
                }
            }
        }

    }

    public void NextAvailableCharacter()
    {
        Gm.NextButtonPressed();
    }

    public void PreviousAvailableCharacter()
    {
        Gm.PrevButtonPressed();
    }
}
