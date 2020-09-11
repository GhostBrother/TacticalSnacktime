﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeDoor : AbstractInteractablePawn
{
   public EmployeeDoor()
    {
        EntityType = EnumHolder.EntityType.EmployeeDoor;
        characterArt = SpriteHolder.instance.GetBuildingArtFromIDNumber(5);
    }

    public override List<Command> GetCommands()
    {
        return SpaceContextualActions;
    }

    public override void GetTargeter(Character character)
    {
        SpaceContextualActions.Clear();
        SpaceContextualActions.Add(new Exit(character));
    }
}
