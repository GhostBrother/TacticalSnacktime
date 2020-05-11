using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecrementValue<T> : Command
{
    iMuteableValue<T> number;
    T ammountToSubtract;

    public iCommandKind typeOfCommand { get; set; }

    public bool isUsable => true;

    public string CommandName { get { return string.Empty; } }

    public DecrementValue(iMuteableValue<T> value, T _amountToSubtract)
    {
        number = value;
        typeOfCommand = new CloseMenu();
        ammountToSubtract = _amountToSubtract;
    }

    public void execute()
    {
        number.Decrement(ammountToSubtract);
        number.updateTextRefrence();
    }
}
