﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRoster {

     public List<PlayercontrolledCharacter> employedCharacters { get; }

    CharacterLoader<PlayercontrolledCharacter> _characterLoader; 

    public CharacterRoster()
    {
        employedCharacters = new List<PlayercontrolledCharacter>();
        _characterLoader = new CharacterLoader<PlayercontrolledCharacter>();

        employedCharacters.Add(_characterLoader.GetCharacterByType("Kobold"));
        employedCharacters.Add(_characterLoader.GetCharacterByType("Kobold"));
        employedCharacters.Add(_characterLoader.GetCharacterByType("Theif"));
        employedCharacters.Add(_characterLoader.GetCharacterByType("Ghost"));
    }

    public List<PlayercontrolledCharacter> GetCharactersForTime(string time)
    {
        List<PlayercontrolledCharacter> _charactersForTime = new List<PlayercontrolledCharacter>(employedCharacters.FindAll(x => x.ArrivalTime == time && x.IsGoingToWork));
        Debug.Log("UH" + employedCharacters[0].ArrivalTime);
        return _charactersForTime;
    }



    //public Character getNextEmployedCharacter()
    //{
    //    PlayercontrolledCharacter temp = PeekAtNextCharacter();
    //    employedCharacters.Remove(temp);
    //    employedCharacters.Add(temp);
    //    return PeekAtNextCharacter();
    //}

    //public Character getPreviousEmployedCharacter()
    //{
    //    PlayercontrolledCharacter temp = employedCharacters[employedCharacters.Count -1];
    //    employedCharacters.RemoveAt(employedCharacters.Count -1);
    //    employedCharacters.Insert(0, temp);
    //    return PeekAtNextCharacter();
    //}


    public bool IsListEmpty()
    {
        return employedCharacters.Count == 0;
    }

    public void AddCharacterBackToList(PlayercontrolledCharacter characterToAdd)
    {
        employedCharacters.Add(characterToAdd);
    }

}
