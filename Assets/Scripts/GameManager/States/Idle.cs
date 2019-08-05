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

    public void RightClick(Tile tile)
    {
        // Info about tile or object on tile. 
    }

    public void TileClicked(Tile tile)
    {
        if (_gameManager.CurentCharacter.TilePawnIsOn == tile)
        {
            _gameManager.CurentCharacter.ShowMoveRange();
            tile.ChangeState(tile.GetClearState());
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
