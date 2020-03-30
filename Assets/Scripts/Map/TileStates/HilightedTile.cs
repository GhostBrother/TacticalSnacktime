using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HilightedTile : iTileState
{
    Tile _tile;

    public HilightedTile(Tile tile)
    {
        _tile = tile;
    }

    public void ChangeColor()
    {
        _tile.BackgroundTile.color = _tile.ActiveColor;
    }

    public void TileClicked()
    {
      // Do current action to execute, like moving the character
    }
}
