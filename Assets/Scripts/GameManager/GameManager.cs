using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    AICharacterFactory _characterFactory;

    Map _gameMap;

    public List<Character> charactersOnMap { get; private set; }

    [SerializeField]
    MapGenerator _mapGenerator;


    [SerializeField]
    CameraController camera;

    public CameraController Camera { get { return camera; } private set { } }

    [SerializeField]
    ActionMenu actionMenu;

    public ActionMenu ActionMenu { get { return actionMenu; } private set { } }

    [SerializeField]
    CharacterDisplay _characterDisplay;

    public CharacterDisplay characterDisplay { get { return _characterDisplay; } private set {; } }



    iGameManagerState idleMode;
    iGameManagerState selectedMode;
    iGameManagerState deployState;
    iGameManagerState actionState;
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
        actionState = new ActionState(this);

        curentState = deployState;

        charactersOnMap = new List<Character>();
        _characterFactory = new AICharacterFactory();

        // Hack circular dependancy.
        actionMenu.gameManager = this;
        
        _gameMap = _mapGenerator.generateMap();

        CheckForCustomerSpawn();
    }

    public iGameManagerState GetIdleState()
    {
        return idleMode;
    }

    public iGameManagerState GetSelectedState()
    {
        return selectedMode;
    }

    public iGameManagerState GetActionState()
    {
        return actionState;
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
        if (charactersOnMap[0].NeedsRemoval)
        {
            charactersOnMap[0].TilePawnIsOn.ChangeState(charactersOnMap[0].TilePawnIsOn.GetClearState());
            CharacterCoasterPool.Instance.PutBackInPool(charactersOnMap[0].characterCoaster);
            charactersOnMap.RemoveAt(0);
        }
        camera.PanToLocation(charactersOnMap[0].TilePawnIsOn.gameObject.transform.position);
        characterDisplay.ChangeCharacterArt(charactersOnMap[0].PawnSprite);
        return charactersOnMap[0];
    }

    public void MoveFirstCharacterToLast()
    {
        Character temp = GetNextCharacter();
        charactersOnMap.Remove(temp);
        charactersOnMap.Add(temp);
    }

    private void CheckForCustomerSpawn()
    {
        AICharacter newCharacter = _characterFactory.SpawnCharacterAt(_gameMap.GetTileAtRowAndColumn(0, 1));
        AddCharacterToList(newCharacter);
    }

    public void KeepTrackOfStartTile(Tile tile)
    {
        _gameMap.SetStartTile(tile);
        GetNextCharacter().CharacterMove();
    }

    public void KeepTrackOfEndTile(Tile tile)
    {
        if (tile.GetCurrentState() == tile.GetHilightedState())
        {
            GetNextCharacter().TilePawnIsOn = tile;
            _gameMap.SetEndTile(tile);
        }
    }

    //Hack for demo, This is called from input manager. Look into a fix. 
    public void EndTurn()
    {
        // This should move at the same time as out human player;
        while (GetNextCharacter() is AICharacter)
        {
            AICharacter tempChar = (AICharacter)GetNextCharacter();
            tempChar.CheckPath();
            tempChar.Move();
            MoveFirstCharacterToLast();
        }

       SetState(GetIdleState());
    }


}
