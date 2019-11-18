using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iContainCaryables
{
    List<iCaryable> cariedObjects { get; }
    int numberOfCarriedObjects { get;  }
}
