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

        //Hack for demo
        // This should move at the same time as out human player;
        while (_gameManager.GetNextCharacter() is AICharacter)
        {
            AICharacter tempChar = (AICharacter)_gameManager.GetNextCharacter();
            tempChar.CheckPath();
            tempChar.Move();
            _gameManager.MoveFirstCharacterToLast();
        }

        _gameManager.SetState(_gameManager.GetIdleState());
    }

}
