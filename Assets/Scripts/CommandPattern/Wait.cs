using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wait : Command
{
    Character _character;
    const int adjacent = 1; 
    public Wait(Character character)
    {
        _character = character;
        typeOfCommand = new CloseMenuAction();
    }

    public string CommandName { get { return "Wait"; } }

    public bool isUsable => true;

   public iCommandKind typeOfCommand { get; set; }

    public void execute()
    {
        _character.onTurnEnd.Invoke();
    }

}
