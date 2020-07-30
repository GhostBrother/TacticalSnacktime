using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : TrasferItemCommand
{
    Supply _supply;
    public PickUpItem(iCanGiveItems ItemToPickUp, Character curentCharacter, int index) : base( index)
    {
        Reciver = curentCharacter;
         _supply = (Supply)ItemToPickUp;
        isUsable = _supply.NumberOfItemsInSupply > 0;
        //isUsable = ItemToPickUp.
        typeOfCommand = new HighlightTilesCommand(1, curentCharacter.TilePawnIsOn, OrganizeTrade, EnumHolder.EntityType.Supply);
    }


    public override string CommandName { get { return $"Pickup {_supply.Name}"; } }

    public override bool isUsable { get; set; }

    public override iCommandKind typeOfCommand { get; set; }

    protected override void OrganizeTrade(Tile tile)
    {
        if (tile.TargetableOnTile is Supply)
        {
            _giver = (iCanGiveItems)tile.TargetableOnTile;
        }

        base.OrganizeTrade(tile);
        typeOfCommand.LoadNewMenu(Reciver.LoadCommands());
    }
}
