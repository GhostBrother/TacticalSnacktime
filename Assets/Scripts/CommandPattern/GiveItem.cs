using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveItem : TrasferItemCommand
{

    public GiveItem( iCanGiveItems curentCharacter, Character recivingCharacter, int index) : base (curentCharacter, recivingCharacter, index)
    {
        characterName = recivingCharacter.Name;
    }

    public override string CommandName { get { return $"Give {itemName} to {characterName}"; } }
}
