using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
///  This is the state where a game piece is moving or doing some other action
///  The point is to freeze click input and allow the unit to move and get to it's final destination. 
/// </summary>
public class ControlsDisabled : iGameManagerState
{
    public void RightClick(Tile tile)
    {

    }

    public void TileClicked(Tile tile)
    {

    }
}
