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
        //_tile.GetComponent<SpriteRenderer>().sprite = _tile.CharacterOnTile.CharacterSprite;
        //_tile.GetComponent<SpriteRenderer>().color = _tile.CharacterOnTile.CanActColor;
    }

    public void TileClicked()
    {
        //_tile.ColorAllAdjacent(_tile.CharacterOnTile.MoveSpeed);
    }

}
