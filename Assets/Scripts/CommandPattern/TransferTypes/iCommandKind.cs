using System;
using System.Collections.Generic;
using UnityEngine;

public interface iCommandKind 
{
    void ActivateType();
    void UndoType();

    // Hack SOLID violation
    Action<List<Command>> LoadNewMenu { get; set; }

}
