using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AICharacter : Character
{

    Tile target; 
    Tile[] path;
    int targetIndex;
    Food desiredFood;
    public int Satisfaction { get; private set; }
    List<IDesireState> Desires;

    public Action<AICharacter> OnExit;
    public Action<decimal> OnPay;

    public bool OrderHasBeenTaken { get; set; }
    public override bool NeedsRemoval { get { return Desires.Count == 0; } set { } }

    public AICharacter()
    {
        targetIndex = 0;
        OrderHasBeenTaken = false;
       
        Desires = new List<IDesireState> { new FindRegister(this), new OrderFood(this), new FindExit(this) };
    }

    public void ChooseWhatToEat(Food food)
    {
        desiredFood = food; 
    }

    public void setTarget(Tile _target)
    {
        target = _target;
    }

    public void CheckPath()
    {
        for (int i = 0; i < Desires.Count; ++i)
        {
            if (Desires[i].isRequestSatisfied())
            {
                Desires.RemoveAt(i);
                i--;
            }
        }
       
        if (Desires.Count <= 0) { return; }
        Desires[0].MoveTarget();
       
    }

    public void Move()
    {
        if (path != null)
        {
            FollowPath();
        }
        else
            Debug.Log("Path is null");
    }

    public void OnPathFound(Tile[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            targetIndex = 0;
        }
    
    }

     void FollowPath()
    {
        if (targetIndex < path.Length) 
        {
            if (targetIndex + MoveSpeed >= (path.Length - 1))
            {
                targetIndex = (targetIndex + MoveSpeed) - (path.Length - 1); 
                if (targetIndex < 0) { targetIndex = 0; }
            }
            else
                targetIndex = MoveSpeed;
        }
        walkBack(targetIndex);

    }

    void walkBack(int targetIndex)
    {
        if (path[targetIndex].EntityTypeOnTile == EnumHolder.EntityType.None || path[targetIndex] == TilePawnIsOn) 
        {
            TilePawnIsOn.EntityTypeOnTile = EnumHolder.EntityType.None;
            TilePawnIsOn.DeactivateTile();
            characterCoaster.OnStopMoving = AILookForAction;
            TilePawnIsOn = path[targetIndex];
            TilePawnIsOn.EntityTypeOnTile = EnumHolder.EntityType.Character;
            PathRequestManager.RequestPath(PreviousTile, TilePawnIsOn, characterCoaster.MoveAlongPath);
            targetIndex = 0;
        }
        else
        {  
            walkBack(targetIndex - 1);
        }

    }

        public void DisplayOrder()
    {
        // linq stuff
        if (Desires.OfType<OrderFood>().Any())
        {
            const int xCordinateOffset = -1;
            const int yCordinateOffset = 1;

            HideCoaster(NeedCoaster);
            HideCoaster(FoodWantCoaster);
            ShowCoasterWithOffset(SpriteHolder.instance.GetTextBubble(), xCordinateOffset, yCordinateOffset, x => NeedCoaster = x);
            ShowCoasterWithOffset(SpriteHolder.instance.GetFoodArtFromIDNumber(desiredFood.ID), xCordinateOffset, yCordinateOffset, x => FoodWantCoaster = x);
        }

    }

    public override List<Command> GetCommands()
    {
       return SpaceContextualActions;
    }

    public override void GetTargeter(Character character)
    {
        SpaceContextualActions.Clear();

        if (character is iCanGiveItems)
        {
            for (int i = 0; i < character.cariedObjects.Count; i++)
            {
                iCanGiveItems giver = (iCanGiveItems)character;
                SpaceContextualActions.Add(new GiveItem(character, i)); 
            }
        }

    }

    public void AssessQuality()
    {
      for(int i = 0; i < cariedObjects.Count; i++)
        {
            if (cariedObjects[i] is Food)
            {
                Food weighedFood = (Food)cariedObjects[i];

                // is order correct
                if (weighedFood.ID == desiredFood.ID)
                {
                    Satisfaction += 10;
                }
                else
                    Satisfaction += 5; 

                // if weighed food is the same type 

               // if doneness scale is in the middle 

            // time it takes to get food. 

            // Price? 

            // flare? 

            // Give em a pop up that shows how the customer feels. 
            }
        }
    }

    public void PayForFood()
    {
        OnPay.Invoke(desiredFood.Price);
    }

    public override void TurnStart()
    {
        // put path find here? 
        onStartTurn.Invoke(this);
    }

    public void MoveCharacter()
    {
        CheckPath();
        Move();  
    }

     private void AILookForAction(Tile tile)
    {
       onTurnEnd.Invoke();   
    }

    public override List<Command> LoadCommands()
    {
        return null;
    }

    public override List<Command> GetAllActionsFromTile()
    {
        // TODO, Give the ai a weighted choice of what to do based on tile, even if it is wait paitently. 
        return null;
    }
}
