using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iTargetable
{
    // Any object that can be interacted with by the crew or customers.
    Command GetCommand();
   
}
