using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoTransfer : iCommandKind
{
    public Action<List<Command>> LoadNewMenu { get; set; }
    public Action CloseMenu { get; set; }

    public void ActivateType()
    {
       
    }

    public void UndoType()
    {
        
    }
}
