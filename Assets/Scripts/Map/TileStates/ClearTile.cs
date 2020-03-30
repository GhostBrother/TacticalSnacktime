using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearTile : iTileState
{

    Tile _tile;

    public ClearTile()
    {
    }

    public ClearTile(Tile tile)
    {
        _tile = tile;
    }

    public void ChangeColor()
    {

    }

    public void TileClicked()
    {
        // Does nothing when clicked, might revert state of board? 
    }
}
