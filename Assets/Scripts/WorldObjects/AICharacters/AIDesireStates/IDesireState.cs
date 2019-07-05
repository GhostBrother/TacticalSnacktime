using System.Collections;
using System.Collections.Generic;


public interface IDesireState 
{
    bool isRequestSatisfied();
    void MoveTarget();
}
