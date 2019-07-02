using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grill : AbstractPawn, iCookingStation
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
        HideItem();
        grillComand = new CookFood(this);
    }

    public void CreateFood()
    {
        foodOnGrill = new Food("burger", 2.00M, SpriteHolder.instance.GetFoodArtFromIDNumber(0));
        ItemSprite = foodOnGrill.CaryableObjectSprite;
        ShowItem();
        grillComand = new GetFood(this);
    }

    public override void GetTargeter(Character character)
    {
        _character = character;
    }
}
