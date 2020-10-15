using System;
using System.Collections;
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

    public List<PlayercontrolledCharacter> GetCharactersForTime(TimeSpan time)
    {
        List<PlayercontrolledCharacter> _charactersForTime = new List<PlayercontrolledCharacter>(employedCharacters.FindAll(x => x.ArrivalTime == time && x.IsGoingToWork));
        employedCharacters.RemoveAll(x => x.ArrivalTime == time && x.IsGoingToWork);
        return _charactersForTime;
    }

    public bool IsListEmpty()
    {
        return employedCharacters.Count == 0;
    }

    public void AddCharacterBackToList(PlayercontrolledCharacter characterToAdd)
    {
        employedCharacters.Add(characterToAdd);
    }

}
