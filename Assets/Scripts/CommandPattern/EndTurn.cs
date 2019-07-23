using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurn : Command
{
    GameManager gm;

    public EndTurn(GameManager _gm)
    {
        gm = _gm;
    }

    public string CommandName { get { return "Wait"; } }

    public void execute()
    {
        
    }

}
