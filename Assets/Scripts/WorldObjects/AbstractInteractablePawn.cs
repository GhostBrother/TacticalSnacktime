using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractInteractablePawn : AbstractPawn, iTargetable
{

    public override Tile TilePawnIsOn
    {
        get { return base.TilePawnIsOn; }
        set
        {
            base.TilePawnIsOn = value;
            base.TilePawnIsOn.TargetableOnTile = this;
        }
    }

    public abstract Command GetCommand();

    public abstract void GetTargeter(Character character);
 
}
