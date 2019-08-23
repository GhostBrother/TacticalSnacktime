using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    AICharacterFactory _characterFactory;

    Map _gameMap;

    List<iAffectedByTime> timeAffectedObjects;

    [SerializeField]
    MapGenerator _mapGenerator;

    [SerializeField]
    ActionMenu actionMenu;

    public ActionMenu ActionMenu { get { return actionMenu; } private set { } }

    [SerializeField]
    CameraController camera;

    public CameraController CameraController { get { return camera; } private set { } }

    [SerializeField]
    CharacterDisplay _characterDisplay;

    public CharacterDisplay characterDisplay { get { return _characterDisplay; } private set {; } }

    public Character CurentCharacter { get; private set; }

    iGameManagerState idleMode;
    iGameManagerState selectedMode;
    iGameManagerState deployState;
    iGameManagerState actionState;
    iGameManagerState curentState;
    iGameManagerState movingState;

	// Use this for initialization
	void Start ()
    {
        idleMode = new Idle(this);
        selectedMode = new TileSelected(this);
        deployState = new DeployState(this);
        actionState = new ActionState(this);
        movingState = new MovingState();

        curentState = deployState;

        timeAffectedObjects = new List<iAffectedByTime>();
        _characterFactory = new AICharacterFactory();


        actionMenu.onTurnEnd = EndTurn;
        
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

    public iGameManagerState GetMovingState()
    {
        return movingState;
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
        character.onStartTurn = MoveCameraToCharacter;
        character.onStartTurn += SetCharacterAsCurrent;
        character.onTurnEnd = EndTurn;
        timeAffectedObjects.Add(character);
    }

    public void ActivateTile(Tile tile)
    {
        curentState.TileClicked(tile);
    }

    public void RightClick(Tile tile)
    {
        curentState.RightClick(tile);
    }

    public void DeactivateAllTiles()
    {
        _gameMap.DeactivateAllTiles();
    }

    public void SortList()
    {
        //charactersOnMap.Sort((x, y) => x.SpeedStat.CompareTo(y.SpeedStat));
    }

    public void CheckIfCharacterNeedsRemoval() 
    {
        if (CurentCharacter.NeedsRemoval)
        {
            CurentCharacter.TilePawnIsOn.ChangeState(CurentCharacter.TilePawnIsOn.GetClearState());
            CurentCharacter.HideCoaster(CurentCharacter.characterCoaster);
            CurentCharacter.HideCoaster(CurentCharacter.ItemCoaster);
            timeAffectedObjects.Remove(CurentCharacter);
        }
        else
        {
            timeAffectedObjects.Remove(CurentCharacter);
            timeAffectedObjects.Add(CurentCharacter);
        }
    }

    public void StartNextCharactersTurn()
    {
        timeAffectedObjects[0].TurnStart();
    }

    private void CheckForCustomerSpawn()
    {
        //TEST
        AICharacter newCharacter = _characterFactory.SpawnCharacterAt(_gameMap.GetTileAtRowAndColumn(7, 6));
        newCharacter.onStartTurn += SetStateToMovingState;
        AddCharacterToList(newCharacter);
        AICharacter newCharacter2 = _characterFactory.SpawnCharacterAt(_gameMap.GetTileAtRowAndColumn(6,7));
        AddCharacterToList(newCharacter2);
        newCharacter2.onStartTurn += SetStateToMovingState;

        //TEST
        Supply newSupply = new Supply(new Food("Burger", 2.00M, SpriteHolder.instance.GetFoodArtFromIDNumber(0)) , SpriteHolder.instance.GetSupplyBox());
        newSupply.characterCoaster = CharacterCoasterPool.Instance.SpawnFromPool();
        newSupply.TilePawnIsOn = _gameMap.GetTileAtRowAndColumn(3, 2);

    }

    private void SetCharacterAsCurrent(Character character)
    {
        CurentCharacter = character;
    }

    private void MoveCameraToCharacter(Character character)
    {
        camera.PanToLocation(character.TilePawnIsOn.gameObject.transform.position);
        characterDisplay.ChangeCharacterArt(character.PawnSprite);
    }
    
    // Hack
    private void SetStateToMovingState(Character character)
    {
        SetState(GetMovingState());
    }

    private void EndTurn()
    {
        CheckIfCharacterNeedsRemoval();
        SetState(GetIdleState());
        StartNextCharactersTurn();
    }

}
