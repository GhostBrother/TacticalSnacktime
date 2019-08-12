using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControledCharacter : Character , iCanGiveItems
{
    Command characterCommand;

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

    public override Command GetCommand()
    {
        return characterCommand;
    }

    public override void GetTargeter(Character character)
    {
        characterCommand = null;
        if (character is PlayerControledCharacter)
        {
           
            PlayerControledCharacter giver = (PlayerControledCharacter)character;
            // Checks to see if we have something to give. 
            if (character.CariedObject != null)
                characterCommand = new GiveItem(giver, this);
            else if (cariedObject != null && character.CariedObject == null)
                characterCommand = new TakeItem(this, character);
            else
                characterCommand = null;
        }
    }
}
