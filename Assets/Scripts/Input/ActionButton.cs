using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    public delegate void OnExecuteCommand();
    public OnExecuteCommand onActionTaken;

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
        onActionTaken.Invoke();
    }
}
