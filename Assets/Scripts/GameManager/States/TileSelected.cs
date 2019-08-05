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

    public void RightClick(Tile tile)
    {
        _gameManager.DeactivateAllTiles();
        _gameManager.SetState(_gameManager.GetIdleState());
    }

    public void TileClicked(Tile tile)
    {
        if (tile.GetCurrentState() == tile.GetHilightedState() || tile == _gameManager.CurentCharacter.TilePawnIsOn)
        {

            
            foreach (Tile neighbor in tile.neighbors)
            {
                if (neighbor.IsTargetableOnTile)
                {
                    neighbor.TargetableOnTile.GetTargeter(_gameManager.CurentCharacter);

                    if (neighbor.TargetableOnTile.GetCommand() != null)
                    _gameManager.ActionMenu.AddCommandToList(neighbor.TargetableOnTile.GetCommand());
                }
            }

            _gameManager.CurentCharacter.characterCoaster.onStopMoving = ActionOnStopMoving;
            _gameManager.CurentCharacter.TilePawnIsOn = tile;
            tile.ChangeState(tile.GetActiveState());
            _gameManager.DeactivateAllTiles();
            _gameManager.SetState(_gameManager.GetMovingState());
        }
        else
        {
            _gameManager.DeactivateAllTiles();
            _gameManager.SetState(_gameManager.GetIdleState());
        }
    }

    private void ActionOnStopMoving(Tile tile)
    {
        _gameManager.ActionMenu.ShowActionsAtTile(tile);
        _gameManager.SetState(_gameManager.GetActionState());
    }
}
