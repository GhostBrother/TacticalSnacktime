using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : AbstractPawn
{
    private int _baseMoveSpeed;

    public int MoveSpeed { get { return _baseMoveSpeed; } }

    public int SpeedStat { get; private set; }

    iCaryable cariedObject;

   
    public Character(int baseMoveSpeed, Sprite characterSprite, int speedStat)
    {
        _baseMoveSpeed = baseMoveSpeed;
        PawnSprite = characterSprite;
        SpeedStat = speedStat;
    }

    public void CharacterMove()
    {
        TilePawnIsOn.ColorAllAdjacent(MoveSpeed);
    }

    public void PickUp(iCaryable caryable)
    {
        cariedObject = caryable;

    }
}
