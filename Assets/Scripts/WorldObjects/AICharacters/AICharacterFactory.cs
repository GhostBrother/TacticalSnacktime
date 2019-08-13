using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacterFactory : MonoBehaviour
{

    public AICharacter SpawnCharacterAt(Tile targetTile)
    {
        AICharacter aICharacter = new AICharacter(1, SpriteHolder.instance.GetCharacterArtFromIDNumber(3), 2, new Food("Cheeseburger", 2.00M, SpriteHolder.instance.GetFoodArtFromIDNumber(0)), "Dargon");
        aICharacter.characterCoaster = CharacterCoasterPool.Instance.SpawnFromPool();
        aICharacter.TilePawnIsOn = targetTile;
        return aICharacter;
    }
}
