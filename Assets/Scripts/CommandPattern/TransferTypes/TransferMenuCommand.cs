using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferMenuCommand : iCommandKind
{
    Func<List<Command>> _nextMenu;

    public Action<List<Command>> LoadNewMenu { get; set; }
    public Action CloseMenu { get; set; }

    public TransferMenuCommand(Func<List<Command>> commands)
    {
        _nextMenu = commands;
    }

    public void ActivateType()
    {
        CloseMenu.Invoke();
        LoadNewMenu.Invoke(_nextMenu.Invoke());
    }

    public void UndoType()
    {
        CloseMenu.Invoke();
    }
}
