using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : iMuteableValue<decimal>
{
    public decimal valueToStore { get; set; }
    public Text textRefrence { get; set; }

    public string Format()
    {
       return string.Format(" {0:C}", valueToStore);
    }

    public decimal Increment(decimal toAdd)
    {
        return valueToStore += toAdd;
    }

    public decimal Decrement(decimal toTake)
    {
        return valueToStore -= toTake;
    }

    public void updateTextRefrence()
    {
        textRefrence.text = Format();
    }
}
