using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRoster {

    List<Character> employedCharacters;

    Sprite KoboldSprite;
    Sprite TheifSprite;
    Sprite DragonSprite;

    public CharacterRoster()
    {
        employedCharacters = new List<Character>();
        KoboldSprite = SpriteHolder.instance.GetArtFromIDNumber(0);
        TheifSprite = SpriteHolder.instance.GetArtFromIDNumber(1);
        DragonSprite = SpriteHolder.instance.GetArtFromIDNumber(2);

        employedCharacters.Add(new Character(1,  KoboldSprite,1));
        //employedCharacters.Add(new Character(1,  KoboldSprite,1));
        //employedCharacters.Add(new Character(1, KoboldSprite,1));

        employedCharacters.Add(new Character(3,  TheifSprite,2));

    }

    public Character PeekAtNextCharacter()
    {
        return employedCharacters[0];
    }

    public Character getNextEmployedCharacter()
    {
        Character temp = PeekAtNextCharacter();
        employedCharacters.Remove(temp);
        employedCharacters.Add(temp);
        return PeekAtNextCharacter();
    }

    public Character getPreviousEmployedCharacter()
    {
        Character temp = employedCharacters[employedCharacters.Count -1];
        employedCharacters.RemoveAt(employedCharacters.Count -1);
        employedCharacters.Insert(0, temp);
        return PeekAtNextCharacter();
    }

    public Character GetCharacterOnTopOfList()
    {
        Character temp = PeekAtNextCharacter();
        employedCharacters.Remove(temp);
        return temp;
    }

    public bool IsListEmpty()
    {
        return employedCharacters.Count == 0;
    }

}
