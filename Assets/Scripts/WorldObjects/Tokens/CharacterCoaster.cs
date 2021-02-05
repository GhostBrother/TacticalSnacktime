using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CharacterCoaster : MonoBehaviour
{
    public Action OnStartMoving;
    public Action<Tile> OnStopMoving;
    float speed = 0.005f;
    Tile[] _path;
    const float ZCordinate = -1.5f;
    [SerializeField]
    Animator animator;

    public RuntimeAnimatorController curAnimation { get; set; }


    [SerializeField]
    public SpriteRenderer curFacingArt;
    public List<Sprite> facingSprites { get; set; }

    public float SpriteBoundX {get { return curFacingArt.sprite.bounds.extents.x; }}
    public float SpriteBoundY { get { return curFacingArt.sprite.bounds.extents.y;}}


    private void Start()
    {
        animator = this.GetComponent<Animator>();  
    }


    public Sprite CharacterSprite
    {
        get { return this.GetComponent<SpriteRenderer>().sprite; }
        set
        {
            curFacingArt.sprite = value;
            curFacingArt.color = new Vector4(1, 1, 1, 1);
        }
    }

     void stopWalkAnimation()
    {
        animator.enabled = false;
        animator.SetInteger("Facing", -1);
        animator.runtimeAnimatorController = null;
    }


     void SetAnimationForWalking(EnumHolder.Facing facing)
    {
        animator.enabled = true;
        animator.runtimeAnimatorController = curAnimation;
        animator.SetInteger("Facing",(int)facing);
    }

    public void SetArtForFacing(EnumHolder.Facing facing)
    {
        CharacterSprite = facingSprites[(int)facing];      
    }


    public void MoveAlongPath(Tile[] path, bool isPathFound)
    {

         if (isPathFound )
        {
            _path = path;
            StartCoroutine("FollowPath");
            OnStartMoving.Invoke();
        }
    }

    public void PlaceCoasterOnTile(Tile tile)
    {
        transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y, ZCordinate);
    }

    IEnumerator FollowPath()
    {  
        
        Vector3 currentWaypoint = new Vector3(_path[0].transform.position.x, _path[0].transform.position.y, ZCordinate);
        EnumHolder.Facing _facing = EnumHolder.Facing.Down;
        int TargetIndex = 0;

        while (true)
        {
            
            if (transform.position == currentWaypoint)
            {

                _path[TargetIndex].BackgroundTile.color = _path[TargetIndex].DeactiveColor; // Burn after reading
                 TargetIndex++;
                
                if (TargetIndex == _path.Length)
                {
                    stopWalkAnimation();
                    SetArtForFacing(_facing);
                    OnStopMoving.Invoke(_path[TargetIndex - 1]); 
                    yield break;
                }
                currentWaypoint = new Vector3(_path[TargetIndex].transform.position.x, _path[TargetIndex].transform.position.y, ZCordinate);

                _facing = determineFacing(_path[TargetIndex - 1], _path[TargetIndex]);
                SetAnimationForWalking(_facing);
                
            }
            transform.position = Vector3.MoveTowards(this.transform.position, currentWaypoint, speed);
            yield return null;
        }

    }


   public EnumHolder.Facing determineFacing(Tile previousWaypoint, Tile nextWaypoint)
    {
        if (previousWaypoint.GridX > nextWaypoint.GridX)
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
