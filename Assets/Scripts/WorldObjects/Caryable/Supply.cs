using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supply : AbstractInteractablePawn, iCanGiveItems, iCaryable 
{

    public int HandsRequired { get; set; }

    public int Weight { get; set; }

    public Sprite CaryableObjectSprite { get; private set; }

    public Food FoodThisSupplyMakes { get; private set; }

    public List<Command> HeldObjectCommands { get; private set; }

    public int NumberOfItemsInSupply { get; set; }

    public Supply(Food foodThisSupplyMakes, Sprite caryableObjectSprite) : this(foodThisSupplyMakes, caryableObjectSprite, 1)
    {

    }

    public Supply(Food foodThisSupplyMakes, Sprite caryableObjectSprite, int numberOfFoodLeftInSupply) 
    {
        HeldObjectCommands = new List<Command>();
        FoodThisSupplyMakes = foodThisSupplyMakes;
        Name = foodThisSupplyMakes.Name;
        CaryableObjectSprite = PawnSprite = caryableObjectSprite;
        NumberOfItemsInSupply = numberOfFoodLeftInSupply;
    }

    public iCaryable Give(int i)
    {
        return this;
    }

    public void GetRidOfItem(int i)
    {
            HideCoaster(characterCoaster);
            TilePawnIsOn.ChangeState(TilePawnIsOn.GetClearState());
    }

    public override List<Command> GetCommands()
    {
        return SpaceContextualActions;
    }

    public override void GetTargeter(Character character)
    {
        SpaceContextualActions.Clear();
        SpaceContextualActions.Add(new TakeItem(this, character, 0));
    }
}
