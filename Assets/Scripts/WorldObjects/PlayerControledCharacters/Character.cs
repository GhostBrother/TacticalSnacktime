using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : AbstractInteractablePawn , iContainCaryables , iAffectedByTime
{

    public int MoveSpeed { get; set; }
    public string Race { get;  set; }
    public int _MoveRemaining { get; set; }
    private bool needsRemoval;
    public virtual bool NeedsRemoval { get { return needsRemoval; } set { needsRemoval = value; } }

    public TimeSpan ArrivalTime { get; set; }

    public TimeSpan LeaveTime { get; set; }

    // hack, Should only apply to Player controled character
    public Money payPerHour { get; private set; }

    public decimal DefaultPay { get { return payPerHour.valueToStore; } set { payPerHour.valueToStore = value;} }
    // End hack

    protected int usedHands = 0; 

    public List<iCaryable> cariedObjects { get; protected set; }


    private int _numberOfCarriedObjects;
    public int numberOfCarriedObjects { get { return 2; } protected set { _numberOfCarriedObjects = value; } }

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

    public Action<iAffectedByTime> AddToTimeline { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public Action<AbstractPawn> RemoveFromTimeline { get; set; }
    //Added
    public Action<AbstractPawn> onStartTurn { get ; set; }
    public Action onTurnEnd { get; set; }

    public Character()
    {
        needsRemoval = false;
        _cariedObjectCommands = new List<Command>();
        cariedObjects = new List<iCaryable>();
        payPerHour = new Money(); 
    }

    public abstract List<Command> LoadCommands();

    protected void ResetMoveValue()
    {
        _MoveRemaining = MoveSpeed;
    }

    public void PickUp(iCaryable caryable)
    {
        for (int i = 0; i < cariedObjects.Count; i++)
        {
            if (cariedObjects[i].Name == caryable.Name) 
            {
                cariedObjects[i].NumberOfItemsInSupply += caryable.NumberOfItemsInSupply;
                return;
            }
        }
        usedHands += caryable.HandsRequired;
        cariedObjects.Add(caryable);
    }

    public override abstract List<Command> GetCommands();


    public abstract List<Command> GetAllActionsFromTile();

    public override abstract void GetTargeter(Character character);

    public virtual void OnEndDay()
    {
        //TilePawnIsOn.ChangeState(TilePawnIsOn.GetClearState());
        TilePawnIsOn.DeactivateTile();
        RemovePawn(characterCoaster);
        RemoveFromTimeline.Invoke(this);
    }
}
