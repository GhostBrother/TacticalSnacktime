using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRoster {

    List<PlayercontrolledCharacter> employedCharacters;

    Sprite KoboldSprite;
    Sprite TheifSprite;
    Sprite DragonSprite;

    public CharacterRoster()
    {
        employedCharacters = new List<PlayercontrolledCharacter>();
        KoboldSprite = SpriteHolder.instance.GetCharacterArtFromIDNumber(0);
        TheifSprite = SpriteHolder.instance.GetCharacterArtFromIDNumber(1);
        DragonSprite = SpriteHolder.instance.GetCharacterArtFromIDNumber(2);

        employedCharacters.Add(new PlayercontrolledCharacter(1,  KoboldSprite,1, "Kobold"));
        

        employedCharacters.Add(new PlayercontrolledCharacter(3,  TheifSprite,3 , "Creepy Guy"));
        employedCharacters.Add(new PlayercontrolledCharacter(3, TheifSprite, 3, "Spooky Man"));
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
