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

    public void RightClick(Tile tile)
    {

    }

    public void TileClicked(Tile tile)
    {
        if(tile.GetCurrentState() == tile.GetDeployState())
        {
            PlayercontrolledCharacter CharacterToUse = characterRoster.GetCharacterOnTopOfList();
            CharacterToUse.characterCoaster = CharacterCoasterPool.Instance.SpawnFromPool();
            CharacterToUse.TilePawnIsOn = tile;
            _gameManager.AddPlayerControlledCharacterToList(CharacterToUse);
        }

        if (characterRoster.IsListEmpty())
        {
            _gameManager.SortList();
            _gameManager.SetState(_gameManager.GetIdleState());
            _gameManager.DeactivateAllTiles();
            _gameManager.StartNextCharactersTurn();
        }

        else
        LoadDisplayWithCharacterArt(characterRoster.PeekAtNextCharacter());
    }

    private void LoadDisplayWithCharacterArt(Character characterToDisplay)
    {
      _gameManager.characterDisplay.ChangeCharacterArt(characterToDisplay.PawnSprite);
    }

    
}
