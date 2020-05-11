using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reputation : iMuteableValue<int>
{
    public int valueToStore { get; private set; }
    public Text textRefrence { get; set; }

    public int Decrement(int toTake)
    {
        return valueToStore -= toTake;
    }

    public string Format()
    {
        return valueToStore.ToString();
    }

    public int Increment(int toAdd)
    {
        return valueToStore += toAdd;
    }

    public void updateTextRefrence()
    {
        textRefrence.text = Format();
    }
}
