using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    private Command storedCommand;

    public Command StoredCommand
    {
        private get { return storedCommand; }
        set
        {
            storedCommand = value;
            this.GetComponentInChildren<Text>().text = storedCommand.CommandName;
        }
    }
    
    public void ExecuteStoredCommand()
    {    
        storedCommand.execute();
        storedCommand.typeOfCommand.ActivateType();        
    }
}
