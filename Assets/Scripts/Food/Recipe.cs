using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe
{

    public string Name { get; private set; }
    public int id { get; private set; }
    public List<string> NameOfIngredentsForRecipe { get; private set; }
    public Food FoodCreated { get;  set; } 

    public Recipe( string Name , List<string> Recipe, int ID)                                  
    {
        NameOfIngredentsForRecipe = Recipe;
        id = ID;
        this.Name = Name;
    }

    public bool CanCraftFood(List<iCaryable> foodForCrafting)
    {
        foodForCrafting.Sort((x, y) => x.Name.CompareTo(y.Name));


        for ( int i = 0; i < NameOfIngredentsForRecipe.Count; i++) 
        {
            if (!foodForCrafting.Exists(x => x.Name == NameOfIngredentsForRecipe[i]))
            {
                return false;
            }
        }
        return true;
    }

    public void CraftFood(List<Food> foodForCrafting)
    {
        foodForCrafting.Sort((x, y) => x.Name.CompareTo(y.Name));
        for (int i = 0; i < NameOfIngredentsForRecipe.Count; i++)
        {
            if(foodForCrafting.Exists(x => x.Name == NameOfIngredentsForRecipe[i]))
            {
                foodForCrafting.RemoveAt(i);
            }
        }

    }
}
