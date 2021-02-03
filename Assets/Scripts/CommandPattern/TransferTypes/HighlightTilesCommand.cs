using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HighlightTilesCommand : iCommandKind
{
    int range;
    Tile startTile;
    Action<Tile> actionForTiles;
    EnumHolder.EntityType _typeToFind;

    public HighlightTilesCommand(int _range, Tile _startTile, Action<Tile> _ActionForTiles, EnumHolder.EntityType typeToFind)
    {
        range = _range;
        startTile = _startTile;
        actionForTiles = _ActionForTiles;
        _typeToFind = typeToFind;
    }
    
    
    public Action<List<Command>> LoadNewMenu { get; set; }
    public Action CloseMenu { get; set; }

    public void ActivateType()
    {
        startTile.ColorAllAdjacent(range, actionForTiles, _typeToFind);
        CloseMenu.Invoke();
    }

    public void UndoType()
    {
        startTile.ClearAllAdjacent(range);
    }
}
