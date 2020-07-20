using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeItem : TrasferItemCommand
{
    Character _curCharacter;
    public TakeItem(iCanGiveItems givingCharacter, Character curentCharacter , int index) : base ( index)
    {
        _giver = givingCharacter;
        isUsable = _giver.cariedObjects.Count > 0;
        Reciver = curentCharacter;
        _curCharacter = curentCharacter;
        curentCharacter.TilePawnIsOn.EntityTypeOnTile = EnumHolder.EntityType.Self;
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
        _curCharacter.TilePawnIsOn.EntityTypeOnTile = EnumHolder.EntityType.Character;
        base.OrganizeTrade(tile);
        isUsable = _giver.cariedObjects.Count > 0;
        typeOfCommand.LoadNewMenu(Reciver.LoadCommands());
    }
}
