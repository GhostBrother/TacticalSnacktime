using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iAffectedByTime 
{
    Action<AbstractPawn> onStartTurn { get; set; } 
    int TurnOrder { get; }
    void TurnStart();
    Action onTurnEnd { set; get; }
    void TurnEnd();

    void OnEndDay();

    Action<iAffectedByTime> AddToTimeline { get; set; }
    Action<AbstractPawn> RemoveFromTimeline { get; set; }
}
