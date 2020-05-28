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

    // Picture.
    [SerializeField]
    Image PictureOfProduct;

    // Name
    [SerializeField]
    Text NameOfFood;

    public event Action UpdateReceiptTotal;


    public void SetFood(Food foodToShow)
    {
        CostText.text = foodToShow.Price.ToString("$ 0.00");
        _Cost = foodToShow.Price; 
        PictureOfProduct.sprite = foodToShow.CaryableObjectSprite;
        NameOfFood.text = foodToShow.Name;

        inputField.text = "0";

        IncrementTotal.StoredCommand = new IncrementValue<decimal>(inputField); //_moneyRefrence
        DecrementTotal.StoredCommand = new DecrementValue<decimal>(inputField); //_moneyRefrence

        inputField.characterValidation = InputField.CharacterValidation.Integer;
        inputField.onValueChanged.AddListener(UpdateReceipt);
        RunningTotalText.text = "$ 0.00";
        
    }

    void UpdateReceipt(string numberIn)
    {
        int numberOfGood;


       if(int.TryParse(numberIn, out numberOfGood))
        {
            inputField.text = numberIn;

            RunningTotal = (_Cost * numberOfGood);
            RunningTotalText.text = RunningTotal.ToString("$ 0.00");

            UpdateReceiptTotal.Invoke();

        }


    }
}
