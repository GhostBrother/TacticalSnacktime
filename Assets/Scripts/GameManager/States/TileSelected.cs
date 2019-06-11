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
        if (tile.GetCurrentState() == tile.GetHilightedState())
        {
            _gameManager.KeepTrackOfEndTile(tile);
            _gameManager.DeactivateAllTiles();
            _gameManager.MoveFirstCharacterToLast();
        }
        else
        {
            _gameManager.DeactivateAllTiles();
        }

        //Hack for demo
        // This should move at the same time as out human player.
        if ((AICharacter)_gameManager.GetNextCharacter() != null)
        {
            AICharacter tempChar = (AICharacter)_gameManager.GetNextCharacter();
            tempChar.CheckPath();
            tempChar.Move();
            _gameManager.MoveFirstCharacterToLast();
        }

        _gameManager.SetState(_gameManager.GetIdleState());

    }
}
