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
            characterName = temp.Name;
            
        }
    }
    public override string CommandName { get { return $"Pickup {characterName} from floor"; } }
}
