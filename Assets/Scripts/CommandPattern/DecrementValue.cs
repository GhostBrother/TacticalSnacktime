using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecrementValue : Command
{
    ItemInStore itemInStore;
    public iCommandKind typeOfCommand { get; set; }

    public bool isUsable => true;

    public string CommandName { get { return string.Empty; } }

    public DecrementValue(ItemInStore _itemInStore)
    {
        itemInStore = _itemInStore;
        typeOfCommand = new CloseMenu();
    }

    public void execute()
    {
        itemInStore.DecrementCount();
    }
}
