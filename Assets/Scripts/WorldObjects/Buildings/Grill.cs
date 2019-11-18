using System;
using System.Collections.Generic;
using UnityEngine;

public class Grill : AbstractInteractablePawn, iCookingStation 
{

    // Hack, factor this and any other addable to timelines out. 
    public Action<AbstractPawn> AddToTimeline { get; set; }
    public Action<AbstractPawn> RemoveFromTimeline { get; set; }

    public List<iCaryable> cariedObjects { get; private set; }

    public int numberOfCarriedObjects { get; private set; }

    List<DonenessTracker> donenessTrackers;

    List<Recipe> recipiesThatCanBeCreated;


    public Grill()
    {
        cariedObjects = new List<iCaryable>();
        donenessTrackers = new List<DonenessTracker>();
        recipiesThatCanBeCreated = new List<Recipe>();

        PawnSprite = SpriteHolder.instance.GetBuildingArtFromIDNumber(0);
        EntityType = EnumHolder.EntityType.CookingStation;

        // HACK hardcode
        numberOfCarriedObjects = 4; 
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
        HideCoaster(ItemCoaster);
        ShowCoaster(itemToCook.CaryableObjectSprite, x => ItemCoaster = x);
        cariedObjects.Add(itemToCook);      
        DonenessTracker donenessTrackerToAdd = _monoPool.GetDonenessTrackerInstance();
        donenessTrackerToAdd.gameObject.transform.position = new Vector3(TilePawnIsOn.transform.position.x + (xCordinateOffset * cariedObjects.Count), TilePawnIsOn.transform.position.y + yCordinateOffset, -0.5f);
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
            List<iCaryable> heldFood = new List<iCaryable>();
            for (int i = 0; i < character.cariedObjects.Count; i++)
            {
      
               if(character.cariedObjects[i] is Supply)
                {
                    Supply supply = (Supply)character.cariedObjects[i];
                    heldFood.Add(supply.FoodThisSupplyMakes);
                }

               if(character.cariedObjects[i] is Food)
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
                    SpaceContextualActions.Add(new CraftFood(this, character ,recipiesThatCanBeCreated[j]));
                }
            }
        }

            for (int i = 0; i < cariedObjects.Count; i++)
            SpaceContextualActions.Add(new TakeItem(this, character, i));

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
                    donenessTrackers[i].MoveArrowOnTracker(food.CurrentDoness);
                }
            }
            //onTurnEnd.Invoke();
        }
    }

    public iCaryable Give(int i)
    {
        return cariedObjects[i];
    }

    public void RemoveFoodFromStation(string foodToRemove)
    {

        for(int i = 0; i < cariedObjects.Count; i++)
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
        _monoPool.PutInstanceBack(donenessTrackers[i].gameObject);
        donenessTrackers.RemoveAt(i);

        if (cariedObjects.Count <= 0)
        {
            HideCoaster(ItemCoaster);
            RemoveFromTimeline.Invoke(this);
        }

    }
}
