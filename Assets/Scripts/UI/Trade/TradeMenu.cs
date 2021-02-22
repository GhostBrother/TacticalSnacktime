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
    public void OpenTradePanels(iContainCaryables LeftCharacter, iContainCaryables RightCharacter)
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
