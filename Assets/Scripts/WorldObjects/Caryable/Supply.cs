using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supply : AbstractInteractablePawn, iContainCaryables //, iCaryable  // iCanGiveItems
{
    public int HandsRequired { get; set; }

    public Sprite CaryableObjectSprite { get; private set; }

   // public Food FoodThisSupplyMakes { get; set; }

    public List<Command> HeldObjectCommands { get; private set; }

    //public int NumberOfItemsInSupply { get; set; }
    
    public List<iCaryable> cariedObjects { get; set; }

    public int numberOfCarriedObjects => 4;

    public Supply()  
    {
        cariedObjects = new List<iCaryable>();
        HeldObjectCommands = new List<Command>();
        CaryableObjectSprite = characterArt = SpriteHolder.instance.GetSupplyBox();
        EntityType = EnumHolder.EntityType.Supply;
    }

    public iCaryable Give(int i)
    {
        return null;
        //return Copy();
    }

    public void GetRidOfItem(int i)
    {
        //NumberOfItemsInSupply--;
        //if (NumberOfItemsInSupply == 0)
        //{
        //    HideCoaster(characterCoaster);
        //    TilePawnIsOn.EntityTypeOnTile = EnumHolder.EntityType.Clear;
        //    TilePawnIsOn.DeactivateTile();
           
        //}
        if(cariedObjects.Count == 0)
        {
            HideCoaster(characterCoaster);
            TilePawnIsOn.EntityTypeOnTile = EnumHolder.EntityType.Clear;
            TilePawnIsOn.DeactivateTile();
        }
    }

    public override List<Command> GetCommands()
    {
        return SpaceContextualActions;
    }

    public override void GetTargeter(Character character)
    {
        if (character is PlayercontrolledCharacter)
        {
            PlayercontrolledCharacter temp = (PlayercontrolledCharacter)character;

            SetName();
            SpaceContextualActions.Clear();
            SpaceContextualActions.Add(new TradeItemCommand(temp)); 
        }

    }

    public void SetName()
    {
        Name = $"Box of items"; 
    }

    //public iCaryable Copy()
    //{
    //   return FoodThisSupplyMakes.Copy();
    //}
}
