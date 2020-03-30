using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iPawn
{
    Tile TilePawnIsOn { get; set; }
    CharacterCoaster characterCoaster { get; set; }
}
