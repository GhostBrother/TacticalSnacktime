using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeMenu : MonoBehaviour
{
    [SerializeField]
    TradeMenuPanelGUI LeftTradePanel;
    [SerializeField]
    TradeMenuPanelGUI RightTradePanel;


    // OpenPanels
    public void OpenTradePanels(iCanGiveItems LeftCharacter, iCanGiveItems RightCharacter)
    {
        LeftTradePanel.OpenTradeMenu(LeftCharacter);
        RightTradePanel.OpenTradeMenu(RightCharacter);
    }
    // ClosePanels
    public void CloseTradePanels()
    {
        LeftTradePanel.CloseTradeMenu();
        RightTradePanel.CloseTradeMenu();
    }

    // ConfirmTrade
    public void ConfirmTrade()
    {
        LeftTradePanel.ConfirmTrade();
        RightTradePanel.ConfirmTrade();
        CloseTradePanels();
    }


}
