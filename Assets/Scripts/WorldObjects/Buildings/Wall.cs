﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : AbstractPawn
{
    public Wall()
    {
        PawnSprite = SpriteHolder.instance.GetBuildingArtFromIDNumber(4);
        EntityType = EnumHolder.EntityType.Wall;
    }
}
