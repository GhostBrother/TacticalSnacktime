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

        if (character is PlayercontrolledCharacter)
        {
            PlayercontrolledCharacter giver = (PlayercontrolledCharacter)character;
            if (character.cariedObjects.Count > 0 || cariedObjects.Count > 0)
            {
                SpaceContextualActions.Add(new TradeItemCommand(giver, this));
            }
        }
    }

    public iCaryable Give(int i)
    {
        return cariedObjects[i];
    }

}
