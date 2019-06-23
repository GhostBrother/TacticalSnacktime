using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacterFactory : MonoBehaviour
{

    public AICharacter SpawnCharacterAt(Tile targetTile)
    {
        AICharacter aICharacter = new AICharacter(1, SpriteHolder.instance.GetCharacterArtFromIDNumber(3), 2);
        aICharacter.TilePawnIsOn = targetTile;
        return aICharacter;
    }
}
