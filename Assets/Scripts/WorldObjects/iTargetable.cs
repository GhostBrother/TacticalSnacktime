using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iTargetable
{
    List<Command> GetCommands();
    void GetTargeter(Character character);
}
