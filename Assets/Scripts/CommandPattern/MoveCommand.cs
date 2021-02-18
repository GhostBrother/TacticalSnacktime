﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : Command
{
    public string CommandName { get { return "Move"; } }

    public bool isUsable => true;

    public iCommandKind typeOfCommand { get; set; }

    Character _character;

    public MoveCommand(Character c)
    {
        _character = c;
        typeOfCommand = new HighlightTilesCommand(_character._MoveRemaining, _character.TilePawnIsOn, onClick, EnumHolder.EntityType.Clear);
    }

    public void execute()
    {
        _character.TilePawnIsOn.EntityTypeOnTile = EnumHolder.EntityType.Clear;
        typeOfCommand.ActivateType();
    }

    void onClick(Tile tile)
    {
        typeOfCommand.UndoType();
        _character._MoveRemaining -= Mathf.Abs(tile.GridX - _character.TilePawnIsOn.GridX);
        _character._MoveRemaining -= Mathf.Abs(tile.GridY - _character.TilePawnIsOn.GridY);
        PathRequestManager.RequestPath(_character.TilePawnIsOn, tile , _character.characterCoaster.MoveAlongPath);       
        _character.TilePawnIsOn = tile;      
    }

}
