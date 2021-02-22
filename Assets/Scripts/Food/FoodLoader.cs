using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    public Food GetFoodByName(string nameOfFoodToFind)
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

    public Food RandomOfType(string TypeOfFoodToFind)
    {

        var match = AllFoods.FindAll(item => item.TypeOfFood.Contains(TypeOfFoodToFind));
        if (match == null)
        {
            Debug.Log($"Type {TypeOfFoodToFind} could not be found");
        }

        return match[Random.Range(0, match.Count)];
    }

    // Don't know if this is the right place;
    public Supply GetFoodAsSupply(string NameOfFoodToFind, int numberOfFood)
    {
        Supply supplyToReturn = new Supply();
       // supplyToReturn.FoodThisSupplyMakes = GetFoodByName(NameOfFoodToFind);
       // supplyToReturn.NumberOfItemsInSupply = numberOfFood;

        Food foodToAddToSupply = GetFoodByName(NameOfFoodToFind);
        foodToAddToSupply.NumberOfItemsInSupply = numberOfFood;
        supplyToReturn.cariedObjects.Add(foodToAddToSupply);

        return supplyToReturn;
    }
}

