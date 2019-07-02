using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : AbstractPawn
{

    public Door()
    {
        EntityType = EnumHolder.EntityType.Door;
        PawnSprite = SpriteHolder.instance.GetBuildingArtFromIDNumber(1);
    }

    public override Command GetCommand()
    {
        return null;
    }

    public override void GetTargeter(Character character)
    {
       
    }
}
