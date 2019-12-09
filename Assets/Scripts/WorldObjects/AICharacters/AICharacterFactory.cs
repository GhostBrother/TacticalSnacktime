using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Move this into map maker when waves are a thing?
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
        _charactersForDay = _customerScheduler.MakeListOfCustomers();
    }

    public List<AICharacter> GetCharacterSpawnsForTime(string time, Tile targetTile)
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


        if (targetTile.GetCurrentState() != targetTile.GetActiveState())
        {
            aICharacter.TilePawnIsOn = targetTile;
        }

        else
            for (int i = 0; i < targetTile.neighbors.Count; i++)
            {
                if (targetTile.neighbors[i].GetCurrentState() != targetTile.neighbors[i].GetActiveState())
                {
                    aICharacter.TilePawnIsOn = targetTile.neighbors[i];
                    break;
                }
            }

        return aICharacter;
    }
    //public AICharacter SpawnCharacterAt(Tile targetTile)
    //{

    //    AICharacter aICharacter = _customerLoader.GetCharacterByType<AICharacter>("Dragon");
    //    aICharacter.ChooseWhatToEat(_desireContainer.chooseWhatToEatBasedOnTaste(aICharacter.Race));
    //    aICharacter.characterCoaster = _monoPool.GetCharacterCoasterInstance();
    //    aICharacter._monoPool = _monoPool;


    //    if (targetTile.GetCurrentState() != targetTile.GetActiveState())
    //    {
    //        aICharacter.TilePawnIsOn = targetTile;
    //    }

    //    else
    //        for (int i = 0; i < targetTile.neighbors.Count; i++)
    //        {
    //            if (targetTile.neighbors[i].GetCurrentState() != targetTile.neighbors[i].GetActiveState())
    //            {
    //                aICharacter.TilePawnIsOn = targetTile.neighbors[i];
    //                break;
    //            }
    //        }

    //    return aICharacter;

    //}
}
