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
