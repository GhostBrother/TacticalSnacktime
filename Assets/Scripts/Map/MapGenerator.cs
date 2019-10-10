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

    Dictionary<char, Func<AbstractPawn>> editorLookUp;

    public void Start()
    {
        Init("Assets/JsonMaps/TestMap.json");
    }

    public override void Init(string filePath)
    {
        base.Init(filePath);
       _rows = jObject["Map"].Count();
       _columns = jObject["Map"][0].ToString().Length;
        
    }

    private void LoadDictionary()
    {
        editorLookUp = new Dictionary<char, Func<AbstractPawn>>();
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
            for (int x = 0; x < jObject["Map"][y].ToString().Length; x++)
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

                temp = PlacePawnOnTile(jObject["Map"][y].ToString()[x], temp);

                temp.GetComponent<Tile>().SetXandYPos(x, y);
                mapToReturn.AddTileToMap(temp.GetComponent<Tile>(), x, y);
                prevTile = temp.GetComponent<Tile>();
            }
        }
        return mapToReturn;
    }

    private GameObject PlacePawnOnTile(char marker, GameObject temp)
    {
        if (editorLookUp.ContainsKey(marker))
        {
            AbstractPawn pawnToPlace = editorLookUp[marker].Invoke();
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

    private AbstractPawn Clone<T>() where T : AbstractPawn, new()
    {
        return new T();
    }

    private AbstractPawn AddRecipies<T>() where T : AbstractPawn, new()
    {
        if (cookingStationFactory == null)
        {
            cookingStationFactory = new FoodContainerFactory();
        }
        AbstractPawn toDecorate = new T();
        toDecorate = cookingStationFactory.LoadCookStation(toDecorate);
        return toDecorate;
    }

    private AbstractPawn BundleSuppply<T>() where T : Supply , new()
    {

        if (cookingStationFactory == null)
        {
            cookingStationFactory = new FoodContainerFactory();
        }
        Supply supply = new T();
            supply.HandsRequired = 1;
            //Temp
            supply.NumberOfItemsInSupply = 1;
            supply.NameOfFoodInSupply = "Patty";
            supply = cookingStationFactory.LoadSupply(supply);
        return supply;
    }

}

  
