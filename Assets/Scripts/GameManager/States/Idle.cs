using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : iGameManagerState
{

    GameManager _gameManager;

    public Idle(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void TileClicked(Tile tile)
    {
        if (_gameManager.GetNextCharacter().TileCharacterIsOn == tile)
        {
            tile.SelectTile();
            _gameManager.KeepTrackOfStartTile(tile);
            _gameManager.SetState(_gameManager.GetSelectedState());
        }
    }

    public void NextArrow()
    {
        // Move camera to next ready character;

    }

    public void PrevArrow()
    {
        // Move camera to last ready character;
    }
}
