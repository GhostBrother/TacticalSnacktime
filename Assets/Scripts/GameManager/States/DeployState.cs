﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployState : iGameManagerState
{
     GameManager _gameManager;
    CharacterRoster _characterRoster;
    List<PlayercontrolledCharacter> _CharactersAtStart;

    public DeployState(GameManager gameManager, CharacterRoster characterRoster)
    {
        _gameManager = gameManager;
        _characterRoster = characterRoster;
       // LoadDisplayWithCharacterArt(_CharactersAtStart[0]);
    }

    public void RightClick(Tile tile)
    {

    }

    public void TileClicked(Tile tile)
    {
        if(tile.curentState == tile.GetDeployState())
        {
            PlayercontrolledCharacter CharacterToUse = _CharactersAtStart[0];
            CharacterToUse.characterCoaster = _gameManager.monoPool.GetCharacterCoasterInstance();
            CharacterToUse._monoPool = _gameManager.monoPool;
            CharacterToUse.TilePawnIsOn = tile;
            _gameManager.AddPlayerControlledCharacterToList(CharacterToUse);
            _CharactersAtStart.Remove(CharacterToUse);
        }

        if (_CharactersAtStart.Count == 0)
        {
            _gameManager.SortList();
            _gameManager.SetState(_gameManager.GetIdleState());
            _gameManager.DeactivateAllTiles();
            _gameManager.StartNextCharactersTurn();
        }

        else
        LoadDisplayWithCharacterArt(_CharactersAtStart[0]);
    }

    public void SetOpeningStaff(string Time)
    {
        _CharactersAtStart = _characterRoster.GetCharactersForTime(Time);
    }

    private void LoadDisplayWithCharacterArt(Character characterToDisplay)
    {
      _gameManager.characterDisplay.ChangeCharacterArt(characterToDisplay.PawnSprite);
    }

 
}
