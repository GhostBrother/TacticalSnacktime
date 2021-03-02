using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventorySlot : MonoBehaviour
{
    InventoryDragable dragableInSlot;

    


    public void CheckForItemPlacement(InventoryDragable item)
    {
        dragableInSlot = item;
        dragableInSlot.transform.position = GetComponent<Collider2D>().bounds.center;
    }
}
