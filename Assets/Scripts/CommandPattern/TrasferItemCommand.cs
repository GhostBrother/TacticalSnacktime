using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TrasferItemCommand : Command
{
    iCanGiveItems _giver;
    Character _reciver;
    protected string characterName;
    protected string itemName;

    public TrasferItemCommand(iCanGiveItems giver, Character reciver)
    {
        _giver = giver;
        _reciver = reciver;
    }

    public abstract string CommandName { get; }

    public void execute()
    {
        _reciver.PickUp(_giver.Give());
        _giver.GetRidOfItem();
    }
}
