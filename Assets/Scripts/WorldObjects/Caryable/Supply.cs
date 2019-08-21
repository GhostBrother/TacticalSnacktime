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

    public Supply(Food foodThisSupplyMakes, Sprite caryableObjectSprite)
    {
        HeldObjectCommands = new List<Command>();
        FoodThisSupplyMakes = foodThisSupplyMakes;
        Name = foodThisSupplyMakes.Name;
        CaryableObjectSprite = PawnSprite = caryableObjectSprite;
    }

    public iCaryable Give()
    {
        return this;
    }

    public void GetRidOfItem()
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
        SpaceContextualActions.Add(new TakeItem(this, character));
    }
}
