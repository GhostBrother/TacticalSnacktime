using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iCookingStation : iCanGiveItems 
{
    void CreateFood(Food itemToCook);
    void RemoveFoodFromStation(Food food);
}
