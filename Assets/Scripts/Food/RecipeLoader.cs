using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RecipeLoader : JsonLoader<Recipe>
{
    string[] CooktopTypes = { "Grill", "DeepFrier" };
    Dictionary<string, List<Recipe>> cookStationFoodLookUp;
    List<Recipe> foodsPerCookStation = new List<Recipe>();
    FoodLoader fl;

    public RecipeLoader()
    {
        fl = new FoodLoader();
        Init("Assets/JsonWaves/RecipeItems.json");
    }

    public override void Init(string filePath)
    {
        base.Init(filePath);
        cookStationFoodLookUp = new Dictionary<string, List<Recipe>>();

        for (int i = 0; i < CooktopTypes.Length; i++)
        {
            List<Recipe> recipes = new List<Recipe>();
            foreach (Recipe r in GetObjectListFromFilePathByString(CooktopTypes[i]))
            {
                r.FoodCreated = fl.GetFoodByName(r.Name);
                recipes.Add(r);
            }
            cookStationFoodLookUp.Add(CooktopTypes[i], recipes);
        }
    }

    public List<Recipe> GetRecipiesForCooktop(string Name)
    {
        return cookStationFoodLookUp[Name];
    }

}
