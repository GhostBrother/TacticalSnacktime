using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractActor : AbstractPawn , iPathfinding
{
    public void CheckPath()
    {
        throw new System.NotImplementedException();
    }

    public void FollowPath()
    {
        throw new System.NotImplementedException();
    }

    public  Command GetCommand()
    {
        throw new System.NotImplementedException();
    }

    public  void GetTargeter(Character character)
    {
        throw new System.NotImplementedException();
    }

    public void Move()
    {
        throw new System.NotImplementedException();
    }

    public void OnPathFound(Tile[] newPath, bool pathSuccessful)
    {
        throw new System.NotImplementedException();
    }

    public void setTarget(Tile _target)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
