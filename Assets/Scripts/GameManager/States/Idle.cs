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
        //if ( tile == _gameManager.CurentCharacter.TilePawnIsOn)
        //{
        //    _gameManager.ActionMenu.ShowActionsAtTile(_gameManager.CurentCharacter);
        //    _gameManager.SetState(_gameManager.GetSelectedState());
        //}
        //else
        //{
        //    _gameManager.DeactivateAllTiles();
        //    _gameManager.SetState(_gameManager.GetIdleState());
        //}
    }
}
