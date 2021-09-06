using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    AICharacterFactory _characterFactory;

    CharacterRoster _characterRoster;

    List<PlayercontrolledCharacter> charactersForStartOfDay;

    List<iAffectedByTime> timeAffectedObjects;

    [SerializeField]
    MonoPool monoPool;

    [SerializeField]
    CharacterDisplay characterDisplay;

    [SerializeField]
    EndOfDayPannel _EndOfDayPannel;

    [SerializeField]
    Clock _clock;

    [SerializeField]
    ActionMenu actionMenu;

    public ActionMenu ActionMenu { get { return actionMenu; } }

    [SerializeField]
    MapGenerator _mapGenerator;

    Map _gameMap;

    [SerializeField]
    CameraController camera;

    [SerializeField]
    TradeMenu tradeMenu;

    public CameraController CameraController { get { return camera; } }

    public Character CurentCharacter { get; private set; }

	// Use this for initialization
	void Start ()
    {
        _characterRoster = new CharacterRoster(); 

        timeAffectedObjects = new List<iAffectedByTime>();
        _characterFactory = new AICharacterFactory(monoPool);

        characterDisplay.InitCharacterDisplay();
      
        actionMenu.onButtonClick = UpdateCharacterDisplay;

        _mapGenerator.SetGm(this);
        _gameMap = _mapGenerator.generateMap();
       
        _EndOfDayPannel.Init(_characterRoster, _gameMap);
        _EndOfDayPannel.startNextDay += StartDay;
        _EndOfDayPannel.SetUpNexDay();

        this.CameraController.Init(_gameMap.GetTileAtRowAndColumn(0,0).gameObject.GetComponent<Collider2D>().bounds.size);
        
        AddInGameClockToList(_clock);

        SortList();
        EndDay();
    }

    public void AddPlayerControlledCharacterToList(PlayercontrolledCharacter character)
    {
        character.onStartTurn = OnPlayerControlledStart;
        character.PutCharacterBack = _characterRoster.AddCharacterBackToList;
        character.onTurnEnd = EndTurn;
        character.characterCoaster.OnStartMoving = SetCameraToFollowCurentCharacter;
        character.onTrade = OpenTradeMenu;
        AddTimeInfluencedToList(character);
    }

    public void AddCustomerCharacterToList(AICharacter customer)
    {
        customer.onStartTurn = OnCustomerStart;
        customer.OnExit = GiveRating;
        customer.OnPay = PayForFood;
        customer.onTurnEnd = EndTurn;
       customer.characterCoaster.OnStartMoving = SetCameraToFollowCurentCharacter;
        customer.onTrade = OpenTradeMenu;
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
        clock.onTurnEnd += SwapToNextCharacter;
        clock.onTurnEnd += StartNextCharactersTurn;
        clock.onDayOver = EndDay;
        AddTimeInfluencedToList(clock);
    }

    private void AddTimeInfluencedToList(iAffectedByTime timeAffected)
    {
        timeAffected.RemoveFromTimeline = RemovePawnFromTimeline;
        timeAffectedObjects.Add(timeAffected);
    }

    public void ActivateTile(Tile tile)
    {
        tile.SelectTile();
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
            CurentCharacter.TilePawnIsOn.DeactivateTile();
            CurentCharacter.HideCoaster(CurentCharacter.characterCoaster);
            CurentCharacter.OnEndDay();
            CurentCharacter = null;
            
            if (!timeAffectedObjects.OfType<Character>().Any())
            {
                EndDay();
            }
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
        if (timeAffectedObjects.Count > 0)
        {
            if (timeAffectedObjects[0] is Character)
            {
                CurentCharacter = (Character)timeAffectedObjects[0];
            }
            timeAffectedObjects[0].TurnStart();
            camera.SwitchToPanCamera();
        }
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
        characterDisplay.ChangeCharacterArt(character.characterArt);   
        UpdateCharacterDisplay();
        camera.cameraFollowChracter(character.characterCoaster);
    }

    private void SetCameraToFollowCurentCharacter()
    {
        camera.SwitchToFollowMode();
    }

    private void OnPlayerControlledStart(AbstractPawn playerCharacter)
    {
        MoveCameraToPawn(playerCharacter);
        playerCharacter.TilePawnIsOn.onClick = ShowCharacterActions;
        playerCharacter.characterCoaster.OnStopMoving = ShowCharacterActions;
    }


    private void OnCustomerStart(AbstractPawn customerCharacter)
    {
        if (customerCharacter is AICharacter)
        {
            AICharacter c = (AICharacter)customerCharacter;
            MoveCameraToPawn(c);
            camera.onStopMoving = c.MoveCharacter;
        }
    }

    private void OnPawnStart(AbstractPawn abstractPawn)
    {
        MoveCameraToPawn(abstractPawn);
        camera.onStopMoving = MoveDonenessMeter;
    }

    private void EndTurn()
    {
        CurentCharacter.TurnEnd();
        CheckIfCharacterNeedsRemoval();
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
        
        _gameMap.AcivateAllDeployTiles(DeployStateReady);
        _characterFactory.RollCharactersForDay();
        SortList();
        _clock.SetClockToStartOfDay();
        LoadDeployState();
        characterDisplay.ChangeCharacterArt(charactersForStartOfDay[0].characterArt);
        camera.SwitchToPlayerControlled();
    }

    private void EndDay()
    {
        _EndOfDayPannel.ShowEndOfDayPage();
        timeAffectedObjects.Remove(_clock);
        for (int i = 0; i < timeAffectedObjects.Count;)
        {
            timeAffectedObjects[i].OnEndDay();
        }
        camera.SwitchToFrozenMode();
    }

    public void UpdateCharacterDisplay()
    {
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
        camera.SwitchToPlayerControlled();
    }


    public void DeployStateReady(Tile tile)
    {

        if (tile.EntityTypeOnTile == EnumHolder.EntityType.Clear)
        {
            PlayercontrolledCharacter CharacterToUse = charactersForStartOfDay[0];
            CharacterToUse.characterCoaster = monoPool.GetCharacterCoasterInstance();
            CharacterToUse._monoPool = monoPool;
            CharacterToUse.TilePawnIsOn = tile;
            CharacterToUse.NeedsRemoval = false;
            AddPlayerControlledCharacterToList(CharacterToUse);
            CharacterToUse.characterCoaster.SetArtForFacing(EnumHolder.Facing.Down);
            tile.EntityTypeOnTile = EnumHolder.EntityType.Character; 
            charactersForStartOfDay.Remove(CharacterToUse);

            if (charactersForStartOfDay.Count == 0)
            {
                SortList();

                DeactivateAllTiles();
                StartNextCharactersTurn();
            }

            else
                characterDisplay.ChangeCharacterArt(charactersForStartOfDay[0].characterArt);
        }

    }

    private void LoadDeployState()
    {
        charactersForStartOfDay = _characterRoster.GetCharactersForTime(_clock.CurTime);
    }

    private void OpenTradeMenu(Character giver, iContainCaryables reciver)
    {
        camera.SwitchToFrozenMode();
        tradeMenu.OpenTradePanels(giver, reciver, actionMenu.OpenMenu); // Hack, find a way to do this without arguing in the menu
    }

}
