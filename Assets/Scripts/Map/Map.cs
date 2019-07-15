using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map {

    private Tile[][] tilesOnMap;
    public Tile[][] TilesOnMap { get { return tilesOnMap; } private set { tilesOnMap = value; } }

    private Tile trackedTile;



    public Map(int _rows, int _columns)
    {
        tilesOnMap = new Tile[_rows][];
        for (int i = 0; i < TilesOnMap.Length; i++)
        {
            tilesOnMap[i] = new Tile[_columns];
        }

    }

    public Tile GetTileAtRowAndColumn(int row, int column)
    {
        return tilesOnMap[row][column];
    }

    public void AddTileToMap(Tile tileToAdd, int row, int column)
    {
        tilesOnMap[row][column] = tileToAdd;
    }

    public void DeactivateAllTiles()
    {
        for(int x = 0; x < tilesOnMap.Length; x++)
        {
            for(int y = 0; y < tilesOnMap[x].Length ; y++)
            tilesOnMap[x][y].DeactivateTile();
        }
    }

    public void SetStartTile(Tile tile)
    {
        trackedTile = tile;
    }

    public void SetEndTile(Tile tile)
    {
        
        tile.ChangeState(tile.GetActiveState());
        trackedTile.ChangeState(trackedTile.GetClearState());           
        trackedTile = null;
    }

}
