using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftFood : Command
{

    string _commandName;
    public string CommandName { get { return _commandName; } }
    iCookingStation _cookingStation;
    Recipe _recipieToMake;
    Character _character;

    public CraftFood( iCookingStation cookStaion,Character character,Recipe recipeToMake)
    {
        createCommandName(recipeToMake);
        _cookingStation = cookStaion;
        _recipieToMake = recipeToMake;
        _character = character;
    }

    void createCommandName(Recipe recipeToMake)
    {
        _commandName = "Cook ";
        for(int i = 0; i < recipeToMake.NameOfIngredentsForRecipe.Count -1; i++)
        {
            _commandName = "Combine ";
            _commandName += $"{recipeToMake.NameOfIngredentsForRecipe[i]} + ";
        }
        _commandName += $"{recipeToMake.NameOfIngredentsForRecipe[recipeToMake.NameOfIngredentsForRecipe.Count-1]} for {recipeToMake.FoodCreated.Name}";
    }

    public void execute()
    {
        List<string> names = new List<string>();
        for(int k = 0; k < _recipieToMake.NameOfIngredentsForRecipe.Count; k++)
        {
            names.Add(_recipieToMake.NameOfIngredentsForRecipe[k]);
        }

        for(int j = 0; j < _character.CariedObjects.Count; j++)
        {
            string s = string.Empty;
            if(_character.CariedObjects[j] is Food)
            {
                Food f = (Food)_character.CariedObjects[j];
                s = f.Name;
            }

            if(_character.CariedObjects[j] is Supply)
            {
                Supply supply = (Supply)_character.CariedObjects[j];
                s = supply.FoodThisSupplyMakes.Name;
            }

            if(names.Contains(s))
            {
                names.Remove(_character.CariedObjects[j].Name);
                _character.CariedObjects.RemoveAt(j);
                j--;
            }
        }
        for(int i = 0; i < names.Count; i++)
        {
            _cookingStation.RemoveFoodFromStation(names[i]);
        }
        _cookingStation.CreateFood(_recipieToMake.FoodCreated);
    }
}
