using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OrderingSupplys : MonoBehaviour, iEndOfDayState
{

    // Knowlage of food stats
    FoodLoader foodLoader;

    // List of foods currently for sale
    List<Food> foodForSale;

    public ActionButton ButtonForState { private get;  set; }

    Map _map;

    // Vend slots for Shop
    [SerializeField]
    List<ItemInStore> itemsInStore;

    [SerializeField]
    MonoPool _monoPool;

    Money _money;

    decimal _totalForThisPage;


    public void Init(Money money, Map map)
    {
        _money = money;
        _map = map;
        foreach (ItemInStore item in itemsInStore)
        {
            item.CheckTotal = totalAllItems;
            item.ReciptTotal = UpdateReciptTotal;
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
            itemsInStore[i].SetFood(foodLoader.RandomOfType("Supply"));
        }
    }

    decimal totalAllItems()
    {
        decimal total = 0;
        foreach (ItemInStore item in itemsInStore)
        {
            total += item.RunningTotal;
        }
        _money.valueToStore -= (total - _totalForThisPage);
        return total;
    }

    void UpdateReciptTotal() 
    {
        _totalForThisPage = totalAllItems();
        _money.updateTextRefrence();
    }

    public void OnStartNextDay()
    {
        for (int i = 0; i < itemsInStore.Count; i++)
        {
            if (itemsInStore[i].curentQuantity != 0)
            {
                Supply sup = foodLoader.GetFoodAsSupply(itemsInStore[i].NameOfFood, itemsInStore[i].curentQuantity);
                sup.characterCoaster = _monoPool.GetCharacterCoasterInstance();
                sup._monoPool = _monoPool;
                sup.TilePawnIsOn = _map.GetTileAtRowAndColumn(4, i);
            }
        }
    }
}
