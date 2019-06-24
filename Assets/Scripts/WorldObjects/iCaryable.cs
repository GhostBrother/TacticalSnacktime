using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Anything that can be lifted and put down
// Each caryable has a list of hands needed to lift it. 
// possibly a weight too. 

public interface iCaryable
{
    string Name { get; }
    int HandsRequired { get; }
    int Weight { get; }
    Sprite CaryableObjectSprite { get; }
}
