using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour, iHeapItem<Tile> {


    public List<Tile> neighbors { get; }

    // Feasibly abstract this out to like an iNavigatable or something
    public int gCost { get; set; }
    public int hCost { get; set; }
    public int fCost { get { return hCost + gCost; } }
    public int GridX { get; private set; }
    public int GridY { get; private set; }
    [SerializeField]
    private int _movementPenalty;
    public int movementPenalty { get { return _movementPenalty; } set { _movementPenalty = value; } }
    int heapIndex;
    public int HeapIndex { get { return heapIndex; } set { heapIndex = value; } }

    public Tile Parent { get; set; }

    [SerializeField]
    SpriteRenderer backgroundTile;
    public SpriteRenderer BackgroundTile { get { return backgroundTile; } private set {; } }

    [SerializeField]
    SpriteRenderer foodImageRenderer;
    public SpriteRenderer FoodImageRenderer { get { return foodImageRenderer; } private set{ foodImageRenderer = value; } }

    [SerializeField]
    Color activeColor;
    public Color ActiveColor { get { return activeColor; } private set {; } }

    [SerializeField]
    Color deactiveColor;
    public Color DeactiveColor { get { return deactiveColor; } private set {; } }

    [SerializeField]
    Color deployZoneColor;
    public Color DeployZoneColor { get { return deployZoneColor; } private set {; } }

    private iTargetable targetableOnTile; 
    public iTargetable TargetableOnTile { get { return targetableOnTile; } set { targetableOnTile = value; } }

    public bool IsTargetableOnTile { get { return targetableOnTile != null; }  }

    private iTileState clear;
    private iTileState hilighted;
    private iTileState occupiedSquare;
    private iTileState deployZoneTile;
    private iTileState curentState;

    [SerializeField]
    string DEBUGSTATE;


    public Tile()
    {
        neighbors = new List<Tile>();
        clear = new ClearTile(this);
        hilighted = new HilightedTile(this);
        occupiedSquare = new OccupiedTile(this);
        deployZoneTile = new DeployZoneTile(this);
        movementPenalty = 0;
        curentState = clear;
    }

    public void SetXandYPos(int gridX, int gridY)
    {
        GridX = gridX;
        GridY = gridY;
    }

    public void ChangeState(iTileState newState)
    {
        curentState = newState;
        curentState.ChangeColor();
        DEBUGSTATE = curentState.ToString();
    }

    public iTileState GetClearState()
    {
        // HACK invest in a moved off function for pawns.
       TargetableOnTile = null;
        return clear;
    }

    public iTileState GetActiveState()
    {
        return occupiedSquare;
    }

    public iTileState GetHilightedState()
    {
        return hilighted;
    }

    public iTileState GetDeployState()
    {
        return deployZoneTile;
    }

    public iTileState GetCurrentState()
    {
        return curentState;
    }

    public void DeactivateTile()
    {
        if (GetCurrentState() != GetActiveState())
        {
            movementPenalty = 0;
            ChangeState(clear);
        }
    }

    public void SetNeighbor(Tile newNeighbor)
    {
        neighbors.Add(newNeighbor);
    }

    public void SelectTile()
    {
        curentState.TileClicked();
    }

    public void ColorAllAdjacent(int numToHilight)
    {
        if (numToHilight >= 0)
        {
            numToHilight--;

            if (GetCurrentState() != GetActiveState()) 
                ChangeState(hilighted);

            for (int i = 0; i < neighbors.Count; i++)
            {
                
                if (neighbors[i].GetCurrentState() != neighbors[i].GetActiveState())
                {
                    neighbors[i].ColorAllAdjacent(numToHilight);
                }

            }
        }
    }

    public int CompareTo(Tile TileToCompare)
    {
        int compare = fCost.CompareTo(TileToCompare.fCost);
        if(compare == 0)
            compare = hCost.CompareTo(TileToCompare.hCost);

        return -compare;
         
    }
}
