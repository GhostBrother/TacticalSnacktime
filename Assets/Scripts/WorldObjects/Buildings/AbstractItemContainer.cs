using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractItemContainer : AbstractInteractablePawn, iCanGiveItems, iContainCaryables
{
    public List<iCaryable> cariedObjects { get;  set; }

    public int numberOfCarriedObjects { get; set; }

    public AbstractItemContainer()  //TODO put this information into a json file and make this a class that cookingstation can inherit from. 
    {
        EntityType = EnumHolder.EntityType.Container;
        characterArt = SpriteHolder.instance.GetBuildingArtFromIDNumber(6);
        cariedObjects = new List<iCaryable>();
        numberOfCarriedObjects = 4; 
    }

    public override List<Command> GetCommands()
    {
        return SpaceContextualActions;
    }

    public void GetRidOfItem(int i)
    {
        cariedObjects.RemoveAt(i);
    }

    public override void GetTargeter(Character character)
    {
        SpaceContextualActions.Clear();

        if (character is iCanGiveItems)
        {
            for (int i = 0; i < character.cariedObjects.Count; i++)
            {
                iCanGiveItems giver = (iCanGiveItems)character;
                SpaceContextualActions.Add(new StoreItem(this.TilePawnIsOn,(iCanGiveItems)character, i));
            }
        }

        for (int i = 0; i < cariedObjects.Count; i++)
        {
            SpaceContextualActions.Add(new GetCookedFood(this, character, i));
        }

    }

    public iCaryable Give(int i)
    {
        return cariedObjects[i];
    }

}
