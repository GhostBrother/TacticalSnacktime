using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartNextDay : Command
{
    Action _startNextDay;
    public StartNextDay(Action startNextDay)
    {
        _startNextDay = startNextDay;
        typeOfCommand = new CloseMenu();
    }

    public string CommandName { get { return "Start the next day"; } }

    public bool isUsable => true;

    public iCommandKind typeOfCommand { get; set; }

    public void execute()
    {
        _startNextDay.Invoke();
    }
}
