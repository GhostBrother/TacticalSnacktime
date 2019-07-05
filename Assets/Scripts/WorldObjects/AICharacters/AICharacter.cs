using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacter : Character
{

    Tile target; 
    Tile[] path;
    int targetIndex;
    Food desiredFood;
    List<IDesireState> Desires;

    public AICharacter(int baseMoveSpeed, Sprite characterSprite, int speedStat, Food _desiredFood) : base(baseMoveSpeed, characterSprite, speedStat)
    {
        targetIndex = 0;
        EntityType = EnumHolder.EntityType.None;
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
        // will be traded out for states after test
        //if(!isTargetFound)
        //PathRequestManager.RequestPath(TilePawnIsOn, target, OnPathFound);
        //else
        if(Desires[0].isRequestSatisfied())
        {
            Desires.RemoveAt(0);
        }
        Desires[0].MoveTarget();
        //Tile TileTest = PathRequestManager.FindClosestEntityOfType(TilePawnIsOn, EnumHolder.EntityType.Character); 
        //Debug.Log("X" + TileTest.GridX + " : Y " + TileTest.GridY + "Status " + TileTest.EntityTypeOnTile );
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
            if (targetIndex + MoveSpeed >= path.Length)
            {
                targetIndex = (path.Length - (targetIndex + MoveSpeed));
            }
            else
                targetIndex += MoveSpeed;

            if (path[targetIndex].GetCurrentState() != path[targetIndex].GetActiveState())
            {
                TilePawnIsOn.ChangeState(TilePawnIsOn.GetClearState());
                TilePawnIsOn = path[targetIndex]; 
            }

        }
    }

    public void DisplayOrder()
    {
        Debug.Log(desiredFood.Name);
    }

}
