using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionMenu : MonoBehaviour
{

    public delegate void OnTurnEnd();
    public OnTurnEnd onTurnEnd;

    public delegate void AddTimedObjectToList(iAffectedByTime timedObject);
    public AddTimedObjectToList addTimed;

    List<Command> ActionMenuCommands;

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

    public void SetCurrentCharacter(Character character)
    {
        _currentCharacter = character;
    }

    public void ShowActionsAtTile()
    {
        this.transform.position = _currentCharacter.TilePawnIsOn.transform.position;
        OpenMenu();       
    }

     void OpenMenu()
    {
        GetAllActionsFromTile();

        if (ActionMenuCommands.Count > actionButtons.Count)
        {
            int temp = ActionMenuCommands.Count - actionButtons.Count;
            for (int i = 0; i < temp; i++)
            {
                ActionButton tempButton = Instantiate(buttonPrefab, this.transform); // ActionButton
                tempButton.onActionTaken += HideAllActions;
                tempButton.onActionTaken += OpenMenu;
                actionButtons.Add(tempButton);
            }
        }

        for (int i = 0; i < ActionMenuCommands.Count; i++)
        {
            actionButtons[i].gameObject.SetActive(true);
            actionButtons[i].StoredCommand = ActionMenuCommands[i];
            actionButtons[i].transform.position = cam.WorldToScreenPoint(new Vector3(this.transform.position.x, this.transform.position.y + (i * actionButtons[i].gameObject.transform.lossyScale.y), this.transform.position.z));
        }

        ActionButton endButton = Instantiate(buttonPrefab, this.transform);
        endButton.StoredCommand = new EndTurn(this);
        endButton.onActionTaken += HideAllActions;
        endButton.gameObject.SetActive(true);
        endButton.transform.position = cam.WorldToScreenPoint(new Vector3(this.transform.position.x, this.transform.position.y + (ActionMenuCommands.Count * endButton.gameObject.transform.lossyScale.y), this.transform.position.z));
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

    // Hack, mainly for grills and other selected objects.
    public void AddTimeAffectableToTimeline(iAffectedByTime timedObject)
    {
        addTimed.Invoke(timedObject);
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
