using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBack : Command
{

    public List<Command> NextMenu { get; private set; }

    public List<Command> BackMenu { get; private set; }

    public bool isUsable => true;

    public string CommandName => "Back";

    public iCommandKind typeOfCommand { get;  set; }

    public MenuBack(List<Command> commands)
    {
        NextMenu = commands;
        typeOfCommand = new TransferMenuCommand(commands);
    }

    public void execute()
    {
        typeOfCommand.LoadNewMenu.Invoke(NextMenu);
    }
}
