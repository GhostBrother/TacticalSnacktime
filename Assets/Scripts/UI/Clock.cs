using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour, iAffectedByTime
{
    public Action<Character> onStartTurn { get; set; }
    public Action onTurnEnd { get ; set; }
    public Action onDayOver { get; set; }

    public int TurnOrder { get; set; }

    int openingHour = 12;
    int timeOfDay;
    // 8:00?
    int closingTurn = 480;

    int demoClosing = 120;

    public void TurnStart()
    {
        timeOfDay += 15;
        TurnEnd();
    }

    public void TurnEnd()
    {
        Debug.Log($"{ openingHour + Math.Floor(timeOfDay * 0.017f)} : {(timeOfDay % 60) }");
        if (timeOfDay != demoClosing)
            onTurnEnd.Invoke();
        else
            onDayOver.Invoke();
    }

}
