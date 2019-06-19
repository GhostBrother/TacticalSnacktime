using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookFood : Command
{

    iCookingStation cookingStation;

    public CookFood(iCookingStation _cookingStation)
    {
        cookingStation = _cookingStation;
    }

    public string CommandName { get { return "Cook Food"; } }

    public void execute()
    {
        cookingStation.CreateFood();
    }
}
