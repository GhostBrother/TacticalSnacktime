using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map {

    private List<Tile> tilesOnMap;
    public List<Tile> TilesOnMap { get { return tilesOnMap; } private set { tilesOnMap = value; } }

    private Tile trackedTile;

    public Map()
    {
        tilesOnMap = new List<Tile>();
    }

    public Tile GetTileAtIndex(int index)
    {
        return tilesOnMap[index];
    }

    public void AddTileToMap(Tile tileToAdd)
    {
        tilesOnMap.Add(tileToAdd);
    }

    public void DeactivateAllTiles()
    {
        for(int i = 0; i < tilesOnMap.Count; i++)
        {
            tilesOnMap[i].DeactivateTile();
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
