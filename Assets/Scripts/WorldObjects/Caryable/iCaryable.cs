using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iCaryable
{
    string Name { get; }
    int HandsRequired { get; }
    Sprite CaryableObjectSprite { get; }
    List<Command> HeldObjectCommands { get; }
    int NumberOfItemsInSupply { get; set; }
    iCaryable Copy();
}
