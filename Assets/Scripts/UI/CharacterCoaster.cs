using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCoaster : MonoBehaviour
{
    public delegate void OnStopMoving(Tile tile);
    public OnStopMoving onStopMoving;
    float speed = 0.005f;
    Tile[] _path;


    Vector3 desiredLocation;
    // Start is called before the first frame update
    void Start()
    {

    }

    public Sprite CharacterSprite
    {
        get { return this.GetComponent<SpriteRenderer>().sprite; }
        set
        {
            this.GetComponent<SpriteRenderer>().sprite = value;
            this.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 1);
        }
    }

    public void MoveAlongPath(Tile[] path, bool isPathFound)
    {
        if (isPathFound)
        {
            _path = path;
           StartCoroutine("FollowPath");
        }
    }


    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = new Vector3(_path[0].transform.position.x, _path[0].transform.position.y, -0.5f);
        int TargetIndex = 0;
        while (true)
        {

            if (transform.position == currentWaypoint)
            {
                TargetIndex++;
                if (TargetIndex == _path.Length)
                {
                    onStopMoving.Invoke(_path[TargetIndex-1]);
                    yield break;
                }
                currentWaypoint = new Vector3(_path[TargetIndex].transform.position.x, _path[TargetIndex].transform.position.y, -0.5f);
            }

            transform.position = Vector3.MoveTowards(this.transform.position, currentWaypoint, speed);
            yield return null;
        }
    }


}
