using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grill : AbstractPawn, iCookingStation
{
    private Food foodOnGrill;
    private Command grillComand; 

    public Grill()
    {
        PawnSprite = SpriteHolder.instance.GetArtFromIDNumber(4);
        grillComand = new CookFood(this);
    }

    public Command GetCommand()
    {
        return grillComand;
    }

    public void AddToFood(Food food)
    {
        
    }

    public Food CollectFood()
    {
        return foodOnGrill;
    }

    public void CreateFood()
    {
        foodOnGrill = new Food("burger", 2.00M);
    }

}
