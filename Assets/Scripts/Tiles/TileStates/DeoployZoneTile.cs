using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployZoneTile : iTileState
{
    Tile _tile;
    public DeployZoneTile(Tile tile)
    {
        _tile = tile;
    }

    public void ChangeColor()
    {
        _tile.BackgroundTile.color = _tile.DeployZoneColor;
    }


    public void TileClicked()
    {
        
    }
}
