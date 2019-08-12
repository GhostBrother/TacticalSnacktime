using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookFood : Command
{

    iCookingStation cookingStation;
    string _foodName;

    public CookFood(iCookingStation _cookingStation, Food foodToCook)
    {
        cookingStation = _cookingStation;
        _foodName = foodToCook.Name;
    }

    public string CommandName { get { return $"Cook {_foodName}"; } }

    public void execute()
    {
        cookingStation.CreateFood();
    }
}
