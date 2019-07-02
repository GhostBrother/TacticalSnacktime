using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPawn : MonoBehaviour, iPawn , iTargetable
{
    private Tile tilePawnIsOn;
    public Tile TilePawnIsOn
    {
        get { return tilePawnIsOn; }
        set
        {
            tilePawnIsOn = value;
            tilePawnIsOn.ChangeState(tilePawnIsOn.GetActiveState());
            ColorTile();
            ChangeTileWeight();
            tilePawnIsOn.TargetableOnTile = this;
            tilePawnIsOn.EntityTypeOnTile = EntityType;

        }
    }

    public Sprite PawnSprite { get; protected set; }

    public  Sprite ItemSprite { get; protected set; }

    public EnumHolder.EntityType EntityType { get; protected set; }


    public void ColorTile()
    {
        tilePawnIsOn.GetComponent<SpriteRenderer>().sprite = PawnSprite;
        tilePawnIsOn.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 1);
        ShowItem();
    }

    public abstract Command GetCommand();


    public abstract void GetTargeter(Character character);


    public void ShowItem()
    {
        tilePawnIsOn.FoodImageRenderer.sprite = ItemSprite;
    }

    public void HideItem()
    {
        tilePawnIsOn.FoodImageRenderer.sprite = null;
    }

     void ChangeTileWeight()
    {
        tilePawnIsOn.movementPenalty = 255;
    }

}
