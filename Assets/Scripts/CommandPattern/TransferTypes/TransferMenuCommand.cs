using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferMenuCommand : iCommandKind
{

    public List<Command> nextMenu { get; set; }


    public TransferMenuCommand(List<Command> commandToGoTo)
    {
        nextMenu = commandToGoTo;
    }

    public Action<List<Command>> LoadNewMenu { get; set; }

    public void ActivateType()
    {
        LoadNewMenu.Invoke(nextMenu);
    }
}
