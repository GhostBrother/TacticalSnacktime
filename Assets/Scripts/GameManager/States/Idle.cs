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
        if (tile.GetCurrentState() == tile.GetHilightedState() || tile == _gameManager.CurentCharacter.TilePawnIsOn)
        {

            _gameManager.CurentCharacter._MoveRemaining -= Mathf.Abs(tile.GridX - _gameManager.CurentCharacter.TilePawnIsOn.GridX);
            _gameManager.CurentCharacter._MoveRemaining -= Mathf.Abs(tile.GridY - _gameManager.CurentCharacter.TilePawnIsOn.GridY);

            _gameManager.CurentCharacter.characterCoaster.onStopMoving = ActionOnStopMoving;
            _gameManager.CurentCharacter.TilePawnIsOn = tile;
            // Added
            _gameManager.CurentCharacter.MoveCharacter();
            tile.ChangeState(tile.GetActiveState());
            _gameManager.DeactivateAllTiles();

        }
        else
        {
            _gameManager.DeactivateAllTiles();
            _gameManager.SetState(_gameManager.GetIdleState());
        }
    }

    private void ActionOnStopMoving(Tile tile)
    {
        _gameManager.ActionMenu.ShowActionsAtTile();
    }
}
