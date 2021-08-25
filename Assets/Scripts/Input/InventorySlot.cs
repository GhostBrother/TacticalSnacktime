using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventorySlot : MonoBehaviour
{
    InventoryDragable dragableInSlot;


    public bool IsSlotOccupied { get; set; }

    
    public void CheckForItemPlacement(InventoryDragable item)
    {
        dragableInSlot = item;
        dragableInSlot.transform.position = GetPositionOfSlot();
        IsSlotOccupied = true;
    }

    public Vector3 GetPositionOfSlot()
    {
        return GetComponent<Collider2D>().bounds.center;
    }

    public iCaryable GetItemInSlot()
    {
      return dragableInSlot.GetCaryableFromInventory();
    }

    public InventoryDragable GetDragable()
    {
        return dragableInSlot;
    }

    public void ClearDragableFromSlot()
    {
        if (IsSlotOccupied)
        {
            dragableInSlot.ClearItemFromSlot();
            dragableInSlot = null;
            IsSlotOccupied = false;
        }
    }
}
