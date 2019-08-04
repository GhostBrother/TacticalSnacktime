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
        if (tile.GetCurrentState() == tile.GetHilightedState() || tile == _gameManager.GetNextCharacter().TilePawnIsOn)
        {

            
            foreach (Tile neighbor in tile.neighbors)
            {
                if (neighbor.IsTargetableOnTile)
                {
                    neighbor.TargetableOnTile.GetTargeter(_gameManager.GetNextCharacter());

                    if (neighbor.TargetableOnTile.GetCommand() != null)
                    _gameManager.ActionMenu.AddCommandToList(neighbor.TargetableOnTile.GetCommand());
                }
            }

            _gameManager.KeepTrackOfEndTile(tile);
            _gameManager.DeactivateAllTiles();
            _gameManager.SetState(_gameManager.GetActionState());
        }
        else
        {
            _gameManager.DeactivateAllTiles();
            _gameManager.SetState(_gameManager.GetIdleState());
        }
    }
}
