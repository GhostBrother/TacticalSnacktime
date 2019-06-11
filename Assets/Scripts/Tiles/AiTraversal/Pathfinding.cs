using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pathfinding : MonoBehaviour
{
    // Has to be the number of tiles on the map... 
    [SerializeField]
     int maxSize;
    PathRequestManager requestManager;

    private void Awake()
    {
        requestManager = GetComponent<PathRequestManager>();
    }

    public Pathfinding(int _maxSize)
    {
        maxSize = _maxSize;
    }

     void FindPath(Tile startPos, Tile targetPos) 
    {

        Tile[] waypoints = new Tile[0];
        bool pathSuccess = false;
        Heap<Tile> openSet = new Heap<Tile>(maxSize);
        HashSet<Tile> closeSet = new HashSet<Tile>();

        openSet.Add(startPos);
        while (openSet.Count > 0)
        {
            Tile currentTile = openSet.RemoveHead();
            closeSet.Add(currentTile);

            if (currentTile == targetPos)
            {
                pathSuccess = true;
                break; 
             }

            foreach (Tile neighbor in currentTile.neighbors)
            {
                if(!closeSet.Contains(neighbor))
                {
                    int newMovementCostToNeighbor = currentTile.gCost + GetDistance(currentTile, neighbor);
                    if (newMovementCostToNeighbor < neighbor.gCost || openSet.Contains(neighbor))
                        neighbor.gCost = newMovementCostToNeighbor;
                        neighbor.hCost = GetDistance(neighbor, targetPos);

                    neighbor.Parent = currentTile;

                if (!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                }
            }
        }

        //yield return null;
        if (pathSuccess)
        {
            waypoints = RetracePath(startPos, targetPos);

        }
        requestManager.FinishedProcessingPath(waypoints,pathSuccess);
    }

    Tile[] RetracePath(Tile startTile, Tile endTile)
    {
        List<Tile> path = new List<Tile>();

        Tile currentTile = endTile;

        while (currentTile != startTile)
        {
            path.Add(currentTile);
            // temp
            currentTile.gameObject.GetComponent<SpriteRenderer>().color = Color.black;
            currentTile = currentTile.Parent;
        }

        Tile[] waypoints = path.ToArray();
        Array.Reverse(waypoints);
        
        return waypoints;
    }

    int GetDistance(Tile tileA, Tile tileB)
    {
        int disX = Mathf.Abs(tileA.GridX - tileB.GridX);
        int disY = Mathf.Abs(tileA.GridY - tileB.GridY);

        if (disX > disY)
            return 14 * disY + 10 *(disX - disY); 

        else
            return 14 * disX + 10 * (disY - disX);
    }

    public void StartFindPath(Tile startTile, Tile targetPos)
    {
        //StartCoroutine(FindPath(startTile,targetPos));
        FindPath(startTile, targetPos);
    }
}
