using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeOrder : Command
{
    Character playerControlledCharacter;
    AICharacter customer;
    public TakeOrder( Character _playercontrolledCharacter ,AICharacter _customer)
    {
        playerControlledCharacter = _playercontrolledCharacter;
       customer = _customer;
    }

    public string CommandName { get { return "Take Order"; } }

    public bool isUsable => true;

    public iCommandKind typeOfCommand { get; set; }

    public void execute()
    {
       playerControlledCharacter.characterCoaster.SetArtForFacing(playerControlledCharacter.characterCoaster.determineFacing(playerControlledCharacter.TilePawnIsOn, customer.TilePawnIsOn));
       customer.OrderHasBeenTaken = true;
       customer.DisplayOrder();
    }


}
