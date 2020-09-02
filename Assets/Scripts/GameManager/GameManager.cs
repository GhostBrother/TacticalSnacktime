using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    AICharacterFactory _characterFactory;

     CharacterRoster _characterRoster;

    List<iAffectedByTime> timeAffectedObjects;

    [SerializeField]
    MonoPool _monoPool;

    public MonoPool monoPool { get { return _monoPool; } }

    #region GameUI
    [SerializeField]
    CharacterDisplay _characterDisplay;

    public CharacterDisplay characterDisplay { get { return _characterDisplay; }  }

    [SerializeField]
    EndOfDayPannel _EndOfDayPannel;

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
    DeployState deployState;
    iGameManagerState curentState;
    iGameManagerState controlsDisabled;

	// Use this for initialization
	void Start ()
    {
       
        //Hack
        _characterRoster = new CharacterRoster(); 

        idleMode = new Idle(this);
        
        controlsDisabled = new ControlsDisabled();

        timeAffectedObjects = new List<iAffectedByTime>();
        _characterFactory = new AICharacterFactory(_monoPool);

        _characterDisplay.InitCharacterDisplay();
      
        actionMenu.onButtonClick = UpdateCharacterDisplay;

        _mapGenerator.SetGm(this);
        _gameMap = _mapGenerator.generateMap();
       
        _EndOfDayPannel.Init(_characterRoster, _gameMap);
        _EndOfDayPannel.startNextDay += StartDay;
        _EndOfDayPannel.SetUpNexDay();
        
        AddInGameClockToList(_clock);
        deployState = new DeployState(this, _characterRoster);
        SortList();
        EndDay();
    }

    public iGameManagerState GetIdleState()
    {
        return idleMode;
    }

    public iGameManagerState GetDisableControls()
    {
        return controlsDisabled;
    }

    public void SetState(iGameManagerState newState)
    {
        curentState = newState;
       
    }

    public void AddPlayerControlledCharacterToList(PlayercontrolledCharacter character)
    {
        character.onStartTurn = OnPlayerControlledStart;
        character.PutCharacterBack = _characterRoster.AddCharacterBackToList;
        character.onTurnEnd = EndTurn;
        AddTimeInfluencedToList(character);
    }

    public void AddCustomerCharacterToList(AICharacter customer)
    {
        customer.onStartTurn = OnCustomerStart;
        customer.OnExit = GiveRating;
        customer.OnPay = PayForFood;
        customer.onTurnEnd = EndTurn;
        AddTimeInfluencedToList(customer);
    }

    public void AddPawnToTimeline(iAffectedByTime ap) 
    {
        ap.onStartTurn = OnPawnStart;
       
        foreach (iAffectedByTime TA in timeAffectedObjects)
        {
            if (TA == ap)
            {
                return;
            }
        }
        ap.onTurnEnd = EndTurn;
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

    private void AddInGameClockToList(Clock clock)
    {
        clock.onTurnEnd += CheckForCustomerSpawn;
        clock.onTurnEnd += CheckForEmployeeSpawn;
        clock.onTurnEnd += EndTurn;
        // clock.onTurnEnd += SortList;
        clock.onDayOver = EndDay;
        AddTimeInfluencedToList(clock);
    }

    private void AddTimeInfluencedToList(iAffectedByTime timeAffected)
    {
        // this plus is causing trouble with grill; 
        //timeAffected.onTurnEnd += EndTurn;
        timeAffected.RemoveFromTimeline = RemovePawnFromTimeline; // Was +=
        timeAffectedObjects.Add(timeAffected);
    }

    public void ActivateTile(Tile tile)
    {
        if (curentState == deployState)
        {
            curentState.TileClicked(tile);
        }
        else
        tile.curentState.TileClicked();
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
        }
        timeAffectedObjects[0].TurnStart();
    }

    private void CheckForEmployeeSpawn()
    {
        List<PlayercontrolledCharacter> playercontrolledCharacters = _characterRoster.GetCharactersForTime(_clock.CurTime);
        for(int i = 0; i < playercontrolledCharacters.Count; i++)
        {
            AddPlayerControlledCharacterToList(playercontrolledCharacters[i]);
        }
    }

    private void CheckForCustomerSpawn()
    {
        List<AICharacter> aICharacters = _characterFactory.GetCharacterSpawnsForTime(_clock.CurTime, _gameMap.GetTileWithType(EnumHolder.EntityType.Door));
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
        // HACK
        camera.onStopMoving = null;
        camera.PanToLocation(character.TilePawnIsOn.gameObject.transform.position);
    }

    // On Player Start

    private void OnPlayerControlledStart(AbstractPawn playerCharacter)
    {
        MoveCameraToPawn(playerCharacter);
        playerCharacter.TilePawnIsOn.onClick = ShowCharacterActions;
        playerCharacter.characterCoaster.OnStopMoving = ShowCharacterActions;
    }

   // On Ai Start

    private void OnCustomerStart(AbstractPawn customerCharacter)
    {
        if (customerCharacter is AICharacter)
        {
            AICharacter c = (AICharacter)customerCharacter;
            SetState(GetDisableControls());
            MoveCameraToPawn(customerCharacter);
            camera.onStopMoving = c.MoveCharacter;  
        }

    }

    private void OnPawnStart(AbstractPawn abstractPawn)
    {
        SetState(GetDisableControls());
        MoveCameraToPawn(abstractPawn);
        camera.onStopMoving = MoveDonenessMeter;
    }

    private void EndTurn()
    {
        CurentCharacter.TurnEnd();
        CheckIfCharacterNeedsRemoval();
        SetState(GetIdleState());
        StartNextCharactersTurn();
    }

    private void GiveRating(AICharacter aICharacter)
    {
        _EndOfDayPannel.reputation.Increment(aICharacter.Satisfaction);
    }

    private void PayForFood(decimal price)
    {
        _EndOfDayPannel.money.Increment(price);
    }

    private void StartDay()
    {
        _EndOfDayPannel.HideEndOfDayPage();
        _gameMap.AcivateAllDeployTiles();
        _characterFactory.RollCharactersForDay();
        SortList();
        _clock.SetClockToStartOfDay();
        deployState.SetOpeningStaff(_clock.OpeningTime);
        curentState = deployState;

    }

    private void EndDay()
    {
        _EndOfDayPannel.ShowEndOfDayPage();
        SetState(GetDisableControls());

        timeAffectedObjects.Remove(_clock);
        for (int i = 0; i < timeAffectedObjects.Count;)
        {
            timeAffectedObjects[i].OnEndDay();
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

    void ShowCharacterActions(Tile tile)
    {
        ActionMenu.ShowActionsAtTile(tile);
        ActionMenu.OpenMenu(CurentCharacter.LoadCommands());
        // Hack;
       // DeactivateAllTiles();
    }

}
