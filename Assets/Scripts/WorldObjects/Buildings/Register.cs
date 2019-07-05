using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Register : AbstractPawn
{
    Character employee;
    AICharacter customer;
    private Command CashRegisterCommand;
    public Register()
    {
        PawnSprite = SpriteHolder.instance.GetBuildingArtFromIDNumber(2);
        EntityType = EnumHolder.EntityType.Register;
    }
    public override Command GetCommand()
    {
        return CashRegisterCommand;
    }

    public override void GetTargeter(Character _character)
    {
        CashRegisterCommand = null;
        employee = _character;

        for(int i = 0; i < TilePawnIsOn.neighbors.Count; i++)
        {
            if (TilePawnIsOn.neighbors[i].TargetableOnTile is AICharacter)
            {
                customer = (AICharacter)TilePawnIsOn.neighbors[i].TargetableOnTile;
                CashRegisterCommand = new TakeOrder(employee,customer);
            }
        }
       
        //if(_character is AICharacter)
        //{
        //    character = (AICharacter)_character;
        //    CashRegisterCommand = new TakeOrder(character);
        //}
    }
}
