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
        typeOfCommand = new HighlightTilesCommand(adjacent, _character.TilePawnIsOn, chooseFacing, EnumHolder.EntityType.None);
    }

    public string CommandName { get { return "Wait"; } }

    public bool isUsable => true;

   public iCommandKind typeOfCommand { get; set; }

    void chooseFacing(Tile tileInDirectionToFace)
    {
        _character.characterCoaster.SetArtForFacing(_character.characterCoaster.determineFacing(_character.TilePawnIsOn, tileInDirectionToFace));
        typeOfCommand.UndoType();
        _character.onTurnEnd.Invoke();
    }

    public void execute()
    {
        
    }

}
