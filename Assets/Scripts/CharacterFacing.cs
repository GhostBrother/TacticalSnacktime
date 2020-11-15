

public class CharacterFacing
{



   public EnumHolder.Facing determineFacing(Tile previousWaypoint, Tile nextWaypoint)
    {
        if (previousWaypoint.GridX > nextWaypoint.GridX)
        {
            return EnumHolder.Facing.Left;
        }

        if (previousWaypoint.GridX < nextWaypoint.GridX)
        {
            return EnumHolder.Facing.Right;
        }

        if (previousWaypoint.GridY > nextWaypoint.GridY)
        {
            return EnumHolder.Facing.Up;
        }

        else
            return EnumHolder.Facing.Down;

    }
}
