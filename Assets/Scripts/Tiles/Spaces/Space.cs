using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Space : MonoBehaviour
{

    public abstract void SelectTile();

    public abstract void ColorAllAdjacent(int numToHilight);

}
