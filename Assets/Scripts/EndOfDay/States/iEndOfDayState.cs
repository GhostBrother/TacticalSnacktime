using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iEndOfDayState 
{
    void DisplayProps();
    void HideProps();
    ActionButton ButtonForState { set; }
    void OnStartNextDay();
}
