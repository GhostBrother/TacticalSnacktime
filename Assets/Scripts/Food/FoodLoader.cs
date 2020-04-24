using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodLoader : JsonLoader<Food>
{

    public List<Food> AllFoods { get; private set; } 


    public FoodLoader()
    {
        AllFoods = new List<Food>();
        Init("Assets/JsonWaves/FoodItems.json");
    }

    public override void Init(string filePath)
    {
        base.Init(filePath);
        AllFoods = GetObjectListFromFilePathByString("Food");
    }

    //TODO: Get Food by name rename
    public Food GetFoodById(string nameOfFoodToFind)
    {
        for(int i = 0; i < AllFoods.Count; i++)
        {
            if(nameOfFoodToFind == AllFoods[i].Name)
            {
                return AllFoods[i]; 
            }
        }
        Debug.Log($"Food named {nameOfFoodToFind} not found in loader" );
        return null;
    }

    public Food GetRandomFood()
    {
        return AllFoods[Random.Range(0, AllFoods.Count)];
    }
}
