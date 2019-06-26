using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveItem : Command
{
    Character _giver;
    Character _reciver;

    public GiveItem( Character giver, Character reciver)
    {
        _giver = giver;
        _reciver = reciver;
    }

    public string CommandName { get { return "GiveFood"; } }

    public void execute()
    {
        _reciver.PickUp(_giver.Give());
        _giver.GetRidOfItem();
    }

}
