using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Command
{
     iCommandKind typeOfCommand { get; set; }
     bool isUsable { get; }
     string CommandName { get; }
     void execute();
}
