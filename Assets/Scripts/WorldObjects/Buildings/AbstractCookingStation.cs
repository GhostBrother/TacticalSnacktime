using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractCookingStation : AbstractInteractablePawn, iCookingStation, iAffectedByTime
{
    public Action<iAffectedByTime> AddToTimeline { get;  set; }
    public Action<AbstractPawn> RemoveFromTimeline { get;  set; }

    public List<iCaryable> cariedObjects { get;  set; }

    public int numberOfCarriedObjects { get; set; }

    //Added
    public Action<AbstractPawn> onStartTurn { get; set; }
    public Action onTurnEnd { get; set; }

    protected List<Recipe> recipiesThatCanBeCreated;

    public void LoadRecipies(List<Recipe> recipes)
    {
        recipiesThatCanBeCreated = recipes;
    }

    public override List<Command> GetCommands()
    {
        return SpaceContextualActions;
    }

    public void CreateFood(Food itemToCook)
    {
        HideCoaster(ItemCoaster);
        ShowCoaster(itemToCook.CaryableObjectSprite, x => {  ItemCoaster = x; });
        cariedObjects.Add(itemToCook);
        AddToTimeline.Invoke(this);
        
    }

    public override void GetTargeter(Character character)
    {

        SpaceContextualActions.Clear();

        if (character is iCanGiveItems)
        {
            List<iCaryable> heldFood = new List<iCaryable>();
            for (int i = 0; i < character.cariedObjects.Count; i++)
            {

                if (character.cariedObjects[i] is Supply)
                {
                    Supply supply = (Supply)character.cariedObjects[i];
                    heldFood.Add(supply.FoodThisSupplyMakes);
                }

                if (character.cariedObjects[i] is Food)
                {
                    Food food = (Food)character.cariedObjects[i];
                    heldFood.Add(food);
                }

            }

            heldFood.AddRange(cariedObjects);
            for (int j = 0; j < recipiesThatCanBeCreated.Count; j++)
            {
                if (recipiesThatCanBeCreated[j].CanCraftFood(heldFood))
                {
                    SpaceContextualActions.Add(new CraftFood(this, character, recipiesThatCanBeCreated[j]));
                }
            }
        }

        for (int i = 0; i < cariedObjects.Count; i++)
            SpaceContextualActions.Add(new GetCookedFood(this, character, i));

    }

    public override void TurnStart()
    {
        onStartTurn.Invoke(this);
        if (cariedObjects.Count > 0)
        {
            for (int i = 0; i < cariedObjects.Count; i++)
            {
                if (cariedObjects[i] is Food)
                {

                    Food food = (Food)cariedObjects[i];
                    food.CurrentDoness++;
                }

            }

        }
    }

    public override void TurnEnd()
    {
        onTurnEnd.Invoke();
    }

    public iCaryable Give(int i)
    {
        return cariedObjects[i];
    }

    public void RemoveFoodFromStation(string foodToRemove)
    {

        for (int i = 0; i < cariedObjects.Count; i++)
        {
            if (foodToRemove == cariedObjects[i].Name)
            {
                GetRidOfItem(i);
                break;
            }
        }
    }

    public void GetRidOfItem(int i)
    {
        cariedObjects.RemoveAt(i);

        if (cariedObjects.Count <= 0)
        {
            HideCoaster(ItemCoaster);
            RemoveFromTimeline.Invoke(this);
        }

    }
    
    void GetRidOfAllItems()
    {
        for(int i = 0; i < cariedObjects.Count; i++)
        {
            GetRidOfItem(i);
        }
    }

    public void OnEndDay()
    {
        GetRidOfAllItems();
        ItemCoaster = null;
    }
}
