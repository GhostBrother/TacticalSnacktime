using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeItemCommand : Command
{
    public bool isUsable { get; set; }
    Character traderOne;

    public TradeItemCommand(Character _TraderOne)
    {
        traderOne = _TraderOne;
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
        if (t.TargetableOnTile is iContainCaryables)
        {
            iContainCaryables reciver = (iContainCaryables)t.TargetableOnTile;
            traderOne.onTrade(traderOne, reciver);
            typeOfCommand.UndoType();
        }
    }
}
