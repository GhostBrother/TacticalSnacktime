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
    

    public void GetRidOfItem(int i )
    {
        cariedObjects[i].NumberOfItemsInSupply--;
        if (cariedObjects[i].NumberOfItemsInSupply < 1)
        {
            usedHands--;
            cariedObjects.RemoveAt(i);
        }
    }

    public iCaryable Give(int i)
    {
        return cariedObjects[i];
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

          for(int i = 0; i < character.CariedObjects.Count; i++)
                SpaceContextualActions.Add(new GiveItem(giver, this,i));


           for (int j = 0; j < cariedObjects.Count; j++)
                SpaceContextualActions.Add(new TakeItem(this, character, j));
        }
    }

    public override void TurnStart()
    {
        onStartTurn.Invoke(this);
    }
}
