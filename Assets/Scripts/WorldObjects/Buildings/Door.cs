using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : AbstractPawn // iTargetable?
{

    public Door()
    {
        EntityType = EnumHolder.EntityType.Door;
        PawnSprite = SpriteHolder.instance.GetBuildingArtFromIDNumber(1);
    }

}
