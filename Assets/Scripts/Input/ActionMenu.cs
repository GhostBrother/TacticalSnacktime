using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionMenu : MonoBehaviour
{
    public delegate void OnButtonClick();
    public OnButtonClick onButtonClick;

    [SerializeField]
    Camera cam;

    [SerializeField]
    ActionButton buttonPrefab;

    List<ActionButton> actionButtons;

    public void Start()
    {
        actionButtons = new List<ActionButton>();
    }   

    public void ShowActionsAtTile(Tile tile)
    {
        this.transform.position = tile.transform.position;
    }

    public void OpenMenu(List<Command> commands)
    {
        if (commands.Count > actionButtons.Count)
        {
            int temp = commands.Count - actionButtons.Count;
            for (int i = 0; i < temp; i++)
            {
                ActionButton tempButton = Instantiate(buttonPrefab, this.transform);
                actionButtons.Add(tempButton);
            }
        }

        for (int i = 0; i < commands.Count; i++)
        {
            actionButtons[i].gameObject.SetActive(true);
            commands[i].typeOfCommand.LoadNewMenu = OpenMenu;
            actionButtons[i].StoredCommand = commands[i];
            actionButtons[i].enabled = commands[i].isUsable;
            actionButtons[i].onActionTaken = HideAllActions;
            actionButtons[i].onActionTaken += onButtonClick.Invoke;
            actionButtons[i].transform.position = cam.WorldToScreenPoint(new Vector3(this.transform.position.x, this.transform.position.y - (i * actionButtons[i].gameObject.transform.lossyScale.y), this.transform.position.z));
        }
    }

    public void HideAllActions()
    {
        for (int i = 0; i < actionButtons.Count; i++)
        {
            actionButtons[i].gameObject.SetActive(false);
        }
        actionButtons.Clear();
    }
}
