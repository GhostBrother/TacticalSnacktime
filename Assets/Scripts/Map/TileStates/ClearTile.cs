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
        //_tile.GetComponent<SpriteRenderer>().sprite = null;
        //_tile.FoodImageRenderer.sprite = null;

    }

    public void TileClicked()
    {
        
    }
}
