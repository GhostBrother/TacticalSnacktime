using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayercontrolledCharacter : Character , iCanGiveItems
{
    // At some point I want name to be randomly chosen from a jason pool ( common, species and class specific) 
    public PlayercontrolledCharacter(int baseMoveSpeed, Sprite characterSprite, int speedStat, string name) : base(baseMoveSpeed, characterSprite, speedStat, name)
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
        if (character is PlayercontrolledCharacter)
        {
            PlayercontrolledCharacter giver = (PlayercontrolledCharacter)character;
            // Checks to see if we have something to give. 
            if (character.CariedObject != null)
                SpaceContextualActions.Add(new GiveItem(giver, this));
            else if (cariedObject != null && character.CariedObject == null)
                SpaceContextualActions.Add(new TakeItem(this, character));
        }
    }

    public override void TurnStart()
    {
        onStartTurn.Invoke(this);
    }
}
