using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployState : iGameManagerState
{
    private GameManager _gameManager;
    private CharacterRoster characterRoster;

    public DeployState(GameManager gameManager)
    {
        _gameManager = gameManager;
        characterRoster = new CharacterRoster();

        LoadDisplayWithCharacterArt(characterRoster.PeekAtNextCharacter());
    }

    public void NextArrow()
    {
        LoadDisplayWithCharacterArt(characterRoster.getNextEmployedCharacter());
    }

    public void PrevArrow()
    {
        LoadDisplayWithCharacterArt(characterRoster.getPreviousEmployedCharacter()); 
    }

    public void TileClicked(Tile tile)
    {
        if(tile.GetCurrentState() == tile.GetDeployState())
        {
            Character CharacterToUse = characterRoster.GetCharacterOnTopOfList();
            CharacterToUse.TileCharacterIsOn = tile;
            _gameManager.AddCharacterToList(CharacterToUse);
        }

        if (characterRoster.IsListEmpty())
        {
            _gameManager.SortList();
            _gameManager.SetState(_gameManager.GetIdleState());
            _gameManager.DeactivateAllTiles();
        }

        else
        LoadDisplayWithCharacterArt(characterRoster.PeekAtNextCharacter());
    }

    private void LoadDisplayWithCharacterArt(Character characterToDisplay)
    {
      _gameManager.characterDisplay.ChangeCharacterArt(characterToDisplay.CharacterSprite);
    }

    
}
