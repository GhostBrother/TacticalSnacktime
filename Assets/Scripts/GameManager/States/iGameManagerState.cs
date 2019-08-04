using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iGameManagerState
{
    void TileClicked(Tile tile);
    void RightClick(Tile tile);
    void NextArrow();
    void PrevArrow();
}
