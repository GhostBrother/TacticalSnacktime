using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionMenu : MonoBehaviour
{

    public delegate void OnTurnEnd();
    public OnTurnEnd onTurnEnd;

    public delegate void OnButtonClick();
    public OnButtonClick onButtonClick;

    public delegate void AddTimedObjectToList(iAffectedByTime timedObject);
    public AddTimedObjectToList addTimed;

    List<Command> ActionMenuCommands;

    GameManager _Gm;

    [SerializeField]
    Camera cam;

    [SerializeField]
    ActionButton buttonPrefab;

    List<ActionButton> actionButtons;

    //Hack
    Character _currentCharacter;

    public void Start()
    {
        ActionMenuCommands = new List<Command>();
        actionButtons = new List<ActionButton>();
    }

    public void SetGM(GameManager gm)
    {
        _Gm = gm;
    }

    public void SetCurrentCharacter() 
    {
        _currentCharacter = _Gm.CurentCharacter;
    }

    public void ShowActionsAtTile()
    {
        this.transform.position = _currentCharacter.TilePawnIsOn.transform.position;
        OpenMenu();       
    }

     void OpenMenu()
    {
        HideAllActions(); // here
        GetAllActionsFromTile();

        if (ActionMenuCommands.Count > actionButtons.Count)
        {
            int temp = ActionMenuCommands.Count - actionButtons.Count;
            for (int i = 0; i < temp; i++)
            {
                ActionButton tempButton = Instantiate(buttonPrefab, this.transform);
                actionButtons.Add(tempButton);
            }
        }

        if (_Gm.CurentCharacter._MoveRemaining > 0)
        {
            ActionButton moveButton = Instantiate(buttonPrefab, this.transform);
            moveButton.StoredCommand = new MoveCommand(_Gm);
            moveButton.onActionTaken += HideAllActions;
            moveButton.gameObject.SetActive(true);
            moveButton.transform.position = cam.WorldToScreenPoint(new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z)); 
            actionButtons.Add(moveButton);
        }

        for (int i = 0; i < ActionMenuCommands.Count; i++)
        {
            actionButtons[i].gameObject.SetActive(true);
            actionButtons[i].StoredCommand = ActionMenuCommands[i];
            actionButtons[i].onActionTaken += HideAllActions;
            actionButtons[i].onActionTaken += OpenMenu;
            actionButtons[i].onActionTaken += onButtonClick.Invoke;
            actionButtons[i].transform.position = cam.WorldToScreenPoint(new Vector3(this.transform.position.x, this.transform.position.y - (i * actionButtons[i].gameObject.transform.lossyScale.y), this.transform.position.z));
        }

        ActionButton endButton = Instantiate(buttonPrefab, this.transform);
        endButton.StoredCommand = new EndTurn(this);
        endButton.onActionTaken += HideAllActions;
        endButton.gameObject.SetActive(true);
        endButton.transform.position = cam.WorldToScreenPoint(new Vector3(this.transform.position.x, this.transform.position.y - ((ActionMenuCommands.Count) * endButton.gameObject.transform.lossyScale.y), this.transform.position.z)); 
        actionButtons.Add(endButton);
    }


    public void HideAllActions()
    {
        for (int i = 0; i < actionButtons.Count; i++)
        {
            actionButtons[i].gameObject.SetActive(false);
        }
        ActionMenuCommands.Clear();
        actionButtons.Clear();
    }

    public void EndTurn()
    {
        onTurnEnd.Invoke();
    }

    public void AddCommandsToList(List<Command> commands)
    {
        foreach (Command c in commands)
        {
            ActionMenuCommands.Add(c);
        }
    }

    void GetAllActionsFromTile()
    {
        foreach (Tile neighbor in _currentCharacter.TilePawnIsOn.neighbors)
        {
            if (neighbor.IsTargetableOnTile)
            {
                neighbor.TargetableOnTile.GetTargeter(_currentCharacter);
                AddCommandsToList(neighbor.TargetableOnTile.GetCommands());
            }

            AddCommandsToList(_currentCharacter.CariedObjectCommands);
        }
    }
}
