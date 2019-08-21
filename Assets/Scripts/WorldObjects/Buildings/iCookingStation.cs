using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iCookingStation : iCanGiveItems
{
    void CreateFood(Food itemToCook);
    void AddToFood(Food food);
    void LoadSupply(Supply supply);
}
