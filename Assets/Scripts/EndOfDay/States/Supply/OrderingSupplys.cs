using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OrderingSupplys : MonoBehaviour, iEndOfDayState
{

    // Vend slots for Shop
    [SerializeField]
    List<ItemInStore> itemsInStore;

    [SerializeField]
    MonoPool _monoPool;

    public ActionButton ButtonForState { get; set; }

    FoodLoader fl;

    Money moneyOnHand;

    public void InitState(Money money)
    {
        fl = new FoodLoader();
        moneyOnHand = money;
        loadShopItemsForDay();
    }

    public void DisplayProps()
    {
        
        foreach (ItemInStore items in itemsInStore)
        {
            items.ChangeMoneyBalance = ChangeMoneyBalance;
            items.gameObject.SetActive(true);
        }
    }

    public void HideProps()
    {
        foreach (ItemInStore items in itemsInStore)
        {
            items.gameObject.SetActive(false);
        }
    }

    public void OnStartNextDay()
    {
        loadShopItemsForDay();
    }

    void ChangeMoneyBalance(decimal moneyBalance)
    {
        moneyOnHand.valueToStore += moneyBalance;
        moneyOnHand.updateTextRefrence();
    }

    void loadShopItemsForDay()
    {
        foreach (ItemInStore items in itemsInStore)
        { 
           items.LoadFood(fl.RandomOfType("Supply"));
        }
    }

}
