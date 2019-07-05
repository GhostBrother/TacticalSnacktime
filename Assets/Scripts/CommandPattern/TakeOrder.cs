using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeOrder : Command
{
    Character employee;
    AICharacter customer;
    public TakeOrder(Character _employee, AICharacter _customer)
    {
        employee = _employee;
       customer = _customer;
    }

    public string CommandName { get { return "Take Order"; } }

    public void execute()
    {
       customer.DisplayOrder();
    }
}
