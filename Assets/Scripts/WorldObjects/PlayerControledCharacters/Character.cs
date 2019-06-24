using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : AbstractPawn
{
    int _baseMoveSpeed;

    public int MoveSpeed { get { return _baseMoveSpeed; } }

    public int SpeedStat { get; private set; }

    // for now, our employees can hold 2 one handed items ( a burger and a dagger) or 1 two handed object ( A mop or a greatsword) 
    int numberOfHands = 2;

    int usedHands = 0; 

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
        usedHands += caryable.HandsRequired;
        if (usedHands < numberOfHands)
        {
            cariedObject = caryable;
            ItemSprite = cariedObject.CaryableObjectSprite;
            ShowItem();
        }

    }
}
