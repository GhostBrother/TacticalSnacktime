using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class IncrementValue<T> : Command
{
    iMuteableValue<T> number;

    T ammountToAdd;


    public iCommandKind typeOfCommand { get; set; }

    public bool isUsable => true;

    public string CommandName { get { return string.Empty; }  }

    public IncrementValue(iMuteableValue<T> value, T _amountToAdd)
    {
        number = value;
        typeOfCommand = new CloseMenu();
        ammountToAdd = _amountToAdd;
    }


    public void execute()
    {
        number.Increment(ammountToAdd);
        number.updateTextRefrence();
    }
}
