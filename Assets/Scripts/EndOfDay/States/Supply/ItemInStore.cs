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

    public decimal RunningTotal { get; private set; }

    //Running Total
    [SerializeField]
    Text RunningTotalText;


    // Textbox for quantity purchase
    [SerializeField]
    InputField inputField;


    //Increment
    [SerializeField]
    ActionButton IncrementTotal;

    // Decrement
    [SerializeField]
    ActionButton DecrementTotal;

    // Quantity owned 
    [SerializeField]
    Text QuantityOwned;

    public int curentQuantity { get; private set; }

    // Picture.
    [SerializeField]
    Image PictureOfProduct;

    // Name
    [SerializeField]
    Text NameOfFoodText;

    public string NameOfFood { get { return NameOfFoodText.text; } }

    public event Func<decimal> CheckTotal;

    public event Action ReciptTotal;


    public void SetFood(Food foodToShow)
    {
        CostText.text = foodToShow.Price.ToString("$ 0.00");
        _Cost = foodToShow.Price; 
        PictureOfProduct.sprite = foodToShow.CaryableObjectSprite;
        NameOfFoodText.text = foodToShow.Name;

        inputField.text = "0";

        IncrementTotal.StoredCommand = new IncrementValue<decimal>(inputField); 
        DecrementTotal.StoredCommand = new DecrementValue<decimal>(inputField); 

        inputField.characterValidation = InputField.CharacterValidation.Integer;
        inputField.onValueChanged.AddListener(CheckIfCanAfford);
        RunningTotalText.text = "$ 0.00";

        curentQuantity = 0;
        
    }

    void UpdateReceipt(string numberIn)
    {
      inputField.text = numberIn.ToString();
   
      RunningTotalText.text = RunningTotal.ToString("$ 0.00");

      ReciptTotal.Invoke();     
    }

    void CheckIfCanAfford(string numberIn)
    {
        int numberOfGood;

        if (!int.TryParse(numberIn, out numberOfGood) || numberOfGood <= 0)
        {
            numberOfGood = 0;
        }

        decimal placeHolder = CheckTotal.Invoke();
        placeHolder += (curentQuantity - numberOfGood) * _Cost;
       
        if (placeHolder < 0)
        {
            numberOfGood = curentQuantity;
        }

        RunningTotal = (_Cost * numberOfGood);
        curentQuantity = numberOfGood;
        UpdateReceipt(numberOfGood.ToString());
 
    }
}
