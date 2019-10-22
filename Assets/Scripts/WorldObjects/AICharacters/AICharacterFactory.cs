using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Move this into map maker when waves are a thing?
public class AICharacterFactory 
{
    MonoPool _monoPool;
    DesireContainer _desireContainer;
    CustomerLoader _customerLoader;

    public AICharacterFactory(MonoPool monoPool)
    {
        _monoPool = monoPool;
        _desireContainer = new DesireContainer();
        _customerLoader = new CustomerLoader();
    }

    public AICharacter SpawnCharacterAt(Tile targetTile)
    {

        AICharacter aICharacter = _customerLoader.GetCustomerByType("Dragon");
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
}
