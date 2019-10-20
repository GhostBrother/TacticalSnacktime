using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class MapGenerator : JsonLoader<Map>
{

    [SerializeField]
    private Tile _tileToGenerate;

    [SerializeField]
    MonoPool _monoPool;

    [SerializeField]
    private float _horizontalDistanceBetweenTiles;

    [SerializeField]
    private float _verticalDistanceBetweenTiles;

    FoodContainerFactory cookingStationFactory;

    private int _rows;
    private int _columns;


    public int MapSize
    { get
        {
            return _rows * _columns;
        }
    }

    Dictionary<char, Func<string,AbstractPawn>> editorLookUp;

    public void Start()
    {
        Init("Assets/JsonMaps/TestMap.json");
    }

    public override void Init(string filePath)
    {
        base.Init(filePath);
        _rows = jObject["Map"].Count();
        _columns = jObject["Map"][0].ToString().Split(' ').Length;
    }

    private void LoadDictionary()
    {
        editorLookUp = new Dictionary<char, Func<string,AbstractPawn>>();
        editorLookUp.Add('G', AddRecipies<Grill>);
        editorLookUp.Add('S', BundleSuppply<Supply>);
        editorLookUp.Add('R', Clone<Register>);
        editorLookUp.Add('D', Clone<Door>);
        editorLookUp.Add('W', Clone<Wall>);

    }

    public Map generateMap()
    {
        LoadDictionary();
        GameObject temp = null;
        Vector3 tilePos = new Vector3(0, 0, 0);
        Tile prevTile = null;
        Map mapToReturn = new Map(_rows, _columns);

        for (int y = 0; y < _rows; y++)
        {
            string[] tiles = jObject["Map"][y].ToString().Split(' ');

            for(int x = 0; x < tiles.Length; x++)
            {
                temp = Instantiate(_tileToGenerate.gameObject);
                tilePos.x = (x * _horizontalDistanceBetweenTiles);
                tilePos.y = (y * -_verticalDistanceBetweenTiles);
                temp.transform.position = tilePos;

                if (y != 0)
                {
                    temp.GetComponent<Tile>().SetNeighbor(mapToReturn.GetTileAtRowAndColumn(x, (y-1)));
                    mapToReturn.GetTileAtRowAndColumn(x, (y - 1)).SetNeighbor(temp.GetComponent<Tile>());
                }


                if (x != 0)
                {
                    temp.GetComponent<Tile>().SetNeighbor(prevTile);
                    prevTile.SetNeighbor(temp.GetComponent<Tile>());
                }

                if (y >= 1)
                {
                    temp.GetComponent<Tile>().ChangeState(temp.GetComponent<Tile>().GetDeployState());
                }

                temp = PlacePawnOnTile(tiles[x], temp);

        temp.GetComponent<Tile>().SetXandYPos(x, y);
                mapToReturn.AddTileToMap(temp.GetComponent<Tile>(), x, y);
                prevTile = temp.GetComponent<Tile>();
            }
        }
        return mapToReturn;
    }

    private GameObject PlacePawnOnTile(string marker, GameObject temp)
    {
        if (editorLookUp.ContainsKey(marker[0]))
        {
            AbstractPawn pawnToPlace = editorLookUp[marker[0]].Invoke(marker);
            if (pawnToPlace is AbstractInteractablePawn)
            {
                temp.GetComponent<Tile>().TargetableOnTile = (AbstractInteractablePawn)pawnToPlace;
            }
            pawnToPlace.characterCoaster = _monoPool.GetCharacterCoasterInstance();
            pawnToPlace._monoPool = _monoPool; 
            pawnToPlace.TilePawnIsOn = temp.GetComponent<Tile>();
        }

        return temp.gameObject;

    }

    private AbstractPawn Clone<T>(string args) where T : AbstractPawn, new()
    {
        return new T();
    }

    private AbstractPawn AddRecipies<T>(string CookStationType) where T : AbstractPawn, new()
    {
        if (cookingStationFactory == null)
        {
            cookingStationFactory = new FoodContainerFactory();
        }
        AbstractPawn toDecorate = new T();
        toDecorate = cookingStationFactory.LoadCookStation(toDecorate);
        return toDecorate;
    }

    private AbstractPawn BundleSuppply<T>(string SupplyTypeAndNumber) where T : Supply , new()
    {

        if (cookingStationFactory == null)
        {
            cookingStationFactory = new FoodContainerFactory();
        }
        Supply supply = new T();

       supply = cookingStationFactory.LoadSupply(supply, SupplyTypeAndNumber);
        return supply;
    }

}

  
