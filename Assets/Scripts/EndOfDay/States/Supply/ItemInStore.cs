using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInStore : MonoBehaviour
{

    // Cost
    [SerializeField]
    Text CostText;

    decimal _Cost;

    //Running Total
    [SerializeField]
    Text RunningTotalText;

    decimal _RunningTotal;

    // Textbox for quantity purchase


    //Increment
    [SerializeField]
    ActionButton IncrementTotal;

    // Decrement
    [SerializeField]
    ActionButton DecrementTotal;

    // Quantity owned 
    [SerializeField]
    Text QuantityOwned;

    // Picture.
    [SerializeField]
    Image PictureOfProduct;

    // Name
    [SerializeField]
    Text NameOfFood;

    public Money _moneyRefrence { get; set; }


    public void SetFood(Food foodToShow)
    {
        CostText.text = foodToShow.Price.ToString("$ 0.00");
        _Cost = foodToShow.Price; 
        PictureOfProduct.sprite = foodToShow.CaryableObjectSprite;
        NameOfFood.text = foodToShow.Name;
        _RunningTotal = 0;

        IncrementTotal.StoredCommand = new IncrementValue<decimal>(_moneyRefrence, _Cost);
        DecrementTotal.StoredCommand = new DecrementValue<decimal>(_moneyRefrence, _Cost);
    }
}
