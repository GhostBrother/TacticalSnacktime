using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionMenu : MonoBehaviour
{

    public delegate void OnTurnEnd();
    public OnTurnEnd onTurnEnd;

    List<Command> commands;

    [SerializeField]
    Camera cam;

    [SerializeField]
    ActionButton buttonPrefab;

    List<ActionButton> actionButtons;

    public void Start()
    {
        commands = new List<Command>();
        actionButtons = new List<ActionButton>();
    }

    public void ShowActionsAtTile(Tile tileWithCommands)
    {

        this.transform.position = tileWithCommands.transform.position;

        commands.Add(new EndTurn());

        if (commands.Count > actionButtons.Count)
        {
            int temp = commands.Count - actionButtons.Count;
            for (int i = 0; i < temp; i++)
            {
                ActionButton tempButton = Instantiate(buttonPrefab, this.transform);
                tempButton.onActionTaken += HideAllActions;
                tempButton.onActionTaken += EndTurn;
                actionButtons.Add(tempButton);
            }
        }

        for(int i = 0; i < commands.Count; i++)
        {
            actionButtons[i].gameObject.SetActive(true);
            actionButtons[i].StoredCommand = commands[i];
            actionButtons[i].transform.position = cam.WorldToScreenPoint(new Vector3(this.transform.position.x, this.transform.position.y + (i * actionButtons[i].gameObject.transform.lossyScale.y), this.transform.position.z));
        }

    }

    public void HideAllActions()
    {
        for (int i = 0; i < actionButtons.Count; i++)
        {
            actionButtons[i].gameObject.SetActive(false);
        }
        commands.Clear();
    }

    public void EndTurn()
    {
        onTurnEnd.Invoke();
    }

    public void AddCommandToList(Command command)
    {
        commands.Add(command);
    }

}
