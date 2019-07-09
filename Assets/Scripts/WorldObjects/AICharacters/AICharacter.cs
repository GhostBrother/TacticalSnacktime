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
        Desires = new List<IDesireState> { new FindRegister(this), new OrderFood(this), new FindExit(this), new LeaveResturant(this)};
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
                Debug.Log("Removing " + Desires[i].ToString());
                Desires.RemoveAt(i);
                i--;
            }
            else
            {
                Debug.Log("Coulden't find " + Desires[i].ToString());
                break;
            }
        }

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
                targetIndex = (path.Length - (targetIndex + MoveSpeed + 1));
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
