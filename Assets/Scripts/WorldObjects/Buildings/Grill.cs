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

    public Grill()
    {
        itemsOnGrill = new List<Food>();

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

    public iCaryable Give()
    {
        // Place holder
        return itemsOnGrill[0];
    }

    public void CreateFood(Food itemToCook)
    {
         ShowCoaster(itemToCook.CaryableObjectSprite, x => ItemCoaster = x);
        itemsOnGrill.Add(itemToCook);
        // StartsCookingTimer
        AddToTimeline.Invoke(this);
    }

    public override void GetTargeter(Character character)
    {
        SpaceContextualActions.Clear();

        if (character.CariedObject is Supply && character is iCanGiveItems)
        {
            iCanGiveItems givingCharacter = (iCanGiveItems)character;
            Supply supply = (Supply)givingCharacter.Give();
            givingCharacter.GetRidOfItem();
            SpaceContextualActions.Add(new CookFood(this, supply.FoodThisSupplyMakes));
        }

        if (itemsOnGrill.Count > 0 && character.CariedObject == null)
            SpaceContextualActions.Add(new TakeItem(this, character));

    }

    public void GetRidOfItem()
    {
        // Placeholder 0;
        itemsOnGrill.RemoveAt(0);
        HideCoaster(ItemCoaster);
        HideCoaster(FoodWantCoaster);
        if (itemsOnGrill.Count <= 0)
        {
            RemoveFromTimeline.Invoke(this);
        }
    }

    public override void TurnStart()
    {
        if (itemsOnGrill.Count > 0)
        {
            const int xCordinateOffset = -1;
            const int yCordinateOffset = 1;
            for(int i = 0; i < itemsOnGrill.Count; i++)
            {
                itemsOnGrill[i].Doneness++;
            }
            ShowCoasterWithOffset(SpriteHolder.instance.GetFoodArtFromIDNumber(0), xCordinateOffset, yCordinateOffset, x => FoodWantCoaster = x);
            onTurnEnd.Invoke();
        }
    }

}
