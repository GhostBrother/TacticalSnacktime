using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControledCharacter : Character , iCanGiveItems
{

    public PlayerControledCharacter (int baseMoveSpeed, Sprite characterSprite, int speedStat) : base(baseMoveSpeed, characterSprite, speedStat)
    {
        EntityType = EnumHolder.EntityType.Character;
    }
    

    public void GetRidOfItem()
    {
        cariedObject = null;
        HideCoaster(ItemCoaster);
    }

    public iCaryable Give()
    {
        return cariedObject;
    }

    public override List<Command> GetCommands()
    {
        return SpaceContextualActions;
    }

    public override void GetTargeter(Character character)
    {
        SpaceContextualActions.Clear();
        if (character is PlayerControledCharacter)
        {
            PlayerControledCharacter giver = (PlayerControledCharacter)character;
            // Checks to see if we have something to give. 
            if (character.CariedObject != null)
                SpaceContextualActions.Add(new GiveItem(giver, this));
            else if (cariedObject != null && character.CariedObject == null)
                SpaceContextualActions.Add(new TakeItem(this, character));
        }
    }
}
