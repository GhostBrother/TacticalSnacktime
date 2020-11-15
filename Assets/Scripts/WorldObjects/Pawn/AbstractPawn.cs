﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPawn : MonoBehaviour , iPawn 
{

    private CharacterCoaster _characterCoaster;
    public CharacterCoaster characterCoaster
    {
        get { return _characterCoaster; }
        set { _characterCoaster = value;
            _characterCoaster.facingSprites = PawnSprites;
            _characterCoaster.CharacterSprite = characterArt;
            _characterCoaster.curAnimation = animationClips;
        }
    }

    public MonoPool _monoPool { get; set; }

    CharacterCoaster _itemCoaster;

    public CharacterCoaster ItemCoaster
    {
        get {
            return _itemCoaster;
            }
        set {
            _itemCoaster = value;
            }
    }

    public CharacterCoaster NeedCoaster
    { get; set; }

    public CharacterCoaster FoodWantCoaster
    { get; set; }

    private Tile previousTile;
    protected Tile PreviousTile { get { return previousTile; } set{ previousTile = value; } }

    private Tile tilePawnIsOn;
    public virtual Tile TilePawnIsOn
    {
        get { return tilePawnIsOn; }
        set
        {
                previousTile = tilePawnIsOn;
                tilePawnIsOn = value;
                if (previousTile == null)
                {
                    characterCoaster.PlaceCoasterOnTile(TilePawnIsOn);
                }
                ChangeTileWeight();

                tilePawnIsOn.EntityTypeOnTile = EntityType;

        }
    }

    public string Name { get; set; }

    public Sprite characterArt { get; set; }

    // for our unit's facing
    public List<Sprite> PawnSprites { get; set; }

    public RuntimeAnimatorController animationClips { get; set; }

    public int ID { get; set; }

    public EnumHolder.EntityType EntityType { get;  set; }

    public int TurnOrder { get; set; }

    public void MoveToPreviousTile()
    {
        tilePawnIsOn.DeactivateTile();
        tilePawnIsOn.EntityTypeOnTile = EnumHolder.EntityType.None;
        tilePawnIsOn = previousTile;

         characterCoaster.PlaceCoasterOnTile(previousTile);


        previousTile.DeactivateTile();
        ChangeTileWeight();

        previousTile.EntityTypeOnTile = EntityType;

    }


    public void ShowCoaster(Sprite sprite, Action<CharacterCoaster> setOutput)
    {
       ShowCoasterWithOffset(sprite, 0,0, setOutput);
    }

    public void ShowCoasterWithOffset( Sprite sprite , float offsetX, float offsetY, Action<CharacterCoaster> setOutput)
    {
            CharacterCoaster coaster = _monoPool.GetCharacterCoasterInstance();
            coaster.transform.parent = _characterCoaster.transform;
            coaster.transform.position = new Vector3(_characterCoaster.transform.position.x + offsetX, _characterCoaster.transform.position.y + offsetY, -1);
            coaster.gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
            setOutput(coaster);
    }

    public void HideCoaster(CharacterCoaster coasterToHide)
    {
        if (coasterToHide != null)
        {
            coasterToHide.transform.parent = null;
            _monoPool.PutInstanceBack(coasterToHide.gameObject);
            coasterToHide = null;

        }
    }

     void ChangeTileWeight()
    {
        tilePawnIsOn.movementPenalty = 255;
    }

    public void RemovePawn(CharacterCoaster coasterToHide)
    {
        HideCoaster(coasterToHide);
        tilePawnIsOn = null;
        previousTile = null;
    }

    public virtual void TurnStart()
    {

    }

    public virtual void TurnEnd()
    {
    }
}
