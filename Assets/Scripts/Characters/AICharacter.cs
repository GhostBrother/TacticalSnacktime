using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacter : Character
{

    private Tile desiredTile;
    public AICharacter(int baseMoveSpeed, Sprite characterSprite, int speedStat) : base(baseMoveSpeed, characterSprite, speedStat)
    {
    }

    public void SetDesiredTile(Tile newDesiredTile)
    {
        desiredTile = newDesiredTile;
    }


}
