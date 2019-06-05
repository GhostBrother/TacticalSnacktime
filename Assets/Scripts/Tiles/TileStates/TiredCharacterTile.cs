using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiredCharacterTile : iTileState
{
    private Tile _tile;
    public TiredCharacterTile(Tile tile)
    {
        _tile = tile;
    }

    public void ChangeColor()
    {
        _tile.GetComponent<SpriteRenderer>().sprite = _tile.CharacterOnTile.CharacterSprite;
        _tile.GetComponent<SpriteRenderer>().color = _tile.CharacterOnTile.CannotActColor;
        _tile.BackgroundTile.color = _tile.DeactiveColor;
    }

    public void TileClicked()
    {
        
    }
}
