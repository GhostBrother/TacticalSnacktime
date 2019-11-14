using System;
using System.Collections.Generic;
using UnityEngine;

public class Grill : AbstractInteractablePawn, iCookingStation 
{
    private List<Food> itemsOnGrill;

    // Hack, factor this and any other addable to timelines out. 
    public Action<AbstractPawn> AddToTimeline { get; set; }
    public Action<AbstractPawn> RemoveFromTimeline { get; set; }

    List<DonenessTracker> donenessTrackers;

    List<Recipe> recipiesThatCanBeCreated;


    public Grill()
    {
        itemsOnGrill = new List<Food>();
        donenessTrackers = new List<DonenessTracker>();
        recipiesThatCanBeCreated = new List<Recipe>();

        PawnSprite = SpriteHolder.instance.GetBuildingArtFromIDNumber(0);
        EntityType = EnumHolder.EntityType.CookingStation;
        Name = "Grill";
    }


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
        const float xCordinateOffset = .5f;
        const float yCordinateOffset = .5f;
        ShowCoaster(itemToCook.CaryableObjectSprite, x => ItemCoaster = x);
        itemsOnGrill.Add(itemToCook);      
        DonenessTracker donenessTrackerToAdd = _monoPool.GetDonenessTrackerInstance();
        donenessTrackerToAdd.gameObject.transform.position = new Vector3(TilePawnIsOn.transform.position.x + (xCordinateOffset * itemsOnGrill.Count), TilePawnIsOn.transform.position.y + yCordinateOffset, -0.5f);
        donenessTrackerToAdd.InitMeter(itemToCook.DonenessesLevels[itemToCook.DonenessesLevels.Length - 1]);
        donenessTrackers.Add(donenessTrackerToAdd);

        AddToTimeline.Invoke(this);
    }

    public override void GetTargeter(Character character)
    {

        // Should every cookable do this? Should there be an abstract cookable
        SpaceContextualActions.Clear();
       
        if (character is iCanGiveItems)
        {
            List<Food> heldFood = new List<Food>();
            for (int i = 0; i < character.CariedObjects.Count; i++)
            {
      
               if(character.CariedObjects[i] is Supply)
                {
                    Supply supply = (Supply)character.CariedObjects[i];
                    heldFood.Add(supply.FoodThisSupplyMakes);
                }

               if(character.CariedObjects[i] is Food)
                {
                    Food food = (Food)character.CariedObjects[i];
                    heldFood.Add(food);
                }

            }

            heldFood.AddRange(itemsOnGrill);    
            for (int j = 0; j < recipiesThatCanBeCreated.Count; j++)
            {
                if (recipiesThatCanBeCreated[j].CanCraftFood(heldFood))
                {
                    SpaceContextualActions.Add(new CraftFood(this, character ,recipiesThatCanBeCreated[j]));
                }
            }
        }

            for (int i = 0; i < itemsOnGrill.Count; i++)
            SpaceContextualActions.Add(new TakeItem(this, character, i));

    }

    public override void TurnStart()
    {
        if (itemsOnGrill.Count > 0)
        {
            for (int i = 0; i < itemsOnGrill.Count; i++)
            {
                itemsOnGrill[i].CurrentDoness++;
                donenessTrackers[i].MoveArrowOnTracker(itemsOnGrill[i].CurrentDoness);
            }
            onTurnEnd.Invoke();
        }
    }

    public iCaryable Give(int i)
    {
        return itemsOnGrill[i];
    }

    public void RemoveFoodFromStation(string foodToRemove)
    {

        for(int i = 0; i < itemsOnGrill.Count; i++)
        {
            if (foodToRemove == itemsOnGrill[i].Name)
            {
                GetRidOfItem(i);
                break;
            }
        }
    }

    public void GetRidOfItem(int i)
    {
        itemsOnGrill.RemoveAt(i);
        _monoPool.PutInstanceBack(donenessTrackers[i].gameObject);
        donenessTrackers.RemoveAt(i);
        HideCoaster(ItemCoaster);
        HideCoaster(FoodWantCoaster);

        if (itemsOnGrill.Count <= 0)
        {
            RemoveFromTimeline.Invoke(this);
        }

    }
}
