using System;
using System.Collections.Generic;
using UnityEngine;

public interface iCommandKind 
{
    void ActivateType();
    void UndoType();


    Action<List<Command>> LoadNewMenu { get; set; }

    Action CloseMenu { get; set; }
}
