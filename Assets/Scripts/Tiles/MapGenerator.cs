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

    public int MapSize {get {return _rows * _columns;}}

    private Pathfinding pf;


    public Map generateMap()
    {
       
        Map mapToReturn = new Map(_rows,_columns);
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
                    temp.GetComponent<Tile>().SetNeighbor(mapToReturn.GetTileAtRowAndColumn((x - 1),  y));
                    Tile tile = mapToReturn.GetTileAtRowAndColumn((x - 1) ,  y);
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

                if (y == 3 && x == 5 || y == 3 && x == 3)
                {
                    Grill grillTest = new Grill();
                    temp.GetComponent<Tile>().TargetableOnTile = grillTest;
                    grillTest.TilePawnIsOn = temp.GetComponent<Tile>();
                }

                temp.GetComponent<Tile>().SetXandYPos(x,y);
                mapToReturn.AddTileToMap(temp.GetComponent<Tile>(), x, y);
                prevTile = temp.GetComponent<Tile>();
            }
            
        }
        return mapToReturn;
        
    }
}
