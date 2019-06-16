using System.Collections;
using System.Collections.Generic;

public class Food : iCaryable
{
    public string Name { get; private set; }
    public string Description { get; private set; }

    // The skill at which this was prepared, 100 is best.  
    public int Skill { get; set; }

    //price
    public decimal price = 0.00M;
    public decimal Price { get { return price; } set {price = value; } }

    public int HandsRequired { get; set; }

    public int Weight { get; set; }

    // for right now I just want to serve a burger to a kobold. 
    public Food(string name, decimal _price)
    {
        Name = name;
        Price = _price;
    }
}
