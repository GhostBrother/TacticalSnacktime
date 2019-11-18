using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iAffectedByTime 
{
    Action<AbstractPawn> onStartTurn { set; get; } // Character
    int TurnOrder { get; }
    void TurnStart();
    Action onTurnEnd { set; get; }
    void TurnEnd();
}
