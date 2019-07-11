using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iPathfinding 
{
    void setTarget(Tile _target);
    void CheckPath();
    void Move();
    void OnPathFound(Tile[] newPath, bool pathSuccessful);
    void FollowPath();

}
