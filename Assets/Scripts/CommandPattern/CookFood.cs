using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookFood : Command
{

    iCookingStation cookingStation;
    Food _foodToCook;

    public CookFood(iCookingStation _cookingStation, Food foodToCook)
    {
        cookingStation = _cookingStation;
        _foodToCook = foodToCook;
    }

    public string CommandName { get { return $"Cook {_foodToCook.Name}"; } }

    public void execute()
    {
        cookingStation.CreateFood(_foodToCook);
    }
}
