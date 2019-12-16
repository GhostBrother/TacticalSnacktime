using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour, iAffectedByTime
{
    public Action<AbstractPawn> onStartTurn { get; set; }
    public Action onTurnEnd { get ; set; }
    public Action onDayOver { get; set; }

    Text _time;
   
    public int TurnOrder { get; set; }
    public Action<iAffectedByTime> AddToTimeline { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public Action<AbstractPawn> RemoveFromTimeline { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    int _openingHour = 8;
    int _openingMinute = 0;

    int _hour { get; set; }
    int _minute;

    public string Time { get { return _time.text; } } 


    int _closingHour = 2;
    int _closingMinute = 0;


    private void Start()
    {
        _time = this.GetComponentInChildren<Text>();
        SetOpeningTime();
        UpdateClock();
    }

    void SetOpeningTime()
    {
        _hour = _openingHour;
        _minute = _openingMinute;
    }

    public void TurnStart()
    {
        _minute += 15;
        UpdateClock();
        TurnEnd();
    }

    public void TurnEnd()
    {
        if (_hour == _closingHour && _closingMinute == _minute)
            onDayOver.Invoke();
        else
         onTurnEnd.Invoke();

        
    }

    private void UpdateClock()
    {
        if (_minute >= 60)
        { _minute -= 60;
            _hour += 1;
        }
        if (_hour > 12)
        {
            _hour -= 12;
        }
        _time.text = LeadingZero(_hour) + ":" + LeadingZero(_minute);
    }

    string LeadingZero(int n)
    {
        return n.ToString().PadLeft(2, '0');
    }

}
