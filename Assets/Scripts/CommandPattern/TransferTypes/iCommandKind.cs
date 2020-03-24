using System;
using System.Collections.Generic;
using UnityEngine;

public interface iCommandKind 
{
    void ActivateType();

    // Hack SOLID violation
    Action<List<Command>> LoadNewMenu { get; set; }

}
