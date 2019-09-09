using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : iCaryable
{
    public int[] Doneness = new int[6];
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

   
    public Food(string name, decimal _price, Sprite caryableObjectSprite)
    {
        Name = name;
        Price = _price;
        CaryableObjectSprite = caryableObjectSprite;
        HeldObjectCommands = new List<Command>();
        for(int i = 0; i < Doneness.Length; i++)
        {
            Doneness[i] = i * 2;
        }
    }
}
