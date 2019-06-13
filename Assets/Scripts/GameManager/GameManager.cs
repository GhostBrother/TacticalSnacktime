using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private AICharacterFactory _characterFactory;
    private Map _gameMap;

    public List<Character> charactersOnMap { get; private set; }

    [SerializeField]
    private MapGenerator _mapGenerator;

    [SerializeField]
    CharacterDisplay _characterDisplay;

    [SerializeField]
    CameraController camera;

    public CameraController Camera { get { return camera; } private set { } }

    public CharacterDisplay characterDisplay { get { return _characterDisplay; } private set {; } }

    private int MaxCharacters;

    private int CharacterIndex;


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
        _characterFactory = new AICharacterFactory();
        
        _gameMap = _mapGenerator.generateMap();
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

        // move this to some sort of increase size list
        MaxCharacters = charactersOnMap.Count;
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

        CheckForCustomerSpawn();
    }

    private void CheckForCustomerSpawn()
    {
        CharacterIndex++;
        if(CharacterIndex == MaxCharacters)
        {
            AICharacter newCharacter = _characterFactory.SpawnCharacterAt(_gameMap.GetTileAtRowAndColumn(0,0));
            newCharacter.setTarget(_gameMap.GetTileAtRowAndColumn(4,4));
            AddCharacterToList(newCharacter);
            
            MaxCharacters = charactersOnMap.Count;
            CharacterIndex = 0; 
        }
    }

    public void KeepTrackOfStartTile(Tile tile)
    {
        _gameMap.SetStartTile(tile);
        GetNextCharacter().CharacterMove();
       // _characterDisplay.ChangeCharacterArt(tile.CharacterOnTile.CharacterSprite);
    }

    public void KeepTrackOfEndTile(Tile tile)
    {
        if (tile.GetCurrentState() == tile.GetHilightedState())
        {
            GetNextCharacter().tileCharacterIsOn = tile;
            GetNextCharacter().ColorTile();
            _gameMap.SetEndTile(tile);
        }
    }


}
