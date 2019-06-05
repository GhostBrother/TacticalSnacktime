using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {

    bool isMouseHeldDown;

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
        scanForMouseUp();
    }

    private void scanForMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && !isMouseHeldDown)
        {
            isMouseHeldDown = true;
        }
    }

    private void scanForMouseUp()
    {
        if (Input.GetMouseButtonUp(0) && isMouseHeldDown)
        {
            isMouseHeldDown = false;
        }
    }

    private void scanForInteractable()
    {
        Vector2 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Collider2D[] col = Physics2D.OverlapPointAll(v);


        if (col.Length > 0)
        {
            foreach (Collider2D c in col)
            {
                if (c.GetComponent<Tile>() != null)
                {
                    tileSelector.MoveToPosition(c.transform.position);

                    if(isMouseHeldDown)
                    Gm.ActivateTile(c.gameObject.GetComponent<Tile>());
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
