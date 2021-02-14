using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    iCaryable itemInSlot;
    Image itemImage;

    private void Start()
    {
       itemImage = this.GetComponent<Image>();
    }

    // Set Caryable
    public void SetCaryable(iCaryable heldItems)
    {
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
        itemInSlot = null;
        itemImage.sprite = null;
    }
}
