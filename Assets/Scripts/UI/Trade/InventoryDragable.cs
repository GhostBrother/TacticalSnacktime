using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDragable : MonoBehaviour
{
    iCaryable itemInSlot;
    [SerializeField]
    Image itemImage;
 
    List<InventorySlot> previousSlots;

    Vector3 LastInventorySlotPosition;

    private void Start()
    {
        previousSlots = new List<InventorySlot>();
    }
    // Set Caryable
    public void SetCaryable(iCaryable heldItems)
    {

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
                previousSlots.Add(collider.gameObject.GetComponent<InventorySlot>());
                previousSlots[0].IsSlotOccupied = false;
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
                if (Slot.IsSlotOccupied && previousSlots.Count > 0 )
                {
                    Slot.GetDragable().transform.position = previousSlots[0].transform.position;
                    previousSlots[0].CheckForItemPlacement(Slot.GetDragable()); 
                }
                
                LastInventorySlotPosition = Slot.GetPositionOfSlot();
                Slot.CheckForItemPlacement(this);
                previousSlots.Clear();

                return;
            }
        }
        transform.position = LastInventorySlotPosition;
    }
}
