using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacterFactory : MonoBehaviour
{

    public AICharacter SpawnCharacterAt(Tile targetTile)
    {
        //int rand = Random.Range(0, 6);

        //switch (rand)
       //{
           // case 1:
               AICharacter aICharacter = new AICharacter(1, SpriteHolder.instance.GetArtFromIDNumber(3), 2);
               aICharacter.tileCharacterIsOn = targetTile;
               aICharacter.ColorTile();
               aICharacter.tileCharacterIsOn.ChangeState(aICharacter.tileCharacterIsOn.GetActiveState());
               return aICharacter;
               // break;
               //default:
               // break;
               // }
               // All for test
               //AICharacter aICharacter = new AICharacter(1, SpriteHolder.instance.GetArtFromIDNumber(3), 2, _gameMap.GetTileAtIndex(13));
               //aICharacter.tileCharacterIsOn = _gameMap.GetTileAtIndex(0);
               //aICharacter.tileCharacterIsOn.ChangeState(aICharacter.tileCharacterIsOn.GetActiveState());
               //aICharacter.ColorTile();
               //charactersOnMap.Add(aICharacter);

        //AICharacter aICharacter2 = new AICharacter(3, SpriteHolder.instance.GetArtFromIDNumber(3), 4, _gameMap.GetTileAtIndex(24));
        //aICharacter2.tileCharacterIsOn = _gameMap.GetTileAtIndex(10);
        //aICharacter2.tileCharacterIsOn.ChangeState(aICharacter2.tileCharacterIsOn.GetActiveState());
        //aICharacter2.ColorTile();
        //charactersOnMap.Add(aICharacter2);
    }
}
