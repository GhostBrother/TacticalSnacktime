using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEndOfDayState : Command
{
    EndOfDayPannel _endOfDayPannel;
    iEndOfDayState _stateToChangeTo;
    
    public ChangeEndOfDayState(EndOfDayPannel endOfDayPannel, iEndOfDayState stateToChangeTo)
    {
        _endOfDayPannel = endOfDayPannel;
        _stateToChangeTo = stateToChangeTo;
        typeOfCommand = new CloseMenu();
    }

    public string CommandName { get { return _stateToChangeTo.ToString();} }

    public bool isUsable => true;

    public iCommandKind typeOfCommand { get; set; }

    public void execute()
    {
        _endOfDayPannel.HidePropsForState();
        _endOfDayPannel.ChangeState(_stateToChangeTo);
        _stateToChangeTo.DisplayProps();
    }
}
