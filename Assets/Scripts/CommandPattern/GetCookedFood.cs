using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCookedFood : TrasferItemCommand
{


    public GetCookedFood(iCanGiveItems ItemToPickUp, Character curentCharacter, int index) : base(index)
    {
        Reciver = curentCharacter;
        isUsable = ItemToPickUp.cariedObjects.Count > 0;
        //isUsable = ItemToPickUp.
        typeOfCommand = new HighlightTilesCommand(1, curentCharacter.TilePawnIsOn, OrganizeTrade, EnumHolder.EntityType.CookingStation);
    }


    public override string CommandName { get { return $"Get Cooked Food"; } }

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
