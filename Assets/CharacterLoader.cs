using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLoader<T> : JsonLoader<T> where T : Character
{

    protected List<T> characters;
    public CharacterLoader()
    {
        Init("Assets/JsonWaves/CustomerWaves.json");
        characters = GetObjectListFromFilePathByString("Races");
    }

    public T GetCharacterByType<T>(string Type) where T : Character, new()
    {
        T characterToReturn = null;
        for (int i = 0; i < characters.Count; i++)
        {
            if (Type == characters[i].Race)
            {
                characterToReturn = new T();
                characterToReturn.Name = characters[i].Name;
                characterToReturn.MoveSpeed = characters[i].MoveSpeed;
                characterToReturn.SpeedStat = characters[i].SpeedStat;
                characterToReturn.ID = characters[i].ID;
                characterToReturn.PawnSprite = SpriteHolder.instance.GetCharacterArtFromIDNumber(characterToReturn.ID);
                characterToReturn.Race = characters[i].Race;

            }
        }

        return characterToReturn;

    }

}
