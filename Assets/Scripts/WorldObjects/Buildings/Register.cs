using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Register : AbstractPawn , iTargetable
{
    AICharacter customer;
    private List<Command> cashRegisterCommands;
    public Register()
    {
        PawnSprite = SpriteHolder.instance.GetBuildingArtFromIDNumber(2);
        EntityType = EnumHolder.EntityType.Register;
        cashRegisterCommands = new List<Command>();
    }
    public List<Command> GetCommands()
    {
        return cashRegisterCommands;
    }

    public void GetTargeter(Character _character)
    {
        cashRegisterCommands.Clear();

        for(int i = 0; i < TilePawnIsOn.neighbors.Count; i++)
        {
            if (TilePawnIsOn.neighbors[i].TargetableOnTile is AICharacter)
            {
                customer = (AICharacter)TilePawnIsOn.neighbors[i].TargetableOnTile;
                cashRegisterCommands.Add(new TakeOrder(customer));
            }
        }
      
    }
}
