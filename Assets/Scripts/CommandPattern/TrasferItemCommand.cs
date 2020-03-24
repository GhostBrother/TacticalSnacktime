using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TrasferItemCommand : Command
{
    public abstract bool isUsable { get; }
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
        itemName = _giver.Give(index).Name; //Change name of Method to "GetItemFromIndex" for clarity
        //typeOfCommand = new CloseMenu();
    }

    public abstract string CommandName { get; }
    public iCommandKind typeOfCommand {  set;  get; }

    public void execute()
    {
        iCaryable swapedItem = _giver.Give(_index);
        _giver.GetRidOfItem(_index);
        _reciver.PickUp(swapedItem);
    }
}
