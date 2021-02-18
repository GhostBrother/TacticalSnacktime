﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMenu : MonoBehaviour
{
    public Action onButtonClick;

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

        onButtonClick.Invoke();

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
            actionButtons[i].gameObject.SetActive(commands[i].isUsable);
            commands[i].typeOfCommand.LoadNewMenu = OpenMenu;
            commands[i].typeOfCommand.CloseMenu = HideAllActions;
            actionButtons[i].StoredCommand = commands[i];
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
