using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractInteractablePawn : AbstractPawn, iTargetable
{
    protected List<Command> SpaceContextualActions;

    public AbstractInteractablePawn()
    {
        SpaceContextualActions = new List<Command>();
    }

    public override Tile TilePawnIsOn
    {
        get { return base.TilePawnIsOn; }
        set
        {
            base.TilePawnIsOn = value;
            base.TilePawnIsOn.TargetableOnTile = this;
        }
    }

    public abstract List<Command> GetCommands();

    public abstract void GetTargeter(Character character);
 
}
