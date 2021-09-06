using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map {

    private Tile[][] tilesOnMap;
    public Tile[][] TilesOnMap { get { return tilesOnMap; } private set { tilesOnMap = value; } }

    List<Tile> deployTiles;

    public Map(int _rows, int _columns)
    {
        deployTiles = new List<Tile>();
        tilesOnMap = new Tile[_rows][];
        for (int i = 0; i < TilesOnMap.Length; i++)
        {
            tilesOnMap[i] = new Tile[_columns];
        }

    }

    public Tile GetTileWithType(EnumHolder.EntityType entityType)
    {
        foreach (Tile[] tileArray in TilesOnMap)
        {
            foreach (Tile t in tileArray)
            {
                if(t.EntityTypeOnTile == entityType)
                {
                    return t;
                }
            }
        }
        return null;
    }

    public Tile GetTileAtRowAndColumn(int row, int column)
    {
        return tilesOnMap[row][column];
    }

    public void AddTileToMap(Tile tileToAdd, int row, int column)
    {
        tilesOnMap[row][column] = tileToAdd;
        if (tileToAdd.IsDeployTile)
        {
            tileToAdd.EntityTypeOnTile = EnumHolder.EntityType.Clear;
            deployTiles.Add(tileToAdd);
        }
    }

    public void DeactivateAllTiles()
    {
        for(int x = 0; x < tilesOnMap.Length; x++)
        {
            for(int y = 0; y < tilesOnMap[x].Length ; y++)
            tilesOnMap[x][y].DeactivateTile();
        }
    }

    public void AcivateAllDeployTiles(Action<Tile> deployState)
    {
        for (int i = 0; i < deployTiles.Count; i++)
        {
              if(deployTiles[i].EntityTypeOnTile == EnumHolder.EntityType.Clear)
            {
                deployTiles[i].onClick = deployState;
            }

        }
    }
}
