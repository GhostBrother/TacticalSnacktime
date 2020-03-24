using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Commands that when selected hilight a number of tiles away from a start tile
///  Some examples are Move, trade, and punch
/// </summary>

public class HighlightTilesCommand : iCommandKind
{
    int range;
    Tile startTile;

    public HighlightTilesCommand(int _range, Tile _startTile)
    {
        range = _range;
        startTile = _startTile;
    }

    public Action<List<Command>> LoadNewMenu { get; set; }

    public void ActivateType()
    {
      startTile.ColorAllAdjacent(range);
    }
}
