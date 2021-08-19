using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class TradeMenu : MonoBehaviour
{
    [SerializeField]
    TradeMenuPanelGUI LeftTradePanel;
    [SerializeField]
    TradeMenuPanelGUI RightTradePanel;


    [SerializeField]
    ActionButton TradeConfirmButton;

    public void OpenTradePanels(iContainCaryables LeftCharacter, iContainCaryables RightCharacter, Action<List<Command>> GoBackMenu)
    {
        LeftTradePanel.OpenTradeMenu(LeftCharacter);
        RightTradePanel.OpenTradeMenu(RightCharacter);
        if (LeftCharacter is PlayercontrolledCharacter)
        {
            PlayercontrolledCharacter temp = (PlayercontrolledCharacter)LeftCharacter;
            ConfirmTradeItems tradeCommand =  new ConfirmTradeItems(this, temp.LoadCommands);
            tradeCommand.typeOfCommand.LoadNewMenu = GoBackMenu;
            tradeCommand.typeOfCommand.CloseMenu = ConfirmTrade;
            TradeConfirmButton.StoredCommand = tradeCommand;
            LeftTradePanel.OpenTradeMenu(temp);
        }
        TradeConfirmButton.gameObject.SetActive(true);
        this.gameObject.SetActive(true);
    }

    void CloseTradeGUI()
    { 
        LeftTradePanel.CloseTradeMenu();
        RightTradePanel.CloseTradeMenu();
        TradeConfirmButton.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public void ConfirmTrade()
    {
        CloseTradeGUI();
    }
}
