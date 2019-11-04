using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : Command
{
    public string CommandName { get { return "Move"; } }
    GameManager _GM;

    public MoveCommand(GameManager gm)
    {
        _GM = gm;
    }

    public void execute()
    {
        _GM.CurentCharacter.ShowMoveRange();
        _GM.CurentCharacter.TilePawnIsOn.ChangeState(_GM.CurentCharacter.TilePawnIsOn.GetClearState());
        _GM.SetState(_GM.GetSelectedState());
    }
}
