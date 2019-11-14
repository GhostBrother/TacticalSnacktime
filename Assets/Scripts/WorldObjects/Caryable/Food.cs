using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : iCaryable
{
    public int[] DonenessesLevels = new int[6];
    public int CurrentDoness;


    public List<Command> HeldObjectCommands { get; private set; }

    public string Name { get; private set; }
    public string Description { get; private set; }

    //price
    public decimal price = 0.00M;
    public decimal Price { get { return price; } set {price = value; } }

    public int HandsRequired { get; set; }

    public int Weight { get; set; }

    public Sprite CaryableObjectSprite { get; private set; }

    public int NumberOfItemsInSupply { get; set; }

    public int ID;

    public List<string> CustomersWhoLikeThis { get; private set; }



    public Food(string Name, decimal Price, int[] DonenessesLevels, string Description, int HandsRequired, int ID, List<string> Customers)
    {
        this.Name = Name;
        this.Price = Price;
        this.ID = ID;
        CaryableObjectSprite = SpriteHolder.instance.GetFoodArtFromIDNumber(ID);
        this.Description = Description;
        this.DonenessesLevels = DonenessesLevels;
        this.HandsRequired = HandsRequired;
        HeldObjectCommands = new List<Command>();
        CustomersWhoLikeThis = Customers;
        NumberOfItemsInSupply = 1;
    }
}
