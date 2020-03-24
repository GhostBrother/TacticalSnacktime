using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : TrasferItemCommand
{
    public PickUpItem(iCanGiveItems ItemToPickUp, Character curentCharacter, int index) : base(ItemToPickUp, curentCharacter, index)
    {
        if (ItemToPickUp is AbstractInteractablePawn)
        {
            AbstractInteractablePawn temp = (AbstractInteractablePawn)ItemToPickUp;
            itemName = temp.Name;
            
        }

        if (ItemToPickUp is iCaryable)
        {
            iCaryable temp = (iCaryable)ItemToPickUp;
            itemName = temp.NumberOfItemsInSupply + " " + temp.Name;
        }
    }
    public override string CommandName { get { return $"Pickup {itemName} from floor"; } }

    public override bool isUsable => true;
}
