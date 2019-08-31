using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : iCaryable
{
    public enum FoodDoneness { UNDERCOOK, GOOD = 2, GREAT = 3, DELUXE = 4, OVERCOOK = 5, BURN };

    public FoodDoneness Doneness{get; set;}

    public List<Command> HeldObjectCommands { get; private set; }

    public string Name { get; private set; }
    public string Description { get; private set; }

    //price
    public decimal price = 0.00M;
    public decimal Price { get { return price; } set {price = value; } }

    public int HandsRequired { get; set; }

    public int Weight { get; set; }

    public Sprite CaryableObjectSprite { get; private set; }

   
    public Food(string name, decimal _price, Sprite caryableObjectSprite)
    {
        Name = name;
        Price = _price;
        CaryableObjectSprite = caryableObjectSprite;
        HeldObjectCommands = new List<Command>();
    }
}
