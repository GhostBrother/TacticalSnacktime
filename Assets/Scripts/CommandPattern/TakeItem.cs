using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeItem : TrasferItemCommand
{
    public TakeItem(iCanGiveItems givingCharacter, Character curentCharacter , int index) : base (givingCharacter , curentCharacter, index)
    {
       // typeOfCommand = new HighlightTilesCommand(1, curentCharacter.TilePawnIsOn);        
    }

    public override string CommandName { get { return "Take"; } }

    public override bool isUsable => true;
}
