using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grill : AbstractInteractablePawn, iCookingStation 
{
   // private Food foodOnGrill;
    private List<Food> itemsOnGrill;

    // Hack, factor this and any other addable to timelines out. 
    public Action<AbstractPawn> AddToTimeline { get; set; }
    public Action<AbstractPawn> RemoveFromTimeline { get; set; }

    List<DonenessTracker> donenessTrackers;

    public Grill()
    {
        itemsOnGrill = new List<Food>();
        donenessTrackers = new List<DonenessTracker>();

        PawnSprite = SpriteHolder.instance.GetBuildingArtFromIDNumber(0);
        EntityType = EnumHolder.EntityType.CookingStation;
        Name = "Grill";
    }

    public override List<Command> GetCommands()
    {
        return SpaceContextualActions;
    }

    public void AddToFood(Food food)
    {
        
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
                    iCanGiveItems givingCharacter = (iCanGiveItems)character;
                    SpaceContextualActions.Add(new CookFood(this, givingCharacter, i));
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

    public void GetRidOfItem(int i)
    {
        itemsOnGrill.RemoveAt(i);
        _monoPool.PutInstanceBack(donenessTrackers[i].gameObject);

        if (itemsOnGrill.Count <= 0)
        {
            HideCoaster(ItemCoaster);
            HideCoaster(FoodWantCoaster);
            RemoveFromTimeline.Invoke(this);
        }
    }
}
