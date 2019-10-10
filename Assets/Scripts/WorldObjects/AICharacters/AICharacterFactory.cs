using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Move this into map maker when waves are a thing?
public class AICharacterFactory 
{
    MonoPool _monoPool;
    FoodLoader foodLoader;

    public AICharacterFactory(MonoPool monoPool)
    {
        _monoPool = monoPool;
        foodLoader = new FoodLoader();
    }

    public AICharacter SpawnCharacterAt(Tile targetTile)
    {

        AICharacter aICharacter = new AICharacter(1, SpriteHolder.instance.GetCharacterArtFromIDNumber(3), 2, foodLoader.GetFoodById("Burger"), "Dargon");
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
