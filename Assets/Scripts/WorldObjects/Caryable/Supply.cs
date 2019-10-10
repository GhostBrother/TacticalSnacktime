using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supply : AbstractInteractablePawn, iCanGiveItems, iCaryable 
{
    public string NameOfFoodInSupply { get; set; }

    public int HandsRequired { get; set; }

    public Sprite CaryableObjectSprite { get; private set; }

    public Food FoodThisSupplyMakes { get; set; }

    public List<Command> HeldObjectCommands { get; private set; }

    public int NumberOfItemsInSupply { get; set; }


    public Supply()  // Food foodThisSupplyMakes, Sprite caryableObjectSprite, int numberOfFoodLeftInSupply
    {
        HeldObjectCommands = new List<Command>();
        CaryableObjectSprite = PawnSprite = SpriteHolder.instance.GetSupplyBox();
        SetName();
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
        SetName();
        SpaceContextualActions.Clear();
        SpaceContextualActions.Add(new PickUpItem(this, character, 0));
    }

    public void SetName()
    {
        Name = $"Box of {NumberOfItemsInSupply} {NameOfFoodInSupply} "; //{NumberOfItemsInSupply} {FoodThisSupplyMakes.Name}
    }
}
