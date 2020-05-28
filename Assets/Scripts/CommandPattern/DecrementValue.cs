using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecrementValue<T> : Command
{

    // iMuteableValue<T> number;

    InputField _valueText;

    T ammountToAdd;

    public iCommandKind typeOfCommand { get; set; }

    public bool isUsable => true;

    public string CommandName { get { return string.Empty; } }

    public DecrementValue(InputField valueText) // iMuteableValue<T> number; T _amountToAdd
    {
        _valueText = valueText;
        typeOfCommand = new CloseMenu();
       // ammountToAdd = _amountToAdd;
    }

    public void execute()
    {
        int i;
        Int32.TryParse(_valueText.text, out i);
        i--;
        _valueText.text = i.ToString();
        //number.Decrement(ammountToSubtract);
        //number.updateTextRefrence();
    }
}
