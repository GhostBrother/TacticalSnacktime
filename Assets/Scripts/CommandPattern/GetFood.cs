using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetFood : Command
{
     iCookingStation _cookingStation;


    public GetFood(iCookingStation cookingStation)
    {
        _cookingStation = cookingStation;
    }

    public string CommandName { get { return "Get Food"; } }

    public void execute()
    {
        _cookingStation.CollectFood();
    }
}
