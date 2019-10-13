using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurn : Command
{
    ActionMenu _actionMenu;
    public EndTurn(ActionMenu actionMenu)
    {
        _actionMenu = actionMenu;
    }

    public string CommandName { get { return "Wait"; } }

    public void execute()
    {
        _actionMenu.EndTurn();  
    }

}
