using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelected : iGameManagerState
{

    GameManager _gameManager;
    public TileSelected(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void NextArrow()
    {
    
    }

    public void PrevArrow()
    {
        
    }

    public void TileClicked(Tile tile)
    {
        _gameManager.KeepTrackOfEndTile(tile);
        _gameManager.DeactivateAllTiles();
        _gameManager.SetState(_gameManager.GetIdleState());
    }
}
