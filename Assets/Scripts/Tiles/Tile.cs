﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {


    private List<Tile> neighbors;

    [SerializeField]
    SpriteRenderer backgroundTile;
    public SpriteRenderer BackgroundTile { get { return backgroundTile; } private set {; } }

    [SerializeField]
    Color activeColor;
    public Color ActiveColor { get { return activeColor; } private set {; } }

    [SerializeField]
    Color deactiveColor;
    public Color DeactiveColor { get { return deactiveColor; } private set {; } }

    [SerializeField]
    Color deployZoneColor;
    public Color DeployZoneColor { get { return deployZoneColor; } private set {; } }

    private iTileState clear;
    private iTileState hilighted;
    private iTileState activeCharacter;
    private iTileState tiredCharacterTile;
    private iTileState deployZoneTile;
    private iTileState curentState;

    private Character _character;

    public Character CharacterOnTile
    {
        get { return _character; }
        set {
            _character = value;
            ChangeState(activeCharacter);
        }
    }

    public Tile()
    {
        neighbors = new List<Tile>();
        clear = new ClearTile(this);
        hilighted = new HilightedTile(this);
        activeCharacter = new ActiveCharacterTile(this);
        tiredCharacterTile = new TiredCharacterTile(this);
        deployZoneTile = new DeployZoneTile(this);
        
        curentState = clear;
       

    }

    public bool CanTileBeActivated()
    {
        return curentState == activeCharacter;
    }

    public void ChangeState(iTileState newState)
    {
        curentState = newState;
        curentState.ChangeColor();
    }

    public iTileState GetClearState()
    {
        if (_character != null) { _character = null; }
        return clear;
    }

    public iTileState GetActiveState()
    {
        return activeCharacter;
    }

    public iTileState GetHilightedState()
    {
        return hilighted;
    }

    public iTileState GetTiredState()
    {
        return tiredCharacterTile;
    }

    public iTileState GetDeployState()
    {
        return deployZoneTile;
    }

    public iTileState GetCurrentState()
    {
        return curentState;
    }

    public void DeactivateTile()
    {
        if (_character == null)
        {
            ChangeState(clear);
        }
    }

    public void SetNeighbor(Tile newNeighbor)
    {
        neighbors.Add(newNeighbor);
    }

    public void SelectTile( )
    {
        curentState.TileClicked();
    }

    public void ColorAllAdjacent(int numToHilight)
    {
        if (numToHilight >= 0)
        {
            numToHilight--;

            if(CharacterOnTile == null)
            ChangeState(hilighted);

            for (int i = 0; i < neighbors.Count; i++)
            {
                if(neighbors[i].CharacterOnTile == null)
                { 
                neighbors[i].ColorAllAdjacent(numToHilight);

                }
            }
        }
    }

}
