using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grill : AbstractInteractablePawn, iCookingStation 
{
    private Food foodOnGrill;
    private List<Food> itemsToCook;

    public Grill()
    {
        itemsToCook = new List<Food>();
        itemsToCook.Add(new Food("burger", 2.00M, SpriteHolder.instance.GetFoodArtFromIDNumber(0)));
        itemsToCook.Add(new Food("egg", 1.00M, SpriteHolder.instance.GetFoodArtFromIDNumber(1)));
        PawnSprite = SpriteHolder.instance.GetBuildingArtFromIDNumber(0);
        EntityType = EnumHolder.EntityType.CookingStation;
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
        return foodOnGrill;
    }

    public void CreateFood(Food itemToCook)
    {
        foodOnGrill = itemToCook;
        ShowCoaster(foodOnGrill.CaryableObjectSprite, x => ItemCoaster = x);
    }

    public override void GetTargeter(Character character)
    {
        SpaceContextualActions.Clear();

        if (foodOnGrill != null && character.CariedObject == null)
            SpaceContextualActions.Add(new TakeItem(this, character));

        else if (foodOnGrill == null)
        {
            foreach(Food food in itemsToCook)
            {
                SpaceContextualActions.Add(new CookFood(this, food));
            }  
        }

    }

    public void GetRidOfItem()
    {
        foodOnGrill = null;
        HideCoaster(ItemCoaster);
    }
}
