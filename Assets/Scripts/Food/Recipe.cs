using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe
{
    public List<Food> IngredentsForRecipe { get; private set; }
    public Food FoodCreated { get; private set; } 

    public Recipe(List<Food> _ingredentsForRecipe, Food _foodToCreate)
    {
        IngredentsForRecipe = _ingredentsForRecipe;
        FoodCreated = _foodToCreate;
    }

    public bool CanCraftFood(List<Food> foodForCrafting)
    {
        foodForCrafting.Sort((x, y) => x.Name.CompareTo(y.Name));
        for ( int i = 0; i < IngredentsForRecipe.Count; i++) 
        {
            if (!foodForCrafting.Exists(x => x.Name == IngredentsForRecipe[i].Name))
            {
                return false;
            }
        }
        return true;
    }

    public void CraftFood(List<Food> foodForCrafting)
    {
        for(int i = 0; i < IngredentsForRecipe.Count; i++)
        {
            foodForCrafting.Remove(IngredentsForRecipe[i]);
        }

    }
}
