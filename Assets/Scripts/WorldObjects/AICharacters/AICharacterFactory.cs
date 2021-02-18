using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Move this into map maker when waves are a thing? Refactor this
public class AICharacterFactory 
{
    MonoPool _monoPool;
    DesireContainer _desireContainer;
    List<AICharacter> _charactersForDay;
    CustomerScheduler _customerScheduler;

    public AICharacterFactory(MonoPool monoPool)
    {
        _monoPool = monoPool;
        _desireContainer = new DesireContainer();
        _customerScheduler = new CustomerScheduler();
        _charactersForDay = new List<AICharacter>();
    }

    public List<AICharacter> GetCharacterSpawnsForTime(TimeSpan time, Tile targetTile)
    {
        List<AICharacter> _charactersForTime = new List<AICharacter>(_charactersForDay.FindAll(x => x.ArrivalTime == time));

        for(int i = 0; i < _charactersForTime.Count; i++)
        {
            setUpCharacter(_charactersForTime[i], targetTile);
        }

        return _charactersForTime;
    }

    AICharacter setUpCharacter(AICharacter aICharacter , Tile targetTile)
    {
        aICharacter.ChooseWhatToEat(_desireContainer.chooseWhatToEatBasedOnTaste(aICharacter.Race));
        aICharacter.characterCoaster = _monoPool.GetCharacterCoasterInstance();
        aICharacter._monoPool = _monoPool;
        aICharacter.characterCoaster.SetArtForFacing(EnumHolder.Facing.Up);

        if (targetTile.EntityTypeOnTile == EnumHolder.EntityType.Clear)
        {
            aICharacter.TilePawnIsOn = targetTile;
            aICharacter.TilePawnIsOn.EntityTypeOnTile = EnumHolder.EntityType.Character;
        }

        else
            for (int i = 0; i < targetTile.neighbors.Count; i++)
            {
                if (targetTile.neighbors[i].EntityTypeOnTile == EnumHolder.EntityType.Clear)
                {
                    aICharacter.TilePawnIsOn = targetTile.neighbors[i];
                    aICharacter.TilePawnIsOn.EntityTypeOnTile = EnumHolder.EntityType.Character;
                    break;
                }
            }

        return aICharacter;
    }

    public void RollCharactersForDay()
    {
        _charactersForDay.Clear();
        _charactersForDay = _customerScheduler.MakeListOfCustomers();
    }
}
