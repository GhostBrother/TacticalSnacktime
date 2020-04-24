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

    //Set

    public void Init()
    {
        IncrementTotal.StoredCommand = new IncrementValue(this);
        DecrementTotal.StoredCommand = new DecrementValue(this);
    }

    public void SetFood (Food foodToShow)
    {
        // Might need to find a way to flux prices, looking at visitor pattern. 
        CostText.text = FormatMoney(foodToShow.Price);

        _Cost = foodToShow.Price;

        // TODO quantity owned, we need how much the player has. 

        PictureOfProduct.sprite = foodToShow.CaryableObjectSprite;

        NameOfFood.text = foodToShow.Name;
        _RunningTotal = 0;
        RunningTotalText.text = FormatMoney(_RunningTotal); 
    }

    public void IncrementCount()
    {
        _RunningTotal += _Cost;
        RunningTotalText.text = FormatMoney(_RunningTotal);
    }

    public void DecrementCount()
    {
        if (_RunningTotal > 0)
        {
            _RunningTotal -= _Cost;
            RunningTotalText.text = FormatMoney(_RunningTotal);
        }
    }

    string FormatMoney(decimal money)
    {
        return "$ " + money.ToString("0.00");
    }

}
