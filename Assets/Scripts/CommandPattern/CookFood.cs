using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookFood : Command
{

    iCookingStation cookingStation;
    iCanGiveItems _characterWhoCanCook;
    Supply _supplyToCook;
    int _index;

    public CookFood(iCookingStation _cookingStation, iCanGiveItems characterWhoCanCook, int index)
    {
        cookingStation = _cookingStation;
        _characterWhoCanCook = characterWhoCanCook;
        _supplyToCook = (Supply)characterWhoCanCook.Give(_index);
    }

    public string CommandName { get { return $"Cook {_supplyToCook.FoodThisSupplyMakes.Name}"; } }

    public void execute()
    {
        _characterWhoCanCook.GetRidOfItem(_index);
        cookingStation.CreateFood(_supplyToCook.FoodThisSupplyMakes);
    }
}
