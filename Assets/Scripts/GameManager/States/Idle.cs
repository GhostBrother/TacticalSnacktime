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
                foreach (Tile neighbor in tile.neighbors)
                {
                    if (neighbor.IsTargetableOnTile)
                    {

                        //HACK
                        if (neighbor.TargetableOnTile is Grill)
                        {
                            Grill G = (Grill)neighbor.TargetableOnTile;
                            G.TurnOrder = _gameManager.CurentCharacter.TurnOrder;
                            G.AddToTimeline = _gameManager.AddPawnToTimeline;
                            G.RemoveFromTimeline = _gameManager.RemovePawnFromTimeline;
                        }
                    }
                }

                _gameManager.SetState(_gameManager.GetMovingState());
                _gameManager.CurentCharacter.characterCoaster.onStopMoving = ActionOnStopMoving;
                _gameManager.CurentCharacter.TilePawnIsOn = tile;
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
            _gameManager.SetState(_gameManager.GetActionState());
        }

        //if (_gameManager.CurentCharacter.TilePawnIsOn == tile)
        //{
        //   // _gameManager.CurentCharacter.ShowMoveRange();
        //    //tile.ChangeState(tile.GetClearState());
        //   // _gameManager.SetState(_gameManager.GetSelectedState());
        //}


    public void NextArrow()
    {
        // Move camera to next ready character;

    }

    public void PrevArrow()
    {
        // Move camera to last ready character;
    }
}
