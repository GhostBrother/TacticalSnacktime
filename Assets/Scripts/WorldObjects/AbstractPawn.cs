using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPawn : MonoBehaviour, iPawn
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
        }
    }

    public Sprite PawnSprite { get; protected set; }

    public Sprite ItemSprite { get; protected set; }

    //protected Sprite foodSprite { get { return tilePawnIsOn.FoodImageRenderer.sprite; } set { tilePawnIsOn.FoodImageRenderer.sprite = value; } }

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

     void ChangeTileWeight()
    {
        tilePawnIsOn.movementPenalty = 255;
    }

}
