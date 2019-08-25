using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iAffectedByTime 
{
    Action<Character> onStartTurn { set; get; }
    int TurnOrder { get; }
    void TurnStart();
    Action onTurnEnd { set; get; }
    void TurnEnd();
}
