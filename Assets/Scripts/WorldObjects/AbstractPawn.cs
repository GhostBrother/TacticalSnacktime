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

    public void ColorTile()
    {
        tilePawnIsOn.GetComponent<SpriteRenderer>().sprite = PawnSprite;
        tilePawnIsOn.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 1);
    }

     void ChangeTileWeight()
    {
        tilePawnIsOn.movementPenalty = 255;
    }

}
