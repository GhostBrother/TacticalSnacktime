using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeItemCommand : Command
{
    public bool isUsable { get; set; }
    Character traderOne;
    iContainCaryables traderTwo;
    TradeMenu tradeMenu;

    public TradeItemCommand(Character _TraderOne, iContainCaryables _TraderTwo)
    {
        traderOne = _TraderOne;
        traderTwo = _TraderTwo;
        typeOfCommand = new HighlightTilesCommand(1, traderOne.TilePawnIsOn, OrganizeTrade, EnumHolder.EntityType.Supply | EnumHolder.EntityType.Character);
        isUsable = true;
    }

    public string CommandName { get { return "Trade"; } }
    public iCommandKind typeOfCommand { set;  get; }
   

    public void execute()
    {
        typeOfCommand.ActivateType(); 
    }

    protected void OrganizeTrade(Tile t)
    {
        traderOne.onTrade(traderOne, traderTwo);
        typeOfCommand.UndoType();
    }
}
