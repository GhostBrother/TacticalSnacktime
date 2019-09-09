using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPawn : MonoBehaviour, iAffectedByTime , iPawn 
{

    private CharacterCoaster _characterCoaster;
    public CharacterCoaster characterCoaster
    {
        get { return _characterCoaster; }
        set { _characterCoaster = value;
           _characterCoaster.CharacterSprite = PawnSprite;

        }
    }

    public MonoPool _monoPool { get; set; }

    public CharacterCoaster ItemCoaster
    { get; set; }

    public CharacterCoaster NeedCoaster
    { get; set; }

    public CharacterCoaster FoodWantCoaster
    { get; set; }

    private Tile previousTile;

    private Tile tilePawnIsOn;
    public virtual Tile TilePawnIsOn
    {
        get { return tilePawnIsOn; }
        set
        {
            previousTile = tilePawnIsOn;
            tilePawnIsOn = value;
            if (previousTile != null)
            {
                PathRequestManager.RequestPath(previousTile, value, characterCoaster.MoveAlongPath);
            }
            else
            {
                _characterCoaster.transform.position = new Vector3(TilePawnIsOn.transform.position.x, TilePawnIsOn.transform.position.y, -0.5f);
            }

            tilePawnIsOn.ChangeState(tilePawnIsOn.GetActiveState());
            ChangeTileWeight();

            tilePawnIsOn.EntityTypeOnTile = EntityType;
           
        }
    }

    public string Name { get; protected set; }

    public Sprite PawnSprite { get; protected set; }

    public EnumHolder.EntityType EntityType { get; protected set; }
 
    public Action<Character> onStartTurn { get; set; }
    public Action onTurnEnd { get; set; }

    public int TurnOrder { get; set; }

    public void MoveToPreviousTile()
    {
        tilePawnIsOn.ChangeState(tilePawnIsOn.GetClearState());
        tilePawnIsOn.EntityTypeOnTile = EnumHolder.EntityType.None;
        tilePawnIsOn = previousTile;

        characterCoaster.transform.position = new Vector3(previousTile.transform.position.x, previousTile.transform.position.y, -0.5f);

        previousTile.ChangeState(tilePawnIsOn.GetActiveState());
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
        }
    }

     void ChangeTileWeight()
    {
        tilePawnIsOn.movementPenalty = 255;
    }

    public virtual void TurnStart()
    {

    }

    public virtual void TurnEnd()
    {

    }
}
