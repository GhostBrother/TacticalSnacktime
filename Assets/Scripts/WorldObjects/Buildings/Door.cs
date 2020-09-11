using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : AbstractInteractablePawn
{

    public Door()
    {
        EntityType = EnumHolder.EntityType.Door;
        characterArt = SpriteHolder.instance.GetBuildingArtFromIDNumber(2);
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
