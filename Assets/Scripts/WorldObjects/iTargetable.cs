using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Any object that can be interacted with by the crew or customers.
public interface iTargetable
{
    
    //Accesses the list of actions that the pawn can give a character
    List<Command> GetCommands();

    // Don't know about this one. 
    void GetTargeter(Character character);
   
}
