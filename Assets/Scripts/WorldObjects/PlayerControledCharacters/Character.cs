using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : AbstractPawn , iTargetable
{
    int _baseMoveSpeed;

    public int MoveSpeed { get { return _baseMoveSpeed; } }

    public int SpeedStat { get; private set; }

    private bool needsRemoval;
    public virtual bool NeedsRemoval { get { return needsRemoval; } set { needsRemoval = value; } }

    Command characterCommand;

    // for now, our employees can hold 2 one handed items ( a burger and a dagger) or 1 two handed object ( A mop or a greatsword) 
    int numberOfHands = 2;

    int usedHands = 0; 

    protected iCaryable cariedObject;

   
    public Character(int baseMoveSpeed, Sprite characterSprite, int speedStat)
    {
        _baseMoveSpeed = baseMoveSpeed;
        PawnSprite = characterSprite;
        SpeedStat = speedStat;
        EntityType = EnumHolder.EntityType.Character;
        needsRemoval = false;
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

    public void GetRidOfItem()
    {
        cariedObject = null;
        ItemSprite = null;
        HideItem();
    }

    public iCaryable Give()
    {
        return cariedObject;
    }

    public override Command GetCommand()
    {
        return characterCommand;
    }

    public override void GetTargeter(Character character)
    {
        characterCommand = null;
        // Checks to see if we have something to give. 
        if (character.Give() != null)
        characterCommand = new GiveItem(character, this);
    }
}
