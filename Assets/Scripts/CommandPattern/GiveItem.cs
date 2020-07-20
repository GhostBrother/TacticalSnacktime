using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveItem : TrasferItemCommand
{
    Character giver;
    public GiveItem( iCanGiveItems curentCharacter, int index) : base (index)
    {
        if (curentCharacter is Character)
        {
            _giver = curentCharacter;
             giver = (Character)_giver;
            giver.TilePawnIsOn.EntityTypeOnTile = EnumHolder.EntityType.Self;
            isUsable = giver.cariedObjects.Count > 0;
            typeOfCommand = new HighlightTilesCommand(1, giver.TilePawnIsOn, OrganizeTrade, EnumHolder.EntityType.Character);
        }
    }

    public override string CommandName {
        get
        {
            return "Give"; 
        }
    }

    protected override void OrganizeTrade(Tile tile)
    {
        if(tile.TargetableOnTile is Character)
        {
            Reciver = (Character)tile.TargetableOnTile;
        }
        giver.TilePawnIsOn.EntityTypeOnTile = EnumHolder.EntityType.Character;
        base.OrganizeTrade(tile);
        isUsable = giver.cariedObjects.Count > 0;
        typeOfCommand.LoadNewMenu(giver.LoadCommands());
    }

    public override bool isUsable { get; set; }
    public override iCommandKind typeOfCommand { get ; set ; }
}
