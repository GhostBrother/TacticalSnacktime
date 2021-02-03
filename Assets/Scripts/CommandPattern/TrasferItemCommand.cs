using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TrasferItemCommand : Command
{
    public abstract bool isUsable { get; set; }
    protected iCanGiveItems _giver;
    protected Character Reciver;
    protected string characterName;
    protected string itemName;
    int _index;

    public TrasferItemCommand( int index)
    {
        _index = index;
    }

    public abstract string CommandName { get; }
    public abstract iCommandKind typeOfCommand {  set;  get; }
   

    public void execute()
    {
        typeOfCommand.ActivateType(); 
    }

    protected virtual void OrganizeTrade(Tile tileToTradeWith)
    {
        iCaryable swapedItem = _giver.Give(_index);
        _giver.GetRidOfItem(_index);
        Reciver.PickUp(swapedItem);
        typeOfCommand.UndoType();
        Reciver.characterCoaster.SetArtForFacing(Reciver.characterCoaster.determineFacing(Reciver.TilePawnIsOn, tileToTradeWith));
    }
}
