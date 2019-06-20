using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionMenu : MonoBehaviour
{
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

        if (commands.Count > actionButtons.Count)
        {
            int temp = commands.Count - actionButtons.Count;
            for (int i = 0; i < temp; i++)
            {
                ActionButton tempButton = Instantiate(buttonPrefab, this.transform);
                // Circular dependancy HACK
                tempButton.actionMenu = this;
                actionButtons.Add(tempButton);
                tempButton.transform.position = cam.WorldToScreenPoint(new Vector3 (this.transform.position.x, this.transform.position.y + (i * tempButton.gameObject.transform.lossyScale.y), this.transform.position.z));//this.transform.position;
            }
        }

        for(int i = 0; i < commands.Count; i++)
        {
            actionButtons[i].gameObject.SetActive(true);
            actionButtons[i].StoredCommand = commands[i];
        }

    }

    public void HideAllActions()
    {
        for(int i = 0; i < actionButtons.Count; i++)
        {
            actionButtons[i].gameObject.SetActive(false);
        }
        commands.Clear();
    }

    public void AddCommandToList(Command command)
    {
        commands.Add(command);
    }

}
