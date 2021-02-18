using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeItemCommand : Command
{
    public bool isUsable { get; set; }
    PlayercontrolledCharacter traderOne;
    iCanGiveItems traderTwo;
    TradeMenu tradeMenu;

    public TradeItemCommand( PlayercontrolledCharacter _TraderOne, iCanGiveItems _TraderTwo)
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
        Debug.Log("Trade Has been organized");
        //tradeMenu.OpenTradePanels(traderOne, traderTwo); 
    }
}
