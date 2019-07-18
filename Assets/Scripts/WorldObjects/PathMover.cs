using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// responsible for moving a pawn across its path.

public class PathMover : MonoBehaviour
{
    //CharacterCoaster _characterCoaster;
    //float speed = 5;
    //Tile[] _path;
    //int TargetIndex;

    //Vector3 desiredLocation;


    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    ////TODO, try hooking this up to map, and to path finder.
    //public void MoveAlongPath(Tile[] path, bool isPathFound)
    //{

    //    if (isPathFound)
    //    {
    //        _path = path;
    //        StopCoroutine("FollowPath");
    //        StartCoroutine("FollowPath");
    //    }
    //}

    
    //public  void SetCharacterCoaster ( CharacterCoaster characterCoaster)
    //{
    //    _characterCoaster = characterCoaster;
    //}


    //IEnumerator FollowPath()
    //{
    //    Vector3 currentWaypoint = _path[0].transform.position;

    //    while(true)
    //    {
    //        if(transform.position == currentWaypoint)
    //        {
    //            TargetIndex++;
    //            if(TargetIndex >= _path.Length)
    //            {
    //                yield break;
    //            }
    //            currentWaypoint = _path[TargetIndex].transform.position;
    //        }

    //        _characterCoaster.transform.position = Vector3.MoveTowards(_characterCoaster.transform.position, currentWaypoint, speed);
    //    }
    //}
}
