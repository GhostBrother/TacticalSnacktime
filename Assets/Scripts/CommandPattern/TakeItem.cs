using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeItem : TrasferItemCommand
{
    public TakeItem(iCanGiveItems givingCharacter, Character curentCharacter) : base (givingCharacter , curentCharacter)
    {
        characterName = string.Empty;
        if (givingCharacter is AbstractInteractablePawn)
        {
            AbstractInteractablePawn temp = (AbstractInteractablePawn)givingCharacter;
            characterName = temp.PawnSprite.ToString();
            itemName = givingCharacter.Give().Name;
        }
           
    }

    public override string CommandName { get { return $"Take {itemName} From {characterName}"; } }
}
