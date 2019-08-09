using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grill : AbstractInteractablePawn, iCookingStation 
{
    private Food foodOnGrill;
    Character _character;
    private Command grillComand; 

    public Grill()
    {
        PawnSprite = SpriteHolder.instance.GetBuildingArtFromIDNumber(0);
        grillComand = new CookFood(this);
        EntityType = EnumHolder.EntityType.CookingStation;
    }

    public override Command GetCommand()
    {
        return grillComand;
    }

    public void AddToFood(Food food)
    {
        
    }

    public void CollectFood()
    {
        _character.PickUp(foodOnGrill);
        foodOnGrill = null;
        HideCoaster(ItemCoaster);
        grillComand = new CookFood(this);
    }

    public void CreateFood()
    {
        foodOnGrill = new Food("burger", 2.00M, SpriteHolder.instance.GetFoodArtFromIDNumber(0));
        ShowCoaster(foodOnGrill.CaryableObjectSprite, x => ItemCoaster = x);
        grillComand = new GetFood(this);
    }

    public override void GetTargeter(Character character)
    {
        _character = character;
    }
}
