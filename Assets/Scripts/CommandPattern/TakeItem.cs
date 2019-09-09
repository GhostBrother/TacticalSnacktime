using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeItem : TrasferItemCommand
{
    public TakeItem(iCanGiveItems givingCharacter, Character curentCharacter , int index) : base (givingCharacter , curentCharacter, index)
    {
        characterName = string.Empty;
        if (givingCharacter is AbstractInteractablePawn)
        {
            AbstractInteractablePawn temp = (AbstractInteractablePawn)givingCharacter;
            characterName = temp.Name;
            itemName = givingCharacter.Give(index).Name;
        }
           
    }

    public override string CommandName { get { return $"Take {itemName} From {characterName}"; } }
}
