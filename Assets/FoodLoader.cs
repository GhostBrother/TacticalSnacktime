using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodLoader : JsonLoader<Food>
{

    List<Food> AllFoods = new List<Food>();


    public FoodLoader()
    {
        Init("Assets/JsonWaves/FoodItems.json");
    }

    public override void Init(string filePath)
    {
        base.Init(filePath);
        AllFoods = GetObjectListFromFilePathByString("Food");
    }

    public Food GetFoodById(string nameOfFoodToFind)
    {
        for(int i = 0; i < AllFoods.Count; i++)
        {
            if(nameOfFoodToFind == AllFoods[i].Name)
            {
                return AllFoods[i];
            }

        }
        return null;
    }
}
