using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupOfTiles : Space
{

    private List<Tile> TilesInGroup;

    public GroupOfTiles(List<Tile> tilesInGroup)
    {
        TilesInGroup = tilesInGroup;
    }
    public override void ColorAllAdjacent(int numToHilight)
    {
        for (int i = 0; i < TilesInGroup.Count; i++)
        {
            TilesInGroup[i].ColorAllAdjacent(numToHilight);
        }
    }

    public override void SelectTile()
    {
        for (int i = 0; i < TilesInGroup.Count; i++)
        {
           TilesInGroup[i].GetCurrentState().TileClicked();
        }
    }
}
