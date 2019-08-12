using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : AbstractInteractablePawn
{

    public int MoveSpeed { get; private set; }

    public int SpeedStat { get; private set; }

    private bool needsRemoval;
    public virtual bool NeedsRemoval { get { return needsRemoval; } set { needsRemoval = value; } }

    // for now, our employees can hold 2 one handed items ( a burger and a dagger) or 1 two handed object ( A mop or a greatsword) 
    int numberOfHands = 2;

    int usedHands = 0; 

    protected iCaryable cariedObject;
    public iCaryable CariedObject { get { return cariedObject; } }

   
    public Character(int baseMoveSpeed, Sprite characterSprite, int speedStat)
    {
        MoveSpeed = baseMoveSpeed;
        PawnSprite = characterSprite;
        SpeedStat = speedStat;
        EntityType = EnumHolder.EntityType.None;
        needsRemoval = false;
    }

    public void ShowMoveRange()
    {
        TilePawnIsOn.ColorAllAdjacent(MoveSpeed);
    }

    public void PickUp(iCaryable caryable)
    {
        usedHands += caryable.HandsRequired;
        if (usedHands < numberOfHands)
        {
            cariedObject = caryable;
            ShowCoaster(cariedObject.CaryableObjectSprite, x => ItemCoaster = x);
        }

    }

    public override abstract Command GetCommand();

    public override abstract void GetTargeter(Character character);

}
