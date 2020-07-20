using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iCanGiveItems 
{
   iCaryable Give(int i);
    void GetRidOfItem(int i);
    List<iCaryable> cariedObjects { get; }
}
