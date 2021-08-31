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

    //Running Total
    [SerializeField]
    Text RunningTotalText;


    // Textbox for quantity purchase
    [SerializeField]
    InputField QuantityOfPurchase;


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
    Text NameOfFoodText;

    public string FoodName { get { return NameOfFoodText.text; }}

    public int ammountOwned { get; private set; }

    decimal _foodCost;

    public Func<decimal> GetTotalMoney;

    public Action<decimal> ChangeMoneyBalance;


    public void LoadFood(Food food)
    {
        _foodCost = food.Price;
        CostText.text = _foodCost.ToString();
        PictureOfProduct.sprite = food.CaryableObjectSprite;
        NameOfFoodText.text = food.Name;
        ammountOwned = 0;

        
        IncrementTotal.StoredCommand = new IncrementValue<decimal>(QuantityOfPurchase);
        DecrementTotal.StoredCommand = new DecrementValue<decimal>(QuantityOfPurchase);
        QuantityOfPurchase.contentType = InputField.ContentType.IntegerNumber;
        QuantityOfPurchase.onValueChanged.AddListener(CheckChange);
        QuantityOfPurchase.text = ammountOwned.ToString();
    }

    void CheckChange(string s)
    {
        int quantityToBuy = 0;

        decimal change = 0;

        int.TryParse(s, out quantityToBuy);

        if (quantityToBuy < 0)
        {
            quantityToBuy = 0;
            QuantityOfPurchase.text = ammountOwned.ToString();
        }

        change = -(quantityToBuy - ammountOwned) * _foodCost;


        ammountOwned = quantityToBuy;
        ChangeMoneyBalance.Invoke(change);
        RunningTotalText.text = (quantityToBuy * _foodCost).ToString();
    }



}
