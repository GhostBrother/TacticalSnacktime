using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private Map _gameMap;

    private List<Character> charactersOnMap;

    [SerializeField]
    private MapGenerator _mapGenerator;

    [SerializeField]
    CharacterDisplay _characterDisplay;

    [SerializeField]
    CameraController camera;

    public CameraController Camera { get { return camera; } private set { } }

    public CharacterDisplay characterDisplay { get { return _characterDisplay; } private set {; } }

    private InputHandler _inputHandler;

    iGameManagerState idleMode;
    iGameManagerState selectedMode;
    iGameManagerState deployState;
    iGameManagerState curentState;

    public GameManager()
    {
       
    }

	// Use this for initialization
	void Start ()
    {
        idleMode = new Idle(this);
        selectedMode = new TileSelected(this);
        deployState = new DeployState(this);

        curentState = deployState;

        charactersOnMap = new List<Character>();
        _gameMap = _mapGenerator.generateMap();
        _inputHandler = new InputHandler();
    }

    public iGameManagerState GetIdleState()
    {
        return idleMode;
    }

    public iGameManagerState GetSelectedState()
    {
        return selectedMode;
    }

    public void SetState(iGameManagerState newState)
    {
        curentState = newState;
    }

    public void NextButtonPressed()
    {
        curentState.NextArrow();
    }

    public void PrevButtonPressed()
    {
        curentState.PrevArrow();
    }

    public void AddCharacterToList(Character character)
    {
        charactersOnMap.Add(character);
    }

    public void ActivateTile(Tile tile)
    {
        curentState.TileClicked(tile);
    }

    public void DeactivateAllTiles()
    {
        _gameMap.DeactivateAllTiles();
    }

    public void SortList()
    {
        charactersOnMap.Sort((x, y) => x.SpeedStat.CompareTo(y.SpeedStat));
    }

    public Character GetNextCharacter()
    {
        return charactersOnMap[0];
    }

    public void MoveFirstCharacterToLast()
    {
        Character temp = GetNextCharacter();
        charactersOnMap.Remove(temp);
        charactersOnMap.Add(temp);
    }

    public void KeepTrackOfStartTile(Tile tile)
    {
        Debug.Log(tile);
        _gameMap.SetStartTile(tile);
        _characterDisplay.ChangeCharacterArt(tile.CharacterOnTile.CharacterSprite);
    }

    public void KeepTrackOfEndTile(Tile tile)
    {
        _gameMap.SetEndTile(tile);
       // checkForEndOfTurn();
    }

    //private void checkForEndOfTurn()
    //{
    //    bool isTurnOver = true;
    //    for(int i = 0; i < _gameMap.TilesOnMap.Count; i++)
    //    {
    //        if(_gameMap.TilesOnMap[i].GetCurrentState() == _gameMap.TilesOnMap[i].GetActiveState())
    //        {
    //            isTurnOver = false;
    //        }
    //    }
    //    if(isTurnOver)
    //    {
    //        resetAllPlayers();
    //    }
    //}

    //private void resetAllPlayers()
    //{
    //    for (int i = 0; i < _gameMap.TilesOnMap.Count; i++)
    //    {
    //        if(_gameMap.TilesOnMap[i].GetCurrentState() == _gameMap.TilesOnMap[i].GetTiredState())
    //        {
    //            _gameMap.TilesOnMap[i].ChangeState(_gameMap.TilesOnMap[i].GetActiveState());
    //        }
    //    }
    //}
    

}
