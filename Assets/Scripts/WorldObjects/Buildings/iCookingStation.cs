using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iCookingStation : iTargetable
{
    void CreateFood();
    void AddToFood(Food food);
    void CollectFood();
}
