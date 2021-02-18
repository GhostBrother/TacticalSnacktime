﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour, iHeapItem<Tile> {


    public List<Tile> neighbors { get; }

    public Action<Tile> onClick { get; set; }

    // Feasibly abstract this out to like an iNavigatable or something Factor this out
    public int gCost { get; set; }
    public int hCost { get; set; }
    public int fCost { get { return hCost + gCost; } }
    public int GridX { get; private set; }
    public int GridY { get; private set; }


    private int _movementPenalty;
    public int movementPenalty { get { return _movementPenalty; } set { _movementPenalty  = value;} }

    int heapIndex;
    public int HeapIndex { get { return heapIndex; } set { heapIndex = value; } }

    public Tile Parent { get; set; }

    [SerializeField]
    SpriteRenderer backgroundTile;
    public SpriteRenderer BackgroundTile { get { return backgroundTile; } private set {; } }

    [SerializeField]
    Color activeColor;
    public Color ActiveColor { get { return activeColor; } private set {; } }

    [SerializeField]
    Color deactiveColor;
    public Color DeactiveColor { get { return deactiveColor; } private set {; } }

    EnumHolder.EntityType entityTypeOnTile;
    public EnumHolder.EntityType EntityTypeOnTile { get { return entityTypeOnTile; } set { entityTypeOnTile = value; } }

    [SerializeField]
    Color deployZoneColor;
    public Color DeployZoneColor { get { return deployZoneColor; }  }

    private iTargetable targetableOnTile; 
    public iTargetable TargetableOnTile { get { return targetableOnTile; } set { targetableOnTile = value; } }

    public bool IsTargetableOnTile { get { return targetableOnTile != null; }  }

    public bool IsDeployTile { get; set; }
     
    public Tile()
    {
        neighbors = new List<Tile>();
       
        movementPenalty = 0;

    }

    public void SetXandYPos(int gridX, int gridY)
    {
        GridX = gridX;
        GridY = gridY;
    }

    public void DeactivateTile()
    {
        if (EntityTypeOnTile == EnumHolder.EntityType.Clear)
        {
            movementPenalty = 0;
            targetableOnTile = null;
        }

        onClick = null;
        BackgroundTile.color = DeactiveColor;
    }

    public void SetNeighbor(Tile newNeighbor)
    {
        neighbors.Add(newNeighbor);
    }

    public void SelectTile()
    {
        if (onClick != null)
            onClick.Invoke(this);
    }

    public void ColorAllAdjacent(int numToHilight, Action<Tile> actionForTile, EnumHolder.EntityType entityToFind)
    {
        if (numToHilight >= 0)
        {
            numToHilight--;

            if (entityToFind.HasFlag(EntityTypeOnTile))
            {
                backgroundTile.color = ActiveColor;
                onClick = actionForTile;
            }

            for (int i = 0; i < neighbors.Count; i++)
            {
                if (entityToFind.HasFlag(neighbors[i].EntityTypeOnTile))
                {
                    neighbors[i].ColorAllAdjacent(numToHilight, actionForTile, entityToFind);
                }
            }
        }
    }

    public void ClearAllAdjacent(int numToClear)
    {
        if (numToClear >= 0)
        {
            numToClear--;
            DeactivateTile();

            for (int i = 0; i < neighbors.Count; i++)
            {
              neighbors[i].ClearAllAdjacent(numToClear);
            }
        }
    }

    public int CompareTo(Tile TileToCompare)
    {
        int compare = fCost.CompareTo(TileToCompare.fCost);
        if(compare == 0)
            compare = hCost.CompareTo(TileToCompare.hCost);

        return -compare;
         
    }
}
