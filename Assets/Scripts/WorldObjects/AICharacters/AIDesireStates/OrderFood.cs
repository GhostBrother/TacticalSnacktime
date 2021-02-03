﻿using System.Collections;
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

    // TODO, should check quality of food and kind
    public bool isRequestSatisfied()
    {
        if(aiCharacter.cariedObjects.Count > 0)
        {
            aiCharacter.HideCoaster(aiCharacter.NeedCoaster);
            aiCharacter.HideCoaster(aiCharacter.FoodWantCoaster);
            aiCharacter.AssessQuality();
            aiCharacter.PayForFood();
            return true;
        }
        return false;
    }
}
