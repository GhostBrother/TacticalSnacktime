using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelector : MonoBehaviour {

	public void MoveToPosition(Vector3 newPos)
    {
        this.transform.position = new Vector3(newPos.x, newPos.y, -1);
    }
}
