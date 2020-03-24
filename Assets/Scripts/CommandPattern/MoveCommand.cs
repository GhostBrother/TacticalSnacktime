using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : Command
{
    public string CommandName { get { return "Move"; } }

    public bool isUsable => _character._MoveRemaining > 0;

    public iCommandKind typeOfCommand { get; set; }

    Character _character;

    public MoveCommand(Character c)
    {
        _character = c;
        typeOfCommand = new HighlightTilesCommand(_character._MoveRemaining, _character.TilePawnIsOn);
    }

    public void execute()
    {
        typeOfCommand.ActivateType();
    }
}
