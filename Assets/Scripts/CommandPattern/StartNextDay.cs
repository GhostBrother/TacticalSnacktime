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
    }

    public string CommandName { get { return "Start the next day"; } }

    public void execute()
    {
        _startNextDay.Invoke();
    }
}
