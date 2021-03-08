using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmTradeItems : Command
{
    public iCommandKind typeOfCommand { get;  set; }

    TradeMenu tradeMenu;

    public ConfirmTradeItems(TradeMenu _tradeMenu,Func<List<Command>> commands)
    {
        tradeMenu = _tradeMenu;
        typeOfCommand = new TransferMenuCommand(commands);
    }

    public bool isUsable => true;

    public string CommandName => "Confirm Trade";

    public void execute()
    {
        tradeMenu.CloseTradeGUI();
        typeOfCommand.ActivateType();
    }
}
