using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grill : AbstractInteractablePawn, iCookingStation 
{
    private Food foodOnGrill;
    private Command grillComand;
    private Food itemToCook;

    public Grill()
    {
        itemToCook = new Food("burger", 2.00M, SpriteHolder.instance.GetFoodArtFromIDNumber(0));
        PawnSprite = SpriteHolder.instance.GetBuildingArtFromIDNumber(0);
        grillComand = new CookFood(this,itemToCook);
        EntityType = EnumHolder.EntityType.CookingStation;
    }

    public override Command GetCommand()
    {
        return grillComand;
    }

    public void AddToFood(Food food)
    {
        
    }

    public iCaryable Give()
    {
        return foodOnGrill;
    }

    public void CreateFood()
    {
        foodOnGrill = itemToCook;
        ShowCoaster(foodOnGrill.CaryableObjectSprite, x => ItemCoaster = x);
    }

    public override void GetTargeter(Character character)
    {
        if (foodOnGrill != null && character.CariedObject == null)
            grillComand = new TakeItem(this, character);

        else if (foodOnGrill == null)
        {
            grillComand = new CookFood(this, itemToCook);
        }

    }

    public void GetRidOfItem()
    {
        foodOnGrill = null;
        HideCoaster(ItemCoaster);
    }
}
