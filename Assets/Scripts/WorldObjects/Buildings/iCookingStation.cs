using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iCookingStation : iContainCaryables // iCanGiveItems ,
{
    void CreateFood(Food itemToCook);
    void RemoveFoodFromStation(string foodName);
    void LoadRecipies(List<Recipe> recipes);
}
