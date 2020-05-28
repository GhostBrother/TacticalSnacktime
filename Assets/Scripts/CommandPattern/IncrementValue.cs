using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncrementValue<T> : Command
{
    // iMuteableValue<T> number;

    //Text _valueText;

   InputField _valueText;

    //T ammountToAdd;


    public iCommandKind typeOfCommand { get; set; }

    public bool isUsable => true;

    public string CommandName { get { return string.Empty; }  }

    public IncrementValue(InputField valueText) //iMuteableValue<T> value , // // T ammount To add
    {
        // number = value;
        _valueText = valueText;
        typeOfCommand = new CloseMenu();
        //ammountToAdd = _amountToAdd;
    }


    public void execute()
    {
        int i;
        Int32.TryParse(_valueText.text, out i);
        i++;
        _valueText.text = i.ToString();
        //number.Increment(ammountToAdd);
        //number.updateTextRefrence();
    }
}
