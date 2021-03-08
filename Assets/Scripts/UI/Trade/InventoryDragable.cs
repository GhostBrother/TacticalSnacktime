using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDragable : MonoBehaviour
{
    iCaryable itemInSlot;
    [SerializeField]
    Image itemImage;

    Vector3 LastInventorySlotPosition;


    // Set Caryable
    public void SetCaryable(iCaryable heldItems)
    {
        gameObject.SetActive(true);
        itemInSlot = heldItems;
        SetCaryableImage(itemInSlot);
        LastInventorySlotPosition = transform.position;
    }

    void SetCaryableImage(iCaryable heldItem)
    {
        if(heldItem != null)
        itemImage.sprite = heldItem.CaryableObjectSprite;
    }

    public iCaryable GetCaryableFromInventory()
    {
        return itemInSlot;
    }

    public void ClearItemFromSlot()
    {
        gameObject.SetActive(false);
        itemInSlot = null;
        itemImage.sprite = null;
    }

    public void OnSlotClick(Vector3 pointerLocation)
    {
        transform.position = pointerLocation;
    }

    public void OnItemRelease()
    {
        Collider2D[] collider2Ds = new Collider2D[10]; 
        gameObject.GetComponent<Collider2D>().OverlapCollider(new ContactFilter2D(), collider2Ds);
        foreach (Collider2D collider in collider2Ds)
        {
            if(collider != null && collider.gameObject.GetComponent<InventorySlot>() != null)
            {
                collider.gameObject.GetComponent<InventorySlot>().CheckForItemPlacement(this);
                LastInventorySlotPosition = collider.gameObject.GetComponent<InventorySlot>().GetPositionOfSlot();
                return;
            }
        }

        transform.position = LastInventorySlotPosition;
    }
}
