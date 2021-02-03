using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeItem : TrasferItemCommand
{
    public TakeItem(Character curentCharacter , int index) : base ( index)
    {
        isUsable = true; 
        Reciver = curentCharacter;
        typeOfCommand = new HighlightTilesCommand(1, curentCharacter.TilePawnIsOn, OrganizeTrade, EnumHolder.EntityType.Character);
    }

    public override string CommandName { get { return "Take"; } }


    public override bool isUsable { set; get; }
    public override iCommandKind typeOfCommand { get; set; }

    protected override void OrganizeTrade(Tile tile)
    {
        if(tile.TargetableOnTile is iCanGiveItems)
        {
            _giver = (iCanGiveItems)tile.TargetableOnTile;
        }
        base.OrganizeTrade(tile);
        isUsable = _giver.cariedObjects.Count > 0;
        typeOfCommand.LoadNewMenu.Invoke(Reciver.LoadCommands());
    }
}
