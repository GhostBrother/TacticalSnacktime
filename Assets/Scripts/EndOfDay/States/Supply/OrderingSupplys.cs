﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderingSupplys : MonoBehaviour, iEndOfDayState
{

    // Knowlage of food stats
    FoodLoader foodLoader;

    // List of foods currently for sale
    List<Food> foodForSale;

    public EndOfDayPannel EndOfDayPannel { private get; set; }
    public ActionButton ButtonForState { private get;  set; }

    // Vend slots for Shop
    [SerializeField]
    List<ItemInStore> itemsInStore;


    public void Init(Money money)
    {
        foreach (ItemInStore item in itemsInStore)
        {
            item._moneyRefrence = money; 
        }
    }

    public void DisplayProps()
    {
        this.gameObject.SetActive(true);
        for (int i = 0; i < itemsInStore.Count; i++)
        {
            itemsInStore[i].gameObject.SetActive(true);
        }
    }

    public void HideProps()
    {
        this.gameObject.SetActive(false);
        for(int i = 0; i < itemsInStore.Count; i++)
        {
            itemsInStore[i].gameObject.SetActive(false);
        }
    }

    public void LoadStoreItems()
    {
        if (!foodLoader) { foodLoader = new FoodLoader(); }
        for (int i = 0; i < itemsInStore.Count; i++)
        {
            itemsInStore[i].SetFood(foodLoader.GetRandomFood());
        }
    }


}
