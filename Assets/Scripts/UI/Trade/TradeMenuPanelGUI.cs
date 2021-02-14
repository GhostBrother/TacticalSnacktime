using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeMenuPanelGUI : MonoBehaviour
{
    iCanGiveItems InventoryOwner;
    [SerializeField]
    Image TraderImage;
    [SerializeField]
    List<InventorySlot> itemsToDisplay;

    public void OpenTradeMenu(iCanGiveItems inventoryOwner)
    {
        this.gameObject.SetActive(true);
        InventoryOwner = inventoryOwner;
        for (int i = 0; i < inventoryOwner.cariedObjects.Count; i++)
        {
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
