using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveItem : TrasferItemCommand
{

    public GiveItem( iCanGiveItems curentCharacter, Character recivingCharacter, int index) : base (curentCharacter, recivingCharacter, index)
    {
        if (curentCharacter is Character)
        {
            Character temp = (Character)curentCharacter;
            typeOfCommand = new HighlightTilesCommand(1, temp.TilePawnIsOn);
        }
    }

    public override string CommandName {
        get
        {
            return "Give"; 
        }
    }

    public override bool isUsable => true;
}
