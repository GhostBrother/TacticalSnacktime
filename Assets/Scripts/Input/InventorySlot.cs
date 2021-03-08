using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventorySlot : MonoBehaviour
{
    InventoryDragable dragableInSlot;

    
    public void CheckForItemPlacement(InventoryDragable item)
    {
        dragableInSlot = item;
        dragableInSlot.transform.position = GetPositionOfSlot();
    }

    public Vector3 GetPositionOfSlot()
    {
        return GetComponent<Collider2D>().bounds.center;
    }
}
