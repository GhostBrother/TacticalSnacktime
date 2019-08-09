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
        if(aiCharacter.Give() != null)
        { 
            aiCharacter.HideCoaster(aiCharacter.NeedCoaster);
            aiCharacter.HideCoaster(aiCharacter.FoodWantCoaster);
            return true;
        }
        return false;
    }
}
