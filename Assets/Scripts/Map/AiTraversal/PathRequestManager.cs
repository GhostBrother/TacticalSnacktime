using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathRequestManager : MonoBehaviour
{

    Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
    PathRequest currentPathRequest;

     static PathRequestManager instance;
    Pathfinding pathfinding;

    bool isProcessingPath;

    void Awake()
    {
        instance = this;
        pathfinding = GetComponent<Pathfinding>();
    }

    // here
    public static void RequestPath(Tile StartTile, Tile TargetTile, Action<Tile[], bool> callback)
    {
        PathRequest newRequest = new PathRequest(StartTile, TargetTile, callback);
        instance.pathRequestQueue.Enqueue(newRequest);
        instance.TryProcessNext();
    }

    void TryProcessNext()
    {
         if(!isProcessingPath && pathRequestQueue.Count > 0)
        {
            currentPathRequest = pathRequestQueue.Dequeue();
            isProcessingPath = true;
            pathfinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
        }
    }

    public void FinishedProcessingPath(Tile[] path, bool success)
    {
        currentPathRequest.callback(path, success);
        isProcessingPath = false;
        TryProcessNext();
    }

    struct PathRequest
    {
        public Tile pathStart;
        public Tile pathEnd;
        public Action<Tile[], bool> callback;

        public PathRequest(Tile _pathStart ,Tile _pathEnd,Action<Tile[],bool> _callback)
        {
            pathStart = _pathStart;
            pathEnd = _pathEnd;
            callback = _callback;
        }
    }

    public static Tile FindClosestEntityOfType(Tile StartTile, EnumHolder.EntityType entityToFind)
    {
       return instance.pathfinding.FindClosestTileOfType(StartTile, entityToFind);
    }

}
