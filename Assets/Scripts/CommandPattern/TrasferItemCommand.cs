using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TrasferItemCommand : Command
{
    iCanGiveItems _giver;
    Character _reciver;
    protected string characterName;
    protected string itemName;
    int _index;

    public TrasferItemCommand(iCanGiveItems giver, Character reciver, int index)
    {
        _giver = giver;
        _reciver = reciver;
        _index = index;
    }

    public abstract string CommandName { get; }

    public void execute()
    {
        _reciver.PickUp(_giver.Give(_index));
        _giver.GetRidOfItem(_index);
    }
}
