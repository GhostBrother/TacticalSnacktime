using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerLoader : JsonLoader<AICharacter>
{
    List<AICharacter> customers;
    public CustomerLoader()
    {
        Init("Assets/JsonWaves/CustomerWaves.json");
        customers = GetObjectListFromFilePathByString("Races");
    }

    public AICharacter GetCustomerByType(string Type)
    {

        
        for (int i = 0; i < customers.Count; i++)
        {
            if (Type == customers[i].Race)
            {
                AICharacter characterToReturn;
                characterToReturn = (AICharacter)Clone<AICharacter>(customers[i]);
                characterToReturn.Name = customers[i].Name;
                characterToReturn.MoveSpeed = customers[i].MoveSpeed;
                characterToReturn.SpeedStat = customers[i].SpeedStat;
                characterToReturn.ID = customers[i].ID;
                characterToReturn.PawnSprite = SpriteHolder.instance.GetCharacterArtFromIDNumber(characterToReturn.ID);
                characterToReturn.Race = customers[i].Race;
                return characterToReturn;
            }
        }
        return null;
        
    }

    private Character Clone<T>(Character characterToClone) where T : Character , new()
    {
        return new T();
    }


}
