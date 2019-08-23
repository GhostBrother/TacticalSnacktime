using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class MapGenerator : MonoBehaviour
{

    [SerializeField]
    private Tile _tileToGenerate;

    string path;
    string jsonString;

    [SerializeField]
    private float _horizontalDistanceBetweenTiles;

    [SerializeField]
    private float _verticalDistanceBetweenTiles;

    private int _rows;
    private int _columns;

    public int MapSize
    { get
        {
            if (jObject == null) { Init(); }
            return _rows * _columns;
        }
    }

    private Pathfinding pf;

    JObject jObject;

    Dictionary<char, Func<AbstractPawn>> editorLookUp;

    public void Init()
    {
        path = "Assets/JsonMaps/TestMap.json";
        using (StreamReader r = new StreamReader(path))
        {
            jsonString = r.ReadToEnd();
            jObject = JObject.Parse(jsonString);
            _rows = jObject["Map"].Count();
            _columns = jObject["Map"][0].ToString().Length;
        }

    }

    private void LoadDictionary()
    {
        editorLookUp = new Dictionary<char, Func<AbstractPawn>>();
        editorLookUp.Add('G', Clone<Grill>);
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
            pawnToPlace.characterCoaster = CharacterCoasterPool.Instance.SpawnFromPool();
            pawnToPlace.TilePawnIsOn = temp.GetComponent<Tile>();
        }

        return temp.gameObject;

    }

    private AbstractPawn Clone<T>() where T : AbstractPawn, new()
    {
        return new T();
    }
}

  
