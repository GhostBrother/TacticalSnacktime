using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacter : Character
{

    Tile target; 
    Tile[] path;
    int targetIndex;

    public AICharacter(int baseMoveSpeed, Sprite characterSprite, int speedStat) : base(baseMoveSpeed, characterSprite, speedStat)
    {
        targetIndex = 0;
        EntityType = EnumHolder.EntityType.None;
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
            PathRequestManager.RequestPath(TilePawnIsOn, PathRequestManager.FindClosestEntityOfType(TilePawnIsOn, EnumHolder.EntityType.Character), OnPathFound);
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


}
