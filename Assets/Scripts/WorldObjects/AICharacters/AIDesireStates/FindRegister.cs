using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindRegister : IDesireState
{
    AICharacter aICharacter;
    public FindRegister(AICharacter _aICharacter)
    {
        aICharacter = _aICharacter;
    }

    public void MoveTarget()
    {
        PathRequestManager.RequestPath(aICharacter.TilePawnIsOn, PathRequestManager.FindClosestEntityOfType(aICharacter.TilePawnIsOn, EnumHolder.EntityType.Register), aICharacter.OnPathFound);
    }

    public bool isRequestSatisfied()
    {
        for (int i = 0; i < aICharacter.TilePawnIsOn.neighbors.Count; i++)
        {
            if (aICharacter.TilePawnIsOn.neighbors[i].EntityTypeOnTile == EnumHolder.EntityType.Register)
            {
                return true;
            }
        }

        return false;
    }
}
