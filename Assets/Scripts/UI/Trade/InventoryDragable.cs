using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDragable : MonoBehaviour
{
    iCaryable itemInSlot;
    [SerializeField]
    Image itemImage;

    // Hack, circular dependancy. 
    InventorySlot previousSlot;

    Vector3 LastInventorySlotPosition;

    // Set Caryable
    public void SetCaryable(iCaryable heldItems)
    {
        // gameObject.SetActive(true);
        this.GetComponent<Image>().enabled = true;
        itemInSlot = heldItems;
        SetCaryableImage(itemInSlot);
        LastInventorySlotPosition = transform.position;
    }

    void SetCaryableImage(iCaryable heldItem)
    {
        if (heldItem != null)
        {
            itemImage.sprite = heldItem.CaryableObjectSprite;

        }
    }

    public iCaryable GetCaryableFromInventory()
    {
        if (itemInSlot != null)
            return itemInSlot;

        else
            return itemInSlot;
    }

    public void ClearItemFromSlot()
    {
        this.GetComponent<Image>().enabled = false;
       // itemInSlot = null;
        itemImage.sprite = null;
    }

    public void OnSlotClick(Vector3 pointerLocation)
    {
        transform.position = pointerLocation;
        Collider2D[] collider2Ds = new Collider2D[10];
        gameObject.GetComponent<Collider2D>().OverlapCollider(new ContactFilter2D(), collider2Ds);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider != null && collider.gameObject.GetComponent<InventorySlot>() != null)
            {
                previousSlot = collider.gameObject.GetComponent<InventorySlot>();
                previousSlot.IsSlotOccupied = false;
                return;
            }
        }
    }

    public void OnItemRelease()
    {
        Collider2D[] collider2Ds = new Collider2D[10]; 
        gameObject.GetComponent<Collider2D>().OverlapCollider(new ContactFilter2D(), collider2Ds);
        foreach (Collider2D collider in collider2Ds)
        {
            if(collider != null && collider.gameObject.GetComponent<InventorySlot>() != null)
            {
                InventorySlot Slot = collider.gameObject.GetComponent<InventorySlot>();
                Slot.CheckForItemPlacement(this);
                LastInventorySlotPosition = Slot.GetPositionOfSlot();
                return;
            }
        }
        transform.position = LastInventorySlotPosition;
    }
}
