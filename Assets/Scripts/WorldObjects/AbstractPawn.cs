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


    private Tile tilePawnIsOn;
    public virtual Tile TilePawnIsOn
    {
        get { return tilePawnIsOn; }
        set
        {
            tilePawnIsOn = value;
            tilePawnIsOn.ChangeState(tilePawnIsOn.GetActiveState());
            //New
            // characterCoaster
            //ColorTile();
            ChangeTileWeight();

            tilePawnIsOn.EntityTypeOnTile = EntityType;
            _characterCoaster.CoasterLocation = TilePawnIsOn.transform.position;

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
