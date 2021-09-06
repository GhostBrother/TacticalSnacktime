using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeMenuPanelGUI : MonoBehaviour
{
    iContainCaryables InventoryOwner;
    [SerializeField]
    Image TraderImage;
    [SerializeField]
    List<InventoryDragable> itemsToDisplay;
    [SerializeField]
    List<InventorySlot> Inventories;


    public void OpenTradeMenu(iContainCaryables inventoryOwner)
    {
        InventoryOwner = inventoryOwner;
        this.gameObject.SetActive(true);
        if (inventoryOwner is AbstractInteractablePawn)
        {
            AbstractInteractablePawn temp = (AbstractInteractablePawn)inventoryOwner;
            TraderImage.sprite = temp.characterArt;
        }

        foreach (InventoryDragable item in itemsToDisplay)
        {
            item.ClearItemFromSlot();
        }

        foreach (InventorySlot inventorySlot in Inventories)
        {
            inventorySlot.ClearDragableFromSlot();
        }

       
        for (int i = 0; i < inventoryOwner.cariedObjects.Count; i++)
        {
            itemsToDisplay[i].SetCaryable(inventoryOwner.cariedObjects[i]);
            itemsToDisplay[i].transform.position = Inventories[i].transform.position;
            Inventories[i].CheckForItemPlacement(itemsToDisplay[i]);
        }

    }

    public void CloseTradeMenu()
    {   
        ConfirmTrade();
        this.gameObject.SetActive(false);
    }

    void ConfirmTrade()
    {
        for (int i = 0; i < Inventories.Count; i++)
        {

            if (Inventories[i].IsSlotOccupied && InventoryOwner.cariedObjects.Count > i)
            {
                InventoryOwner.cariedObjects[i] = Inventories[i].GetItemInSlot();
            }

            if (Inventories[i].IsSlotOccupied &&  i >= InventoryOwner.cariedObjects.Count)
            {
                InventoryOwner.cariedObjects.Add(Inventories[i].GetItemInSlot());
            }

            if (!Inventories[i].IsSlotOccupied && InventoryOwner.cariedObjects.Count > i)
            {
               InventoryOwner.cariedObjects.RemoveAt(i);
            }
        }

    }
}
