using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecrementValue<T> : Command
{

    InputField _valueText;

    T ammountToAdd;

    public iCommandKind typeOfCommand { get; set; }

    public bool isUsable => true;

    public string CommandName { get { return string.Empty; } }

    public DecrementValue(InputField valueText)
    {
        _valueText = valueText;
        typeOfCommand = new CloseMenu();
    }

    public void execute()
    {
        int i;
        Int32.TryParse(_valueText.text, out i);
        if (i > 0)
        {
            i--;
            _valueText.text = i.ToString();
        }
    }
}
