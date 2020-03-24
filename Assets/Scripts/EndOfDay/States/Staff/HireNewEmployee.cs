using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HireNewEmployee : Command
{
    public string CommandName => "Hire New Employee";

    public bool isUsable => true;

    public iCommandKind typeOfCommand { get; set; }

    public void execute()
    {
      
    }
}
