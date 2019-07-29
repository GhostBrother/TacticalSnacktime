using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPawn : MonoBehaviour, iPawn 
{
    private CharacterCoaster _characterCoaster;
    public CharacterCoaster characterCoaster
    {
        get { return _characterCoaster; }
        set { _characterCoaster = value;
            _characterCoaster.CharacterSprite = PawnSprite;

        }
    }

    public CharacterCoaster _itemCoaster;
    public CharacterCoaster ItemCoaster
    {
        get { return _itemCoaster; }
        set
        {
            _itemCoaster = value;
        }
    }


    private Tile tilePawnIsOn;
    public virtual Tile TilePawnIsOn
    {
        get { return tilePawnIsOn; }
        set
        {
            Tile temp = tilePawnIsOn;
            tilePawnIsOn = value;
            if (temp != null)
            {
               
                PathRequestManager.RequestPath(temp, value, characterCoaster.MoveAlongPath);
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

    public Sprite PawnSprite { get; protected set; }

    public  Sprite ItemSprite { get; protected set; }

    public EnumHolder.EntityType EntityType { get; protected set; }
    

    public void ShowItem()
    {
        // tilePawnIsOn.FoodImageRenderer.sprite = ItemSprite;
        _itemCoaster = CharacterCoasterPool.Instance.SpawnFromPool(new Vector3(_characterCoaster.transform.position.x, _characterCoaster.transform.position.y, -1f),Quaternion.identity);
        _itemCoaster.transform.SetParent(_characterCoaster.transform);
        _itemCoaster.gameObject.GetComponent<SpriteRenderer>().sprite = ItemSprite;
    }

    public void HideItem()
    {
        //tilePawnIsOn.FoodImageRenderer.sprite = null;
        _itemCoaster.transform.parent = null;
        CharacterCoasterPool.Instance.PutBackInPool(_itemCoaster);
    }

     void ChangeTileWeight()
    {
        tilePawnIsOn.movementPenalty = 255;
    }

}
