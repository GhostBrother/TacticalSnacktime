using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    AICharacterFactory _characterFactory;

    Map _gameMap;

    List<Character> charactersOnMap;

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

        charactersOnMap = new List<Character>();
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
        charactersOnMap.Add(character);
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
        charactersOnMap.Sort((x, y) => x.SpeedStat.CompareTo(y.SpeedStat));
    }

    public void CheckIfCharacterNeedsRemoval() 
    {
        if (CurentCharacter.NeedsRemoval)
        {
            CurentCharacter.TilePawnIsOn.ChangeState(CurentCharacter.TilePawnIsOn.GetClearState());
            CharacterCoasterPool.Instance.PutBackInPool(CurentCharacter.characterCoaster);
            CurentCharacter.HideItem();
            charactersOnMap.Remove(CurentCharacter);
        }
        else
        {
            charactersOnMap.Remove(CurentCharacter);
            charactersOnMap.Add(CurentCharacter);
        }
    }

    public void StartNextCharactersTurn()
    { 
        CurentCharacter = charactersOnMap[0];

        camera.PanToLocation(CurentCharacter.TilePawnIsOn.gameObject.transform.position);
        characterDisplay.ChangeCharacterArt(CurentCharacter.PawnSprite);

    }

    private void CheckForCustomerSpawn()
    {
        AICharacter newCharacter = _characterFactory.SpawnCharacterAt(_gameMap.GetTileAtRowAndColumn(0, 1));
        AddCharacterToList(newCharacter);
        AICharacter newCharacter2 = _characterFactory.SpawnCharacterAt(_gameMap.GetTileAtRowAndColumn(1,0));
        AddCharacterToList(newCharacter2);

    }
    // Character, what is your interperation of start turn? ( possible question for refactor.

    public void CheckForAIPlayer()
    {
         if (CurentCharacter is AICharacter)
        {
            SetState(GetMovingState());
            AICharacter tempChar = (AICharacter)CurentCharacter;
            tempChar.CheckPath();
            tempChar.Move();
            tempChar.characterCoaster.onStopMoving = AILookForAction;
        }
         else
            SetState(GetIdleState());
    }

    public void AILookForAction(Tile tile)
    {
        // TODO, Give the ai a weighted choice of what to do based on tile, even if it is wait paitently. 
        EndTurn();
    }

    public void EndTurn()
    {
        CheckIfCharacterNeedsRemoval();
        SetState(GetIdleState());
        StartNextCharactersTurn();
        CheckForAIPlayer();
    }

}
