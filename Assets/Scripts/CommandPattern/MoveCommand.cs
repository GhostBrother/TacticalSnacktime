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
        typeOfCommand = new HighlightTilesCommand(_character._MoveRemaining, _character.TilePawnIsOn, onClick);
    }

    public void execute()
    {
        typeOfCommand.ActivateType();
       
    }

    void onClick(Tile tile)
    {

        typeOfCommand.UndoType();

        _character._MoveRemaining -= Mathf.Abs(tile.GridX - _character.TilePawnIsOn.GridX);
        _character._MoveRemaining -= Mathf.Abs(tile.GridY - _character.TilePawnIsOn.GridY);

        _character.TilePawnIsOn.ChangeState(_character.TilePawnIsOn.GetClearState());

        _character.TilePawnIsOn = tile;
        PathRequestManager.RequestPath(tile, _character.TilePawnIsOn, _character.characterCoaster.MoveAlongPath);

        tile.ChangeState(tile.GetActiveState());

        // Clear tiles goes here? 
        _character.MoveCharacter();
    }

}
