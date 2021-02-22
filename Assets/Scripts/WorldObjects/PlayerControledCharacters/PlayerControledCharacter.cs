using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayercontrolledCharacter : Character , iContainCaryables //iCanGiveItems
{

    public Action<PlayercontrolledCharacter> PutCharacterBack;

    public bool IsGoingToWork { get;  set; }


    public PlayercontrolledCharacter()
    {
        EntityType = EnumHolder.EntityType.Character;
    }


    public void GetRidOfItem(int i )
    {
        cariedObjects[i].NumberOfItemsInSupply--;
        if (cariedObjects[i].NumberOfItemsInSupply == 0)
        {
            usedHands--;
            cariedObjects.RemoveAt(i);
        }
    }

    public iCaryable Give(int i)
    {
        return cariedObjects[i].Copy();
    }

    public override List<Command> GetCommands()
    {
        return SpaceContextualActions;
    }

    public override void GetTargeter(Character character)
    {
    }

    public override void TurnStart()
    {
        ResetMoveValue();
        EntityType = EnumHolder.EntityType.Self;
        TilePawnIsOn.EntityTypeOnTile = EnumHolder.EntityType.Self;
        onStartTurn.Invoke(this);
    }

    public override void TurnEnd()
    {
        EntityType = EnumHolder.EntityType.Character;
        TilePawnIsOn = this.TilePawnIsOn;
    }

    public override void OnEndDay()
    {
        GetRidOfAllItems();
        PutCharacterBack.Invoke(this);
        base.OnEndDay();
    }

    void GetRidOfAllItems()
    {
        for(int i = 0; i < cariedObjects.Count; i++)
        {
            GetRidOfItem(i);  
        }
    }

    public override List<Command> LoadCommands()
    {
        List<Command> BaseCommands = new List<Command>();
        BaseCommands.Add(new MoveCommand(this));
        BaseCommands.Add(new Act(GetAllActionsFromTile(), LoadCommands));
        BaseCommands.Add(new Wait(this));
        BaseCommands.Add(new StatusCommand(this));
        return BaseCommands;
    }

    public override List<Command> GetAllActionsFromTile()
    {
        List<Command> ListToReturn = new List<Command>();
        foreach (Tile neighbor in TilePawnIsOn.neighbors)
        {
            if (neighbor.IsTargetableOnTile)
            {

                neighbor.TargetableOnTile.GetTargeter(this);

                for (int i = 0; i < neighbor.TargetableOnTile.GetCommands().Count; i++)
                {
                   
                    if (!ListToReturn.Exists(x => x.GetType() == neighbor.TargetableOnTile.GetCommands()[i].GetType()))
                    {
                        if (neighbor.TargetableOnTile.GetCommands()[i].typeOfCommand == null)
                        {
                            TransferMenuCommand temp = new TransferMenuCommand(LoadCommands);
                            neighbor.TargetableOnTile.GetCommands()[i].typeOfCommand = temp;
                        }
                        ListToReturn.Add(neighbor.TargetableOnTile.GetCommands()[i]);
                    }
                       
                }

                if(neighbor.TargetableOnTile is PlayercontrolledCharacter)
                {
                    PlayercontrolledCharacter temp = (PlayercontrolledCharacter)neighbor.TargetableOnTile;

                    if(temp.cariedObjects.Count > 0 || cariedObjects.Count > 0)
                    {
                        ListToReturn.Add(new TradeItemCommand(this, temp));
                    }
                }
               
            }
            
        }

        //if (isTargetableOnTile)
        //{
        //    ListToReturn.Add(new GiveItem(this, 0));  
        //}

        ListToReturn.AddRange(CariedObjectCommands);
        return ListToReturn;
    }


}
