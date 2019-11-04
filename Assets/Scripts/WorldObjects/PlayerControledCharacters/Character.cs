using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : AbstractInteractablePawn
{

    public int MoveSpeed { get; set; }
    public string Race { get;  set; }
    public int SpeedStat { get; set; }
    public int _MoveRemaining { get; set; }

    private bool needsRemoval;
    public virtual bool NeedsRemoval { get { return needsRemoval; } set { needsRemoval = value; } }

    // for now, our employees can hold 2 one handed items ( a burger and a dagger) or 1 two handed object ( A mop or a greatsword) 
    int numberOfItemsCanCary = 2;
    public int NumberOfItemsCanCary { get { return numberOfItemsCanCary; }}

    protected int usedHands = 0; 

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

    public Character()
    {
        EntityType = EnumHolder.EntityType.None;
        needsRemoval = false;
        _cariedObjectCommands = new List<Command>();
        cariedObjects = new List<iCaryable>();
    }

    protected void ResetMoveValue()
    {
        _MoveRemaining = MoveSpeed;
    }

    public void ShowMoveRange()
    {
        TilePawnIsOn.ColorAllAdjacent(_MoveRemaining); 
    }

    public void PickUp(iCaryable caryable)
    {
        if (!cariedObjects.Contains(caryable))
        {
            usedHands += caryable.HandsRequired;
            cariedObjects.Add(caryable);
        }
    }

    public virtual void MoveCharacter()
    {

    }

    public override abstract List<Command> GetCommands();

    public override abstract void GetTargeter(Character character);

}
