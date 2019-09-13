using System;
using System.Collections;
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

        // PlaceHolder
        List<Food> breakfastBurger = new List<Food>();
        breakfastBurger.Add(new Food("Burger", 2.00M, SpriteHolder.instance.GetFoodArtFromIDNumber(0), 1));
        breakfastBurger.Add(new Food("Egg", 1.00M, SpriteHolder.instance.GetFoodArtFromIDNumber(1), 2));
        Recipe recipe = new Recipe(breakfastBurger, new Food("Breakfast Burger", 3.00M, SpriteHolder.instance.GetFoodArtFromIDNumber(1),3));
        recipiesThatCanBeCreated.Add(recipe);
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
        donenessTrackerToAdd.InitMeter(itemToCook.Doneness[itemToCook.Doneness.Length - 1]);
        donenessTrackers.Add(donenessTrackerToAdd);

        AddToTimeline.Invoke(this);
    }

    public override void GetTargeter(Character character)
    {
        SpaceContextualActions.Clear();
        if (character is iCanGiveItems)
        {
            for (int i = 0; i < character.CariedObjects.Count; i++)
            {
                if (character.CariedObjects[i] is Supply)
                {
                    Supply supply = (Supply)character.CariedObjects[i];
                    iCanGiveItems givingCharacter = (iCanGiveItems)character;
                    SpaceContextualActions.Add(new CookFood(this, givingCharacter, i));

                    for(int j = 0; j < recipiesThatCanBeCreated.Count; j++)
                    {
                       if(recipiesThatCanBeCreated[j].CanCraftFood(itemsOnGrill))
                        {
                            SpaceContextualActions.Add(new CraftFood(this, recipiesThatCanBeCreated[j]));
                        }
                    }
                }
            }
        }

            for(int i = 0; i < itemsOnGrill.Count; i++ )
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

    public void RemoveFoodFromStation(Food foodToRemove)
    {
        for(int i = 0; i < itemsOnGrill.Count; i++)
        {
            if (foodToRemove.Name == itemsOnGrill[i].Name)
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
