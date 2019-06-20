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

    // Circular dependancy HACK
    private ActionMenu _actionMenu;
    public ActionMenu actionMenu { private get { return _actionMenu; } set { _actionMenu = value; } }
    
  
    public void ExecuteStoredCommand()
    {
        actionMenu.HideAllActions();
        storedCommand.execute();
    }
}
