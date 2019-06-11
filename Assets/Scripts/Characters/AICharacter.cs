using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacter : Character
{

    private Tile target; // wasPublic
    Tile[] path;
    int targetIndex;
    public AICharacter(int baseMoveSpeed, Sprite characterSprite, int speedStat, Tile _target) : base(baseMoveSpeed, characterSprite, speedStat)
    {

        //For testing only
        target = _target;
        targetIndex = 0;
    }

    public void CheckPath()
    {
        PathRequestManager.RequestPath(tileCharacterIsOn, target, OnPathFound);
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
 
        }

    }

     void FollowPath()
    {
        if (targetIndex < path.Length)
        {
            tileCharacterIsOn.ChangeState(tileCharacterIsOn.GetClearState());
            if (targetIndex + MoveSpeed >= path.Length)
            {
                targetIndex = (path.Length - (targetIndex + MoveSpeed));
            }
            else
                targetIndex += MoveSpeed;

            tileCharacterIsOn = path[targetIndex];
            tileCharacterIsOn.ChangeState(tileCharacterIsOn.GetActiveState());
            ColorTile();
        }
    }


}
