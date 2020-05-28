using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OccupiedTile : iTileState
{

    Tile _tile;

    public OccupiedTile(Tile tile)
    {
        _tile = tile;
    }

    public void ChangeColor()
    {

    }

    public void TileClicked()
    {
        if(_tile.onClick != null)
        _tile.onClick.Invoke(_tile);
    }

}
