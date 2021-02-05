using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour, iAffectedByTime
{
    public Action<AbstractPawn> onStartTurn { get; set; }
    public Action onTurnEnd { get; set; }
    public Action onDayOver { get; set; }

    Text _time;

    public int TurnOrder { get; set; }
    public Action<iAffectedByTime> AddToTimeline { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public Action<AbstractPawn> RemoveFromTimeline { get; set; }

    public TimeSpan OpeningTime { get; private set; }

    public TimeSpan CurTime { get; private set; }

    TimeSpan _ClosingTime;


    private void Start()
    {
        _time = this.GetComponentInChildren<Text>();
        OpeningTime = new TimeSpan(8, 0,0);
        _ClosingTime = new TimeSpan(10, 0, 0);
    }

   public void SetClockToStartOfDay()
    {
        CurTime = OpeningTime;
        UpdateClock();
    }

    public void TurnStart()
    {
        CurTime += TimeSpan.FromMinutes(15);
        UpdateClock();
        TurnEnd();
    }

    public void TurnEnd()
    {
        if (CurTime == _ClosingTime)
            onDayOver.Invoke();
        else
         onTurnEnd.Invoke();
   
    }

    private void UpdateClock()
    {
        _time.text = CurTime.ToString(@"hh\:mm");
    }

    public void OnEndDay()
    {
        SetClockToStartOfDay();
    }
}
