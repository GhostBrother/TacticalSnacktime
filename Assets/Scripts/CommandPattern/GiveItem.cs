using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveItem : TrasferItemCommand
{
    Character giver;
    public GiveItem( Character curentCharacter , int index) : this (curentCharacter.TilePawnIsOn,(iCanGiveItems)curentCharacter, index) //iCanGiveItems curentCharacter
    {

    }

    public GiveItem(Tile starTile, iCanGiveItems curentCharacter, int index) : base(index)
    {
        if (curentCharacter is Character)
        {
            _giver = (iCanGiveItems)curentCharacter;
            giver = (Character)_giver;
            isUsable = giver.cariedObjects.Count > 0;
            typeOfCommand = new HighlightTilesCommand(1, starTile, OrganizeTrade, EnumHolder.EntityType.Character);
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
       
        if (tile.TargetableOnTile is Character)
        {
            Reciver = (Character)tile.TargetableOnTile;
        }
        base.OrganizeTrade(tile);
        isUsable = giver.cariedObjects.Count > 0;
        typeOfCommand.LoadNewMenu.Invoke(giver.LoadCommands());
    }

    public override bool isUsable { get; set; }
    public override iCommandKind typeOfCommand { get ; set ; }
}
