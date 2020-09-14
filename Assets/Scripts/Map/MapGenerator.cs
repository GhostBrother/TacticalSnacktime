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

    FoodContainerFactory cookingStationFactory;


    // Hack for now
    GameManager _Gm; 

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
        Init("Assets/JsonMaps/TestMap.json"); //"Assets/JsonMaps/TestMap.json" "Assets/JsonMaps/MapTwo.json"
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
        editorLookUp.Add('G', AddRecipies<AbstractCookingStation>); 
        editorLookUp.Add('S', BundleSuppply<Supply>);
        editorLookUp.Add('R', Clone<Register>);
        editorLookUp.Add('D', Clone<Door>);
        editorLookUp.Add('E', Clone<EmployeeDoor>);
        editorLookUp.Add('W', Clone<Wall>);
    }

    // HACK
    public void SetGm(GameManager gm)
    {
        _Gm = gm;
        cookingStationFactory = new FoodContainerFactory();
        cookingStationFactory.AddToTimeline = _Gm.AddPawnToTimeline;
        cookingStationFactory.RemoveFromTimeline = _Gm.RemovePawnFromTimeline;
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
                Debug.Log(temp.GetComponentInChildren<SpriteRenderer>().sprite.rect.width);
                Debug.Log(temp.GetComponentInChildren<SpriteRenderer>().sprite.bounds.extents.y);
                tilePos.x = ((x - y) * ((temp.GetComponentInChildren<SpriteRenderer>().sprite.bounds.max.x * 10)));
                tilePos.y = ((y + x) * (-(temp.GetComponentInChildren<SpriteRenderer>().sprite.bounds.max.y * 5)));
                temp.transform.position = tilePos;
                AddDeployTile(tiles[x],temp.GetComponent<Tile>());

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

    private AbstractPawn AddRecipies<T>(string CookStationType) where T : AbstractCookingStation , new() 
    {
        AbstractCookingStation toDecorate = new T();
        toDecorate = cookingStationFactory.LoadCookStation(toDecorate,CookStationType);
        return toDecorate;
    }

    private AbstractPawn BundleSuppply<T>(string SupplyTypeAndNumber) where T : Supply , new()
    {         
        return cookingStationFactory.LoadSupply(SupplyTypeAndNumber);
    }

    private void AddDeployTile(string marker ,Tile tileToAdd)
    {
        if(marker[marker.Length -1] == 'P')
        tileToAdd.IsDeployTile = true;
    }

}

  
