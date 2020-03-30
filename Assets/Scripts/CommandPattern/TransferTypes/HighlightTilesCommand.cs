﻿using System;
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
    Action<Tile> actionForTiles;

    public HighlightTilesCommand(int _range, Tile _startTile, Action<Tile> _ActionForTiles)
    {
        range = _range;
        startTile = _startTile;
        actionForTiles = _ActionForTiles;
    }

    public Action<List<Command>> LoadNewMenu { get; set; }

    public void ActivateType()
    {
      startTile.ColorAllAdjacent(range, actionForTiles);
    }
}