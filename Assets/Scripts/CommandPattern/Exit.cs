using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : Command
{
    Character _employee;
    public iCommandKind typeOfCommand { get; set; }

    public bool isUsable => true;

    public string CommandName => "Exit";

    public Exit(Character employee)
    {
        _employee = employee;
        typeOfCommand = new CloseMenuAction();
    }

    public void execute()
    {
        _employee.NeedsRemoval = true;
        _employee.onTurnEnd.Invoke();
    }
}
