using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iEndOfDayState 
{
    void DisplayProps();
    void HideProps();
    EndOfDayPannel EndOfDayPannel {  set; }
    ActionButton ButtonForState { set; }
}
