using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Act : Command//MenuTransitionCommand
{
    public string CommandName => "Action";

    // public List<Command> NextMenu { get; private set; }

    public bool isUsable => true; //typeOfCommand.NextMenu.Count > 0;

   // public List<Command> BackMenu { get; private set; }

    public iCommandKind typeOfCommand { get; set; }

    public Act(List<Command> commands, List<Command> back) 
    {
        commands.Add(new MenuBack(back));
        typeOfCommand = new TransferMenuCommand(commands);
    }

    public void execute()
    {
        typeOfCommand.ActivateType();
    } 
}
