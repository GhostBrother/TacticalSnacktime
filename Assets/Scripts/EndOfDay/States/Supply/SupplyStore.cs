using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyStore : MonoBehaviour
{

    // Knowlage of food stats
    FoodLoader foodLoader;

    // List of foods currently for sale
    List<Food> foodForSale;

    // Vend slots for Shop
    [SerializeField]
    List<ItemInStore> itemsInStore;

    public void Init()
    {
        foreach(ItemInStore item in itemsInStore)
        {
            item.Init();
        }
    }

    // Load shop 
    public void LoadShop()
    {
        if (!foodLoader) { foodLoader = new FoodLoader(); }
        for(int i = 0; i < itemsInStore.Count; i++)
        {
            itemsInStore[i].SetFood(foodLoader.GetRandomFood());
        }
    }
}
