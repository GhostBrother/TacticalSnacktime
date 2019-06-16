using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grill : AbstractPawn, iCookingStation
{
    private Food foodOnGrill;

    public Grill()
    {
        PawnSprite = SpriteHolder.instance.GetArtFromIDNumber(4);
    }


    public void Activate()
    {
       
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
