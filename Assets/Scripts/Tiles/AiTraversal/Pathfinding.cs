using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pathfinding
{

    public Tile start, end;
    private int maxSize;

    public Pathfinding(int _maxSize)
    {
        maxSize = _maxSize;
    }

     public void FindPath(Tile startPos, Tile targetPos) // was private
    {
        Heap<Tile> openSet = new Heap<Tile>(maxSize);
        HashSet<Tile> closeSet = new HashSet<Tile>();

        openSet.Add(startPos);
        while (openSet.Count > 0)
        {
            Tile currentTile = openSet.RemoveHead();
            closeSet.Add(currentTile);

            if (currentTile == targetPos)
            {
                RetracePath(startPos, targetPos);
                return;
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
    }

    void RetracePath(Tile startTile, Tile endTile)
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
        path.Reverse();
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
}
