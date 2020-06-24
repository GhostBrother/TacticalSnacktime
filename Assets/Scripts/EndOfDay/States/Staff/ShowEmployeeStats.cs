using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowEmployeeStats : Command
{
    StaffSetup _staffSetup { get; set; }
    PlayercontrolledCharacter _pcc { get; set; }
    public ShowEmployeeStats(StaffSetup staffSetup, PlayercontrolledCharacter pcc)
    {
        _staffSetup = staffSetup;
        _pcc = pcc;
        CommandName = _pcc.Name;
    }

    public string CommandName { get; private set; }

    public bool isUsable => true;

    public iCommandKind typeOfCommand { get; set; }

    public void execute()
    {
       // _staffSetup.ShowCharacterArt(_pcc.PawnSprite);
    }


}
