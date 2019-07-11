using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iCookingStation
{
    void CreateFood();
    void AddToFood(Food food);
    void CollectFood();
}
