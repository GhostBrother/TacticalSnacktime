using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    private int _baseMoveSpeed;

    public int MoveSpeed { get { return _baseMoveSpeed; } }

    public Sprite CharacterSprite { get; private set; }

    private Color canActColor;
    public Color CanActColor { get { return canActColor; } private set { canActColor = value; } }

    private Color cannotActColor;
    public Color CannotActColor { get { return cannotActColor; } private set { cannotActColor = value; } }

    public Character(int baseMoveSpeed, Sprite characterSprite)
    {
        _baseMoveSpeed = baseMoveSpeed;
        CharacterSprite = characterSprite;
        canActColor = new Color(225, 225, 255, 225);
        cannotActColor = new Color(.5f, .5f, .5f, 225);
    }


    
}
