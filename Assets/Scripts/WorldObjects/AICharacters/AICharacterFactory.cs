using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacterFactory : MonoBehaviour
{

    public AICharacter SpawnCharacterAt(Tile targetTile)
    {

        AICharacter aICharacter = new AICharacter(1, SpriteHolder.instance.GetCharacterArtFromIDNumber(3), 2, new Food("Cheeseburger", 2.00M, SpriteHolder.instance.GetFoodArtFromIDNumber(0)), "Dargon");
        aICharacter.characterCoaster = CharacterCoasterPool.Instance.SpawnFromPool();

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
