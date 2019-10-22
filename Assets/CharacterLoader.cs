using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLoader : JsonLoader<PlayercontrolledCharacter>
{

    List<PlayercontrolledCharacter> characters;
    public CharacterLoader()
    {
        Init("Assets/JsonWaves/CustomerWaves.json");
        characters = GetObjectListFromFilePathByString("Races");
    }

    public PlayercontrolledCharacter GetCharacterByType(string Type)
    {
       
        for (int i = 0; i < characters.Count; i++)
        {
            PlayercontrolledCharacter characterToReturn;
            if (Type == characters[i].Race)
            {
                characterToReturn = (PlayercontrolledCharacter)Clone<PlayercontrolledCharacter>(characters[i]);
                characterToReturn.Name = characters[i].Name;
                characterToReturn.MoveSpeed = characters[i].MoveSpeed;
                characterToReturn.SpeedStat = characters[i].SpeedStat;
                characterToReturn.ID = characters[i].ID;
                characterToReturn.PawnSprite = SpriteHolder.instance.GetCharacterArtFromIDNumber(characterToReturn.ID);
                characterToReturn.Race = characters[i].Race;
                return characterToReturn;
            }
        }

        return null;
        
    }

    private Character Clone<T>(Character characterToClone) where T : Character, new()
    {
        return new T();
    }
}
