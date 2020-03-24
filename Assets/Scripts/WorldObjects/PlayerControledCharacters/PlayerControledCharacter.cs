using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayercontrolledCharacter : Character , iCanGiveItems
{

    public Action<PlayercontrolledCharacter> PutCharacterBack;

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
        if (character is PlayercontrolledCharacter)
        {
            PlayercontrolledCharacter giver = (PlayercontrolledCharacter)character;

          for(int i = 0; i < character.cariedObjects.Count; i++)
                SpaceContextualActions.Add(new GiveItem(giver, this,i));


            for (int j = 0; j < cariedObjects.Count; j++)
                SpaceContextualActions.Add(new TakeItem(this, character, j));
            
        }
    }

    public override void MoveCharacter()
    {
        TilePawnIsOn.ChangeState(TilePawnIsOn.GetClearState());
        PathRequestManager.RequestPath(PreviousTile, TilePawnIsOn, characterCoaster.MoveAlongPath);
    }

    public override void TurnStart()
    {
        ResetMoveValue();
        onStartTurn.Invoke(this);
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
        SpaceContextualActions.Add(new MoveCommand(this));
        SpaceContextualActions.Add(new Act(GetAllActionsFromTile(), SpaceContextualActions));
        SpaceContextualActions.Add(new Wait(this));
        SpaceContextualActions.Add(new StatusCommand(this));
        return SpaceContextualActions;
    }

    public override List<Command> GetAllActionsFromTile()
    {
        List<Command> ListToReturn = new List<Command>();
        SpaceContextualActions.Clear();
        foreach (Tile neighbor in TilePawnIsOn.neighbors)
        {
            if (neighbor.IsTargetableOnTile)
            {
                neighbor.TargetableOnTile.GetTargeter(this);

               for(int i = 0; i < neighbor.TargetableOnTile.GetCommands().Count; i++)
                {
                    if(!ListToReturn.Exists(x => x.GetType() == neighbor.TargetableOnTile.GetCommands()[i].GetType()))
                    {
                        if(neighbor.TargetableOnTile.GetCommands()[i].typeOfCommand == null)
                        {
                            TransferMenuCommand temp = new TransferMenuCommand(SpaceContextualActions);
                            neighbor.TargetableOnTile.GetCommands()[i].typeOfCommand = temp;
                        }
                        ListToReturn.Add(neighbor.TargetableOnTile.GetCommands()[i]);
                    }
                }
            }
            
        }
        ListToReturn.AddRange(CariedObjectCommands);
        return ListToReturn;
    }


}
