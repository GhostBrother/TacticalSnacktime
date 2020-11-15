using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveItem : TrasferItemCommand
{
    Character giver;
    public GiveItem( Character curentCharacter , int index) : this (curentCharacter.TilePawnIsOn,(iCanGiveItems)curentCharacter, index) 
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

    protected override void OrganizeTrade(Tile tileToTradeWith)
    {
       
        if (tileToTradeWith.TargetableOnTile is Character)
        {
            Reciver = (Character)tileToTradeWith.TargetableOnTile;
        }
        base.OrganizeTrade(tileToTradeWith);
        isUsable = giver.cariedObjects.Count > 0;
        typeOfCommand.LoadNewMenu.Invoke(giver.LoadCommands());
        giver.characterCoaster.SetArtForFacing(giver.characterCoaster.determineFacing(giver.TilePawnIsOn, tileToTradeWith));
    }

    public override bool isUsable { get; set; }
    public override iCommandKind typeOfCommand { get ; set ; }
}
