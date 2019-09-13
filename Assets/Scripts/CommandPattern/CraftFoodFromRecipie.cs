using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftFood : Command
{

    string _commandName;
    public string CommandName { get { return _commandName; } }
    iCookingStation _cookingStation;
    Recipe _recipieToMake;

    public CraftFood( iCookingStation cookStaion ,Recipe recipeToMake)
    {
        createCommandName(recipeToMake);
        _cookingStation = cookStaion;
        _recipieToMake = recipeToMake;
    }

    void createCommandName(Recipe recipeToMake)
    {
        _commandName = "Combine ";
        for(int i = 0; i < recipeToMake.IngredentsForRecipe.Count -1; i++)
        {
            _commandName += $"{recipeToMake.IngredentsForRecipe[i].Name} + ";
        }
        _commandName += $"{recipeToMake.IngredentsForRecipe[recipeToMake.IngredentsForRecipe.Count-1].Name} for {recipeToMake.FoodCreated}";
    }

    public void execute()
    {
        for(int i = 0; i < _recipieToMake.IngredentsForRecipe.Count; i++)
        {
            _cookingStation.RemoveFoodFromStation(_recipieToMake.IngredentsForRecipe[i]);
        }
        _cookingStation.CreateFood(_recipieToMake.FoodCreated);
    }
}
