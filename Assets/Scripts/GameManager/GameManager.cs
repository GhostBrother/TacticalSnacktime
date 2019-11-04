using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    AICharacterFactory _characterFactory;

    Map _gameMap;

    List<iAffectedByTime> timeAffectedObjects;

    [SerializeField]
    MonoPool _monoPool;

    public MonoPool monoPool{ get { return _monoPool; } }

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
        _characterFactory = new AICharacterFactory(_monoPool);

        _characterDisplay.InitCharacterDisplay();

        actionMenu.onTurnEnd = EndCharacterTurn;
        actionMenu.addTimed = AddTimeInfluencedToList;
        actionMenu.SetGM(this);
       
        _gameMap = _mapGenerator.generateMap();

        AddInGameClockToList(_clock);
        SortList();

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
        character.onStartTurn = OnPlayerControlledStart; 
        AddCharacterToList(character);
    }

    public void AddCustomerCharacterToList(AICharacter customer)
    {
        customer.onStartTurn += OnCustomerStart;
        AddCharacterToList(customer);
    }

    public void AddPawnToTimeline(AbstractPawn ap)
    {
        ap.onTurnEnd = EndNonCharacterTurn;
        foreach (iAffectedByTime TA in timeAffectedObjects)
        {
            if (TA == (iAffectedByTime)ap)
            {
                return;
            }
        }
        AddTimeInfluencedToList(ap);
    }

    public void RemovePawnFromTimeline(AbstractPawn ap)
    {
        for (int i = 0; i < timeAffectedObjects.Count; i++)
        {
            if (timeAffectedObjects[i] == (iAffectedByTime)ap)
            {
                timeAffectedObjects.RemoveAt(i);
                break;
            }
        }
    }

    private void AddCharacterToList(Character character)
    {
        character.onTurnEnd = EndCharacterTurn;
        AddTimeInfluencedToList(character);
    }

    private void AddInGameClockToList(Clock clock)
    {
        _clock.onTurnEnd = EndNonCharacterTurn;
        _clock.onTurnEnd += CheckForCustomerSpawn;
        _clock.onTurnEnd += SortList;
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
        _clock.TurnOrder = timeAffectedObjects[timeAffectedObjects.Count - 1].TurnOrder + 1;
    }

    public void CheckIfCharacterNeedsRemoval() 
    {
        if (CurentCharacter.NeedsRemoval)
        {
            CurentCharacter.TilePawnIsOn.ChangeState(CurentCharacter.TilePawnIsOn.GetClearState());
            CurentCharacter.HideCoaster(CurentCharacter.characterCoaster);
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

        if (timeAffectedObjects[0] is Character)
        {
            Character nextCharacter = (Character)timeAffectedObjects[0];
            CurentCharacter = nextCharacter;
            actionMenu.SetCurrentCharacter(); //nextCharacter
        }
        timeAffectedObjects[0].TurnStart();
    }

    private void CheckForCustomerSpawn()
    {
        if (Random.Range(0, 3) == 2)
        {
            AICharacter newCharacter = _characterFactory.SpawnCharacterAt(_gameMap.GetTileWithType(EnumHolder.EntityType.Door));
            AddCustomerCharacterToList(newCharacter);
        }
    }

    private void MoveCameraToCharacter(Character character)
    {
        camera.PanToLocation(character.TilePawnIsOn.gameObject.transform.position);
        characterDisplay.ChangeCharacterArt(character.PawnSprite);
        characterDisplay.ChangeHeldItemArt(character.CariedObjects, character.NumberOfItemsCanCary);
    }

    // On Player Start

    private void OnPlayerControlledStart(Character playerCharacter)
    {
        camera.onStopMoving = playerCharacter.MoveCharacter;
        MoveCameraToCharacter(playerCharacter);
       // CurentCharacter = playerCharacter;
        SetState(GetIdleState());
    }

   // On Ai Start

    private void OnCustomerStart(Character customerCharacter)
    {
        if (customerCharacter is AICharacter)
        {
            AICharacter c = (AICharacter)customerCharacter;
           // CurentCharacter = customerCharacter;
            SetState(GetMovingState());
            camera.onStopMoving = c.MoveCharacter;
            MoveCameraToCharacter(customerCharacter);
        }
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

    private void DebugListAllTimeAffected()
    {
        for (int i = 0; i < timeAffectedObjects.Count; i++)
        {
            if (timeAffectedObjects[i] is Grill) { Debug.Log(i + ": " + " is gril"); }
            if (timeAffectedObjects[i] is PlayercontrolledCharacter) { Debug.Log(i + ": " + " is character"); }
            if (timeAffectedObjects[i] is AICharacter) { Debug.Log(i + ": " + " is customer"); }
            if (timeAffectedObjects[i] is Clock) { Debug.Log(i + ": " + " is clock"); }
        }
    }

}
