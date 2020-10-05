using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supply : AbstractInteractablePawn, iCanGiveItems, iCaryable 
{
    public int HandsRequired { get; set; }

    public Sprite CaryableObjectSprite { get; private set; }

    public Food FoodThisSupplyMakes { get; set; }

    public List<Command> HeldObjectCommands { get; private set; }

    public int NumberOfItemsInSupply { get; set; }
    
    // Don't know if this is nessisary. It's a SOLID violation for sure
    public List<iCaryable> cariedObjects { get; }

    public Supply()  
    {
        HeldObjectCommands = new List<Command>();
        CaryableObjectSprite = characterArt = SpriteHolder.instance.GetSupplyBox();
        EntityType = EnumHolder.EntityType.Supply;
    }

    public iCaryable Give(int i)
    {
        return Copy();
    }

    public void GetRidOfItem(int i)
    {
        NumberOfItemsInSupply--;
        if (NumberOfItemsInSupply == 0)
        {
            HideCoaster(characterCoaster);
            TilePawnIsOn.EntityTypeOnTile = EnumHolder.EntityType.None;
            TilePawnIsOn.DeactivateTile();
           
        }

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
        Name = $"Box of {FoodThisSupplyMakes.Name}"; 
    }

    public iCaryable Copy()
    {
       return FoodThisSupplyMakes.Copy();
    }
}
