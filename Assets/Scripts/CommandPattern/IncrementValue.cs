using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncrementValue<T> : Command
{

    InputField _valueText;

    T _ammountToAdd;


    public iCommandKind typeOfCommand { get; set; }

    public bool isUsable => true;

    public string CommandName { get { return string.Empty; }  }

    public IncrementValue(InputField valueText) 
    {
        _valueText = valueText;
        typeOfCommand = new NoTransfer();
    }


    public void execute()
    {
        int i = 0;
        Int32.TryParse(_valueText.text, out i);

        i++;
        _valueText.text = i.ToString();
    }
}
