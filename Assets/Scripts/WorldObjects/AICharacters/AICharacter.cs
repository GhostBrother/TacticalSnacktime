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
    List<IDesireState> Desires;

    public override bool NeedsRemoval { get { return Desires.Count == 0; } set { } }

    public AICharacter(int baseMoveSpeed, Sprite characterSprite, int speedStat, Food _desiredFood, string _name) : base(baseMoveSpeed, characterSprite, speedStat, _name )
    {
        targetIndex = 0;
        desiredFood = _desiredFood;

        // Temporary 
        Desires = new List<IDesireState> { new FindRegister(this), new OrderFood(this), new FindExit(this)};
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
                for (int j = i; j >= 0; j --)
                {
                    Desires.RemoveAt(j);
                }
            }
        }

        if (Desires.Count <= 0) { return; }
        Desires[0].MoveTarget();
    }

    public void Move()
    {
        if(path != null)
        {
            FollowPath();
        }
    }

    public void OnPathFound(Tile[] newPath, bool pathSuccessful)
    {
        if(pathSuccessful)
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
                targetIndex = (path.Length  - (targetIndex + MoveSpeed + 1) );
                if (targetIndex < 0) { targetIndex = 0; }
            }
            else
                targetIndex += MoveSpeed;


            if (path[targetIndex].GetCurrentState() != path[targetIndex].GetActiveState())
            {
                TilePawnIsOn.ChangeState(TilePawnIsOn.GetClearState());
                TilePawnIsOn = path[targetIndex];
            }
            else
            {
                TilePawnIsOn = TilePawnIsOn;
            }
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
            ShowCoasterWithOffset(SpriteHolder.instance.GetFoodArtFromIDNumber(0), xCordinateOffset, yCordinateOffset, x => FoodWantCoaster = x);
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
            for (int i = 0; i < character.CariedObjects.Count; i++)
            {
                iCanGiveItems giver = (iCanGiveItems)character;
                SpaceContextualActions.Add(new GiveItem(giver, this, i));
            }
        }

    }

    public override void TurnStart()
    {
        onStartTurn.Invoke(this);
        CheckPath();
        Move();
        characterCoaster.onStopMoving = AILookForAction;
    }


     private void AILookForAction(Tile tile)
    {
        // TODO, Give the ai a weighted choice of what to do based on tile, even if it is wait paitently. 
        onTurnEnd.Invoke();
    }
}
