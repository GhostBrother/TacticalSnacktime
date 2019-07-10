using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveResturant : IDesireState
{
    AICharacter aiCharacter;
    public LeaveResturant(AICharacter _aiCharacter)
    {
        aiCharacter = _aiCharacter;
    }

    public void MoveTarget()
    {

    }

    public bool isRequestSatisfied()
    {
        return true;
    }
}
