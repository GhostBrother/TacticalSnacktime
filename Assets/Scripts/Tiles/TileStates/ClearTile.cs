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
        _tile.EntityTypeOnTile = EnumHolder.EntityType.None;
    }

    public void ChangeColor()
    {
        _tile.GetComponent<SpriteRenderer>().sprite = null;
        _tile.BackgroundTile.color = _tile.DeactiveColor;
        _tile.FoodImageRenderer.sprite = null;

    }

    public void TileClicked()
    {
        
    }
}
