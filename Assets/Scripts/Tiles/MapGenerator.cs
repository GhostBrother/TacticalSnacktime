using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {


    [SerializeField]
    private Tile _tileToGenerate;

    [SerializeField]
    private int _rows;

    [SerializeField]
    private int _columns;

    [SerializeField]
    private float _horizontalDistanceBetweenTiles;

    [SerializeField]
    private float _verticalDistanceBetweenTiles;

    private int MapSize {get {return _rows * _columns;}}

    private Pathfinding pf;


    public Map generateMap()
    {
       

        Map mapToReturn = new Map();
        Vector3 tilePos = new Vector3(0, 0, 0);
        Tile prevTile = null;
        for (int x = 0; x < _rows; x++)
        {
            for (int y = 0; y < _columns; y++)
            {
                GameObject temp =  Instantiate(_tileToGenerate.gameObject);
                tilePos.x = (x * _horizontalDistanceBetweenTiles);
                tilePos.y = (y * _verticalDistanceBetweenTiles);
                temp.transform.position = tilePos;

                if (x != 0)
                {
                    temp.GetComponent<Tile>().SetNeighbor(mapToReturn.GetTileAtIndex((x - 1) * _rows + y));
                    Tile tile = mapToReturn.GetTileAtIndex((x - 1) * _rows + y);
                    tile.SetNeighbor(temp.GetComponent<Tile>());
                }

                if (y != 0)
                {
                    temp.GetComponent<Tile>().SetNeighbor(prevTile);
                    prevTile.SetNeighbor(temp.GetComponent<Tile>());
                }

                if (y > 1)
                {
                    temp.GetComponent<Tile>().ChangeState(temp.GetComponent<Tile>().GetDeployState());
                }
             
                mapToReturn.AddTileToMap(temp.GetComponent<Tile>());
                prevTile = temp.GetComponent<Tile>();
            }
            
        }
        // Just to test
        pf = new Pathfinding(MapSize);
        pf.FindPath(mapToReturn.GetTileAtIndex(0), mapToReturn.GetTileAtIndex(11)); 
        return mapToReturn;
        
    }
}
