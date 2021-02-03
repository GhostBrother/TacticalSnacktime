using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindExit : IDesireState
{
    AICharacter aICharacter;
    public FindExit(AICharacter _aICharacter)
    {
        aICharacter = _aICharacter;
    }
    public void MoveTarget()
    {
        PathRequestManager.RequestPath(aICharacter.TilePawnIsOn, PathRequestManager.FindClosestEntityOfType(aICharacter.TilePawnIsOn, EnumHolder.EntityType.Door), aICharacter.OnPathFound);
    }

    public bool isRequestSatisfied()
    {
        if (aICharacter.cariedObjects.Count > 0)
        {
            for (int i = 0; i < aICharacter.TilePawnIsOn.neighbors.Count; i++)
            {
                if (aICharacter.TilePawnIsOn.neighbors[i].EntityTypeOnTile == EnumHolder.EntityType.Door)
                {
                    aICharacter.OnExit.Invoke(aICharacter);
                    return true;
                }
            }
        }

        return false;
    }
}
