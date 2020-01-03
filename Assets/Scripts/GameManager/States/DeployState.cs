using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployState : iGameManagerState
{
     GameManager _gameManager;
    CharacterRoster _characterRoster;

    public DeployState(GameManager gameManager, CharacterRoster characterRoster)
    {
        _gameManager = gameManager;
        _characterRoster = characterRoster;

        LoadDisplayWithCharacterArt(characterRoster.PeekAtNextCharacter());
    }

    public void RightClick(Tile tile)
    {

    }

    public void TileClicked(Tile tile)
    {
        if(tile.GetCurrentState() == tile.GetDeployState())
        {
            PlayercontrolledCharacter CharacterToUse = _characterRoster.GetCharacterOnTopOfList();
            CharacterToUse.characterCoaster = _gameManager.monoPool.GetCharacterCoasterInstance();
            CharacterToUse._monoPool = _gameManager.monoPool;
            CharacterToUse.TilePawnIsOn = tile;
            _gameManager.AddPlayerControlledCharacterToList(CharacterToUse);
        }

        if (_characterRoster.IsListEmpty())
        {
            _gameManager.SortList();
            _gameManager.SetState(_gameManager.GetIdleState());
            _gameManager.DeactivateAllTiles();
            _gameManager.StartNextCharactersTurn();
        }

        else
        LoadDisplayWithCharacterArt(_characterRoster.PeekAtNextCharacter());
    }

    private void LoadDisplayWithCharacterArt(Character characterToDisplay)
    {
      _gameManager.characterDisplay.ChangeCharacterArt(characterToDisplay.PawnSprite);
    }
 
}
