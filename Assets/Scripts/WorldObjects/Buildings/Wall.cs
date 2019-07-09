using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : AbstractPawn
{
    public Wall()
    {
        PawnSprite = SpriteHolder.instance.GetBuildingArtFromIDNumber(3);
        EntityType = EnumHolder.EntityType.Wall;
    }

    public override Command GetCommand()
    {
        // at some point, attack will be a command so walls can break;
        return null;
    }

    public override void GetTargeter(Character character)
    {
        
    }
}
