using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    AICharacterFactory _characterFactory;

    List<iAffectedByTime> timeAffectedObjects;

    [SerializeField]
    MonoPool _monoPool;

    public MonoPool monoPool { get { return _monoPool; } }

    #region GameUI
    [SerializeField]
    CharacterDisplay _characterDisplay;

    public CharacterDisplay characterDisplay { get { return _characterDisplay; }  }

    [SerializeField]
    ResturantStats _ResturantStats;

    [SerializeField]
    Clock _clock;

    [SerializeField]
    ActionMenu actionMenu;

    public ActionMenu ActionMenu { get { return actionMenu; } }
    #endregion

    [SerializeField]
    MapGenerator _mapGenerator;

    Map _gameMap;

    [SerializeField]
    CameraController camera;

    public CameraController CameraController { get { return camera; } }

    public Character CurentCharacter { get; private set; }

    iGameManagerState idleMode;
    iGameManagerState selectedMode;
    iGameManagerState deployState;
    iGameManagerState curentState;
    iGameManagerState movingState;

	// Use this for initialization
	void Start ()
    {
        idleMode = new Idle(this);
        selectedMode = new TileSelected(this);
        deployState = new DeployState(this);
        movingState = new MovingState();

        curentState = deployState;

        timeAffectedObjects = new List<iAffectedByTime>();
        _characterFactory = new AICharacterFactory(_monoPool);

        _characterDisplay.InitCharacterDisplay();

        actionMenu.onTurnEnd = EndTurn;
        actionMenu.addTimed = AddTimeInfluencedToList;
        actionMenu.onButtonClick = UpdateCharacterDisplay;
        actionMenu.SetGM(this);

        _mapGenerator.SetGm(this);
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

    public iGameManagerState GetMovingState()
    {

        return movingState;
    }

    public void SetState(iGameManagerState newState)
    {
        curentState = newState;
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

    public void AddPawnToTimeline(iAffectedByTime ap) 
    {
        ap.onStartTurn = OnPawnStart;
        ap.onTurnEnd = EndTurn;
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
        character.onTurnEnd = EndTurn;
        AddTimeInfluencedToList(character);
    }

    private void AddInGameClockToList(Clock clock)
    {
        _clock.onTurnEnd += CheckForCustomerSpawn;
        _clock.onTurnEnd += EndTurn; 
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
        timeAffectedObjects.Remove(_clock);
        timeAffectedObjects.Sort((x, y) => x.TurnOrder.CompareTo(y.TurnOrder));
        timeAffectedObjects.Add(_clock);
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
            CurentCharacter = (Character)timeAffectedObjects[0];
            actionMenu.SetCurrentCharacter();
        }
        timeAffectedObjects[0].TurnStart();
    }

    private void CheckForCustomerSpawn()
    {

        List<AICharacter> aICharacters = _characterFactory.GetCharacterSpawnsForTime(_clock.Time, _gameMap.GetTileWithType(EnumHolder.EntityType.Door));
        for (int i = 0; i < aICharacters.Count; i++)
        {
            AddCustomerCharacterToList(aICharacters[i]);
        }
            
    }


    private void MoveCameraToPawn(AbstractPawn character)
    {
        SetDonenessTracks();
        characterDisplay.ChangeCharacterArt(character.PawnSprite);  
        UpdateCharacterDisplay();
        camera.PanToLocation(character.TilePawnIsOn.gameObject.transform.position);
    }

    // On Player Start

    private void OnPlayerControlledStart(AbstractPawn playerCharacter)
    {
        if (playerCharacter is PlayercontrolledCharacter)
        {
            PlayercontrolledCharacter pc = (PlayercontrolledCharacter)playerCharacter;
            camera.onStopMoving = pc.MoveCharacter;
            MoveCameraToPawn(playerCharacter);
            SetState(GetIdleState());
        }
    }

   // On Ai Start

    private void OnCustomerStart(AbstractPawn customerCharacter)
    {
        if (customerCharacter is AICharacter)
        {
            AICharacter c = (AICharacter)customerCharacter;
            SetState(GetMovingState());
            camera.onStopMoving = c.MoveCharacter;
            MoveCameraToPawn(customerCharacter);
        }
    }

    private void OnPawnStart(AbstractPawn abstractPawn)
    {
        SetState(GetMovingState());
        camera.onStopMoving = MoveDonenessMeter;
        MoveCameraToPawn(abstractPawn);
    }

    private void EndTurn()
    {
        CheckIfCharacterNeedsRemoval();
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


    public void UpdateCharacterDisplay()
    {
        // Hack
        if (timeAffectedObjects[0] is iContainCaryables)
        {
            iContainCaryables character = (iContainCaryables)timeAffectedObjects[0];
            characterDisplay.ChangeHeldItemArt(character.cariedObjects, character.numberOfCarriedObjects);
        }
    }

    void MoveDonenessMeter()
    {
        if (timeAffectedObjects[0] is iContainCaryables)
        {
            iContainCaryables character = (iContainCaryables)timeAffectedObjects[0];
            characterDisplay.onStopMoving = timeAffectedObjects[0].TurnEnd;
            characterDisplay.UpdateDonenessTrackers(character.cariedObjects, 0);
        }
    }

    void SetDonenessTracks()
    {
        if (timeAffectedObjects[0] is iContainCaryables)
        {
            iContainCaryables character = (iContainCaryables)timeAffectedObjects[0];
            characterDisplay.SetDonenessTracks(character.cariedObjects);
        }
    }

    


}
