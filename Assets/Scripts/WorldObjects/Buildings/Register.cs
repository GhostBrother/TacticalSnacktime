﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Register : AbstractInteractablePawn 
{
    AICharacter customer;
    public Register()
    {
        characterArt = SpriteHolder.instance.GetBuildingArtFromIDNumber(3);
        EntityType = EnumHolder.EntityType.Register;
    }
    public override List<Command> GetCommands()
    {
        return SpaceContextualActions;
    }

    public override void GetTargeter(Character _character)
    {
        SpaceContextualActions.Clear();
 

        for(int i = 0; i < TilePawnIsOn.neighbors.Count; i++)
        {
            if (TilePawnIsOn.neighbors[i].TargetableOnTile is AICharacter)
            {
                
                customer = (AICharacter)TilePawnIsOn.neighbors[i].TargetableOnTile;


                 if(!customer.OrderHasBeenTaken)
                 SpaceContextualActions.Add(new TakeOrder(_character ,customer));
            }
        }
      
    }

}
