using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusCommand : Command
{
    public string CommandName => "Status" ;

    public bool isUsable => true;

    public iCommandKind typeOfCommand { get; set; }

    public StatusCommand(Character character)
    {

        typeOfCommand = new NoTransfer();
    }

    public void execute()
    {
        
    }
}
