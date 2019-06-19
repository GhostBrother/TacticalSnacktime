using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Command
{
     string CommandName { get; }
     void execute();
}
