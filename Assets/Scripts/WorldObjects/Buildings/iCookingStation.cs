using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iCookingStation : iCanGiveItems , iContainCaryables
{
    void CreateFood(Food itemToCook);
    void RemoveFoodFromStation(string foodName);
    void LoadRecipies(List<Recipe> recipes);
}
