using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodContainerFactory 
{
   RecipeLoader recipeLoader = new RecipeLoader();
    FoodLoader foodLoader = new FoodLoader();

    public AbstractPawn LoadCookStation(AbstractPawn abstractPawn)
    {
        if (abstractPawn is iCookingStation)
        {
            iCookingStation cookingStation = (iCookingStation)abstractPawn;
            cookingStation.LoadRecipies(recipeLoader.GetRecipiesForCooktop(abstractPawn.Name));
        }

        return abstractPawn;
    }

    public Supply LoadSupply(Supply supply)
    {
        supply.FoodThisSupplyMakes = foodLoader.GetFoodById(supply.NameOfFoodInSupply);
        return supply;
    }
   
}
