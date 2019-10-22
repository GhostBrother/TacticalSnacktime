using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRoster {

    List<PlayercontrolledCharacter> employedCharacters;

    CharacterLoader _characterLoader; 

    public CharacterRoster()
    {
        employedCharacters = new List<PlayercontrolledCharacter>();
        _characterLoader = new CharacterLoader();

        employedCharacters.Add(_characterLoader.GetCharacterByType("Kobold"));
        employedCharacters.Add(_characterLoader.GetCharacterByType("Theif"));
        employedCharacters.Add(_characterLoader.GetCharacterByType("Ghost"));
    }

    public PlayercontrolledCharacter PeekAtNextCharacter()
    {
        return employedCharacters[0];
    }

    public Character getNextEmployedCharacter()
    {
        PlayercontrolledCharacter temp = PeekAtNextCharacter();
        employedCharacters.Remove(temp);
        employedCharacters.Add(temp);
        return PeekAtNextCharacter();
    }

    public Character getPreviousEmployedCharacter()
    {
        PlayercontrolledCharacter temp = employedCharacters[employedCharacters.Count -1];
        employedCharacters.RemoveAt(employedCharacters.Count -1);
        employedCharacters.Insert(0, temp);
        return PeekAtNextCharacter();
    }

    public PlayercontrolledCharacter GetCharacterOnTopOfList()
    {
        PlayercontrolledCharacter temp = PeekAtNextCharacter();
        employedCharacters.Remove(temp);
        return temp;
    }

    public bool IsListEmpty()
    {
        return employedCharacters.Count == 0;
    }

}
