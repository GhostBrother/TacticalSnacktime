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

    public void Start()
    {
        ActionMenuCommands = new List<Command>();
        actionButtons = new List<ActionButton>();
    }

    public void ShowActionsAtTile(Tile tileWithCommands)
    {

        this.transform.position = tileWithCommands.transform.position;

        ActionMenuCommands.Add(new EndTurn());

        if (ActionMenuCommands.Count > actionButtons.Count)
        {
            int temp = ActionMenuCommands.Count - actionButtons.Count;
            for (int i = 0; i < temp; i++)
            {
                ActionButton tempButton = Instantiate(buttonPrefab, this.transform);
                tempButton.onActionTaken += HideAllActions;
                tempButton.onActionTaken += EndTurn;
                actionButtons.Add(tempButton);
            }
        }

        for(int i = 0; i < ActionMenuCommands.Count; i++)
        {
            actionButtons[i].gameObject.SetActive(true);
            actionButtons[i].StoredCommand = ActionMenuCommands[i];
            actionButtons[i].transform.position = cam.WorldToScreenPoint(new Vector3(this.transform.position.x, this.transform.position.y + (i * actionButtons[i].gameObject.transform.lossyScale.y), this.transform.position.z));
        }

    }

    public void HideAllActions()
    {
        for (int i = 0; i < actionButtons.Count; i++)
        {
            actionButtons[i].gameObject.SetActive(false);
        }
        ActionMenuCommands.Clear();
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
        foreach(Command c in commands)
        ActionMenuCommands.Add(c);
    }

}
