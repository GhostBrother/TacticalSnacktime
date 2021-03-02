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

    public void OpenTradeMenu(iContainCaryables inventoryOwner)
    {
        this.gameObject.SetActive(true);
        InventoryOwner = inventoryOwner;
        for (int i = 0; i < itemsToDisplay.Count; i++)
        {
            itemsToDisplay[i].ClearItemFromSlot();
            if(inventoryOwner.cariedObjects.Count > i)
            itemsToDisplay[i].SetCaryable(inventoryOwner.cariedObjects[i]);
        }
    }

    public void ConfirmTrade()
    {
        for(int i = 0; i < InventoryOwner.cariedObjects.Count; i++)
        {
            InventoryOwner.cariedObjects[i] = itemsToDisplay[i].GetCaryableFromInventory();
            itemsToDisplay[i].ClearItemFromSlot();
        }
    }

    public void CloseTradeMenu()
    {
        this.gameObject.SetActive(false);
    }
}
