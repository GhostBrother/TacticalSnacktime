using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeOrder : Command
{
    AICharacter customer;
    public TakeOrder( AICharacter _customer)
    {
       customer = _customer;
    }

    public string CommandName { get { return "Take Order"; } }

    public bool isUsable => true;

    public iCommandKind typeOfCommand { get; set; }

    public void execute()
    {
       customer.OrderHasBeenTaken = true;
       customer.DisplayOrder();
    }


}
