using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderFood : IDesireState
{
    AICharacter aiCharacter;
    public OrderFood(AICharacter _aiCharacter)
    {
        aiCharacter = _aiCharacter;
    }

    public void MoveTarget ()
    {
       
    }

    public bool isRequestSatisfied()
    {
        return (aiCharacter.Give() != null);
    }
}
