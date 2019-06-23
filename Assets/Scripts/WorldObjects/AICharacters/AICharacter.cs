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
    }

    public void setTarget(Tile _target)
    {
        target = _target;
    }

    public void CheckPath()
    {
        PathRequestManager.RequestPath(TilePawnIsOn, target, OnPathFound);
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
            TilePawnIsOn.ChangeState(TilePawnIsOn.GetClearState());
            if (targetIndex + MoveSpeed >= path.Length)
            {
                targetIndex = (path.Length - (targetIndex + MoveSpeed));
            }
            else
                targetIndex += MoveSpeed;


            TilePawnIsOn = path[targetIndex];
            
        }
    }


}
