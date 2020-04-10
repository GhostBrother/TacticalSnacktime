using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseMenu : iCommandKind
{
    public Action<List<Command>> LoadNewMenu { get; set; }

    public void ActivateType()
    {

    }

    public void UndoType()
    {
        
    }
}
