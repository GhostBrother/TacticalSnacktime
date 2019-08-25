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

    [SerializeField]
    Clock _clock;

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

        actionMenu.onTurnEnd = EndCharacterTurn;
        
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

    public void AddPlayerControlledCharacterToList(PlayercontrolledCharacter character)
    {
        character.onStartTurn += OnPlayerControlledStart;
        AddCharacterToList(character);
    }

    public void AddCustomerCharacterToList(AICharacter customer)
    {
        customer.onStartTurn += OnCustomerStart;
        AddCharacterToList(customer);
    }

    private void AddCharacterToList(Character character)
    {
        character.onTurnEnd = EndCharacterTurn;
        AddTimeInfluencedToList(character);
    }

    private void AddInGameClockToList(Clock clock)
    {
        _clock.onTurnEnd = EndNonCharacterTurn;
        _clock.onDayOver = EndDay;
        AddTimeInfluencedToList(clock);
    }

    private void AddTimeInfluencedToList(iAffectedByTime timeAffected)
    {
        timeAffectedObjects.Add(timeAffected);
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
        timeAffectedObjects.Sort((x, y) => x.TurnOrder.CompareTo(y.TurnOrder));
        _clock.TurnOrder = timeAffectedObjects[timeAffectedObjects.Count -1 ].TurnOrder + 1;
        AddInGameClockToList(_clock);
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
            SwapToNextCharacter();
        }
    }

    private void SwapToNextCharacter()
    {
        timeAffectedObjects.Add(timeAffectedObjects[0]);
        timeAffectedObjects.RemoveAt(0);
    }

    public void StartNextCharactersTurn()
    {
        timeAffectedObjects[0].TurnStart();
    }

    private void CheckForCustomerSpawn()
    {
        //TEST
        AICharacter newCharacter = _characterFactory.SpawnCharacterAt(_gameMap.GetTileAtRowAndColumn(7, 6));
        AddCustomerCharacterToList(newCharacter);
        AICharacter newCharacter2 = _characterFactory.SpawnCharacterAt(_gameMap.GetTileAtRowAndColumn(6,7));
        AddCustomerCharacterToList(newCharacter2);


        //TEST
        Supply newSupply = new Supply(new Food("Burger", 2.00M, SpriteHolder.instance.GetFoodArtFromIDNumber(0)) , SpriteHolder.instance.GetSupplyBox());
        newSupply.characterCoaster = CharacterCoasterPool.Instance.SpawnFromPool();
        newSupply.TilePawnIsOn = _gameMap.GetTileAtRowAndColumn(3, 2);

    }

    private void MoveCameraToCharacter(Character character)
    {
        camera.PanToLocation(character.TilePawnIsOn.gameObject.transform.position);
        characterDisplay.ChangeCharacterArt(character.PawnSprite);
    }

    // On Player Start

    private void OnPlayerControlledStart(Character playerCharacter)
    {
        MoveCameraToCharacter(playerCharacter);
        CurentCharacter = playerCharacter;
        SetState(GetIdleState());
    }

   // On Ai Start

    private void OnCustomerStart(Character customerCharacter)
    {
        MoveCameraToCharacter(customerCharacter);
        CurentCharacter = customerCharacter;
        SetState(GetMovingState());
    }

   // On all Cooking station Start

    private void EndCharacterTurn()
    {
        CheckIfCharacterNeedsRemoval();
        EndTurn();
    }

    private void EndNonCharacterTurn()
    {
        SwapToNextCharacter();
        EndTurn();
    }

    private void EndTurn()
    {
        SetState(GetIdleState());
        StartNextCharactersTurn();
    }

    private void EndDay()
    {
        Debug.Log("Day is done");
    }

}
