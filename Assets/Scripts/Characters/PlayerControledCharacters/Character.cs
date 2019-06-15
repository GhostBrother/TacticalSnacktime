using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    private int _baseMoveSpeed;

    public int MoveSpeed { get { return _baseMoveSpeed; } }

    public int SpeedStat { get; private set; }

    public Sprite CharacterSprite { get; private set; }

    private Tile tileCharacterIsOn;
    public Tile TileCharacterIsOn
    {
        get { return tileCharacterIsOn; }
        set {
            tileCharacterIsOn = value;
            tileCharacterIsOn.ChangeState(tileCharacterIsOn.GetActiveState());
            ColorTile();
        }
    }

    public enum Size { small,med, big };
    public Size thisUnitsSize { get; private set; }

    public Character(int baseMoveSpeed, Sprite characterSprite, int speedStat)
    {
        _baseMoveSpeed = baseMoveSpeed;
        CharacterSprite = characterSprite;
        SpeedStat = speedStat;
    }

    public void CharacterMove()
    {
        tileCharacterIsOn.ColorAllAdjacent(MoveSpeed);
    }

    private void ColorTile()
    {
        tileCharacterIsOn.GetComponent<SpriteRenderer>().sprite = CharacterSprite;
        tileCharacterIsOn.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 1);
    }


    
}
