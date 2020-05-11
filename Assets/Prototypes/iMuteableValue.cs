using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface iMuteableValue<T> 
{
    T valueToStore { get; }
    T Increment(T toAdd);
    T Decrement(T toTake);
    string Format();
    Text textRefrence { get; set; }
    void updateTextRefrence();
}
