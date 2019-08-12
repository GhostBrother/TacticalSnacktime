using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveItem : TrasferItemCommand
{

    public GiveItem( iCanGiveItems curentCharacter, Character recivingCharacter) : base (curentCharacter, recivingCharacter)
    {
        characterName = recivingCharacter.PawnSprite.ToString();
    }

    public override string CommandName { get { return $"Give {itemName} to {characterName}"; } }
}
