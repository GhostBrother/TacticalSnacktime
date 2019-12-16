using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodContainerFactory 
{
   RecipeLoader recipeLoader = new RecipeLoader();
   FoodLoader foodLoader = new FoodLoader();
    CookstationLoader cookstationLoader = new CookstationLoader();
    
   public Action<iAffectedByTime> AddToTimeline {get; set; }
   public Action<AbstractPawn> RemoveFromTimeline { get; set; }

    public AbstractCookingStation LoadCookStation(AbstractCookingStation cookingStation, string cookStationType)  //AbstractPawn abstractPawn
    {
        int numberOfSupply;
        if (int.TryParse(cookStationType[1].ToString(), out numberOfSupply))
            cookingStation = cookstationLoader.GetCookingStationById(numberOfSupply);

         cookingStation.LoadRecipies(recipeLoader.GetRecipiesForCooktop(cookingStation.Name));
         cookingStation.AddToTimeline = AddToTimeline;
         cookingStation.RemoveFromTimeline = RemoveFromTimeline;

        return cookingStation;
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
                        supplyToFind = "FrozenFries";
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
