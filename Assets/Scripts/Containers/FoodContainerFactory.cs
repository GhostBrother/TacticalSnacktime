using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodContainerFactory 
{
   RecipeLoader recipeLoader = new RecipeLoader();
   FoodLoader foodLoader = new FoodLoader();

    
   public Action<iAffectedByTime> AddToTimeline {get; set; }
   public Action<AbstractPawn> RemoveFromTimeline { get; set; }

    public AbstractPawn LoadCookStation(AbstractPawn abstractPawn)
    {
        if (abstractPawn is iCookingStation)
        {
            iCookingStation cookingStation = (iCookingStation)abstractPawn;
            cookingStation.LoadRecipies(recipeLoader.GetRecipiesForCooktop(abstractPawn.Name));
        }

        if (abstractPawn is iAffectedByTime)
        {
            iAffectedByTime affectedByTime = (iAffectedByTime)abstractPawn;
            affectedByTime.AddToTimeline = AddToTimeline;
            affectedByTime.RemoveFromTimeline = RemoveFromTimeline;
        }

        return abstractPawn;
    }

    public Supply LoadSupply(Supply supply, string supplyParams)
    {
        if (supplyParams.Length > 1)
        {
            string supplyToFind = string.Empty;
           
            switch (char.ToLower(supplyParams[1]))
            {

                case 'p':
                    {
                        supplyToFind = "Patty";
                        break;
                    }

                case 'b':
                    {
                        supplyToFind = "Burger";
                        break;
                    }

                case 'e':
                    {
                        supplyToFind = "Fried Egg";
                        break;
                    }

                case 'f':
                    {
                        supplyToFind = "Fries";
                        break;
                    }
            }
            supply.FoodThisSupplyMakes = foodLoader.GetFoodById(supplyToFind);
            int numberOfSupply;
            if (int.TryParse(supplyParams[2].ToString(), out numberOfSupply))
                supply.NumberOfItemsInSupply = numberOfSupply;
            else
                numberOfSupply = 1;
            supply.HandsRequired = 1;
        }
        return supply;
    }
   
}
