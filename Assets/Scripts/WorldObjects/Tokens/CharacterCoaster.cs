using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCoaster : MonoBehaviour
{
    public Action<Tile> OnStopMoving;
    float speed = 0.005f;
    Tile[] _path;
    const float ZCordinate = -1.5f;

    //A list of all the facings and statusus of a character sprite.
    public List<Sprite> facingSprites { get; set; }

    
    Vector3 desiredLocation;


    public Sprite CharacterSprite
    {
        get { return this.GetComponent<SpriteRenderer>().sprite; }
        set
        {
            this.GetComponent<SpriteRenderer>().sprite = value;
            this.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 1);
        }
    }


    public void SetArtForFacing(EnumHolder.Facing facing)
    {
        CharacterSprite = facingSprites[(int)facing];
    }


    public void MoveAlongPath(Tile[] path, bool isPathFound)
    {

        if (isPathFound)
        {
            _path = path;
           StartCoroutine("FollowPath");
        }
    }

    public void PlaceCoasterOnTile(Tile tile)
    {
        transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y, ZCordinate);
    }

    IEnumerator FollowPath()
    {

        Vector3 currentWaypoint = new Vector3(_path[0].transform.position.x, _path[0].transform.position.y, ZCordinate);
        int TargetIndex = 0;
        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                TargetIndex++;
                if (TargetIndex == _path.Length)
                {
                    OnStopMoving.Invoke(_path[TargetIndex-1]);
                    yield break;
                }
                currentWaypoint = new Vector3(_path[TargetIndex].transform.position.x, _path[TargetIndex].transform.position.y, ZCordinate);

               SetArtForFacing(determineFacing(_path[TargetIndex-1], _path[TargetIndex]));
            }

            transform.position = Vector3.MoveTowards(this.transform.position, currentWaypoint, speed);
            yield return null;
        }
    }


    EnumHolder.Facing determineFacing(Tile previousWaypoint, Tile nextWaypoint)
    {

       if(previousWaypoint.GridX > nextWaypoint.GridX)
        {
            return EnumHolder.Facing.Left;
        }

       if(previousWaypoint.GridX < nextWaypoint.GridX)
        {
            return EnumHolder.Facing.Right;
        }

        if (previousWaypoint.GridY > nextWaypoint.GridY)
        {
            return EnumHolder.Facing.Up;
        }

        else
            return EnumHolder.Facing.Down;

    }


}
