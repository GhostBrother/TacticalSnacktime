using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    iCaryable itemInSlot;
    [SerializeField]
    Image itemImage;


    // Set Caryable
    public void SetCaryable(iCaryable heldItems)
    {
        gameObject.SetActive(true);
        itemInSlot = heldItems;
        SetCaryableImage(itemInSlot);
    }

    void SetCaryableImage(iCaryable heldItem)
    {
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
}
