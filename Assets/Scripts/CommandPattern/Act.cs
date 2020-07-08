using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Act : Command
{
    public string CommandName => "Action";

    public bool isUsable => true; 

    private List<Command> _NextCommands;

    private List<Command> GetCommands()
    {
        return _NextCommands;
    }

    public iCommandKind typeOfCommand { get; set; }

    public Act(List<Command> commands, Func<List<Command>> back ) 
    {
        _NextCommands = commands;
        _NextCommands.Add(new MenuBack(back));
        typeOfCommand = new TransferMenuCommand(GetCommands);
    }

    public void execute()
    {
        typeOfCommand.ActivateType();
    } 
}
