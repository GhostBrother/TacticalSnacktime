using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionState : iGameManagerState
{

    GameManager _gameManager;

    public ActionState(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void NextArrow()
    {
       // throw new System.NotImplementedException();
    }

    public void PrevArrow()
    {
       // throw new System.NotImplementedException();
    }

    public void TileClicked(Tile tile)
    {
    
    }

}
