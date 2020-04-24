
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncrementValue : Command
{
    ItemInStore itemInStore;
    public iCommandKind typeOfCommand { get; set; }

    public bool isUsable => true;

    public string CommandName { get { return string.Empty; }  }

    public IncrementValue(ItemInStore _itemInStore)
    {
        itemInStore = _itemInStore;
        typeOfCommand = new CloseMenu();
    }


    public void execute()
    {
        itemInStore.IncrementCount();
    }
}
