using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBack : Command
{

    public bool isUsable => true;

    public string CommandName => "Back";

    public iCommandKind typeOfCommand { get;  set; }

    public MenuBack(Func<List<Command>> commands)
    {
        typeOfCommand = new TransferMenuCommand(commands);
    }

    public void execute()
    {
        typeOfCommand.ActivateType();
    }
}
