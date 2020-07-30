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

    Map _map;

    public void InitState(Money money, Map _Map)
    {
        fl = new FoodLoader();
        _map = _Map;
        moneyOnHand = money;
        loadShopItemsForDay();
    }

    public void DisplayProps()
    {
        foreach (ItemInStore items in itemsInStore)
        {
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
        for(int i = 0; i < itemsInStore.Count; i++)//each (ItemInStore items in itemsInStore)
        {
            if (itemsInStore[i].ammountOwned > 0)
            {
                Supply s = fl.GetFoodAsSupply(itemsInStore[i].FoodName, itemsInStore[i].ammountOwned);
                s.characterCoaster = _monoPool.GetCharacterCoasterInstance();
                s._monoPool = _monoPool;
                s.TilePawnIsOn = _map.GetTileAtRowAndColumn(4, i);
            }
        }
        loadShopItemsForDay();
    }

    decimal getMoneyAmount()
    {
        return moneyOnHand.valueToStore;
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
            items.ChangeMoneyBalance = ChangeMoneyBalance;
            items.GetTotalMoney = getMoneyAmount;
            items.LoadFood(fl.RandomOfType("Supply"));
            
        }
    }

}
