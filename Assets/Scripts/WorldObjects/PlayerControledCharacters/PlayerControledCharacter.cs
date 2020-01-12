using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayercontrolledCharacter : Character , iCanGiveItems
{

    public Action<PlayercontrolledCharacter> PutCharacterBack; 

    public PlayercontrolledCharacter()
    {
        EntityType = EnumHolder.EntityType.Character;
    }

    public void GetRidOfItem(int i )
    {
            usedHands--;
            cariedObjects.RemoveAt(i);
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

          for(int i = 0; i < character.cariedObjects.Count; i++)
                SpaceContextualActions.Add(new GiveItem(giver, this,i));


            for (int j = 0; j < cariedObjects.Count; j++)
                SpaceContextualActions.Add(new TakeItem(this, character, j));
        }
    }

    public override void TurnStart()
    {
        ResetMoveValue();
        onStartTurn.Invoke(this);
    }

    public override void OnEndDay()
    {
        GetRidOfAllItems();
        PutCharacterBack.Invoke(this);
        base.OnEndDay();
    }

    void GetRidOfAllItems()
    {
        for(int i = 0; i < cariedObjects.Count; i++)
        {
            GetRidOfItem(i);  
        }
    }
}
