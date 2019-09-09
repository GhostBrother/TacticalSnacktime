using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : AbstractInteractablePawn
{

    public int MoveSpeed { get; private set; }

    private bool needsRemoval;
    public virtual bool NeedsRemoval { get { return needsRemoval; } set { needsRemoval = value; } }

    // for now, our employees can hold 2 one handed items ( a burger and a dagger) or 1 two handed object ( A mop or a greatsword) 
    int numberOfHands = 2;

    int usedHands = 0; 

    protected List<iCaryable> cariedObjects;
    public List<iCaryable> CariedObjects { get { return cariedObjects; } }

    List<Command> _cariedObjectCommands;
    public List<Command> CariedObjectCommands
    {
        get
        {
            _cariedObjectCommands.Clear();
            if (cariedObjects.Count > 0)
            {
                for (int i = 0; i < cariedObjects.Count; i++)
                {
                    foreach (Command c in cariedObjects[i].HeldObjectCommands)
                    {
                        _cariedObjectCommands.Add(c);
                    }
                }
            }
            return _cariedObjectCommands;
        }
            
    }


    public Character(int baseMoveSpeed, Sprite characterSprite, int speedStat , string name)
    {
        MoveSpeed = baseMoveSpeed;
        PawnSprite = characterSprite;
        TurnOrder = speedStat;
        EntityType = EnumHolder.EntityType.None;
        Name = name;
        needsRemoval = false;
        _cariedObjectCommands = new List<Command>();
        cariedObjects = new List<iCaryable>();
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
            cariedObjects.Add(caryable);
            ShowCoaster(caryable.CaryableObjectSprite, x => ItemCoaster = x);
        }

    }

    public override abstract List<Command> GetCommands();

    public override abstract void GetTargeter(Character character);

}
