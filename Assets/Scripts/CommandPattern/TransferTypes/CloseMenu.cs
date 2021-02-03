using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseMenuAction : iCommandKind
{
    public Action<List<Command>> LoadNewMenu { get; set; }
    public Action CloseMenu  { get; set; }

    public void ActivateType()
    {
        CloseMenu.Invoke();
    }

    public void UndoType()
    {
    }
}
