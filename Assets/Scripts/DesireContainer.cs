using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesireContainer
{
    FoodLoader foodLoader = new FoodLoader();
    Dictionary<string, List<Food>> customerLookUp;

    const string COMMON = "Common";


    public DesireContainer()
    {
        customerLookUp = new Dictionary<string, List<Food>>();
        LoadContainer();
    }

    public Food chooseWhatToEatBasedOnTaste(string customer)
    {
        int rand;
        if (customerLookUp.ContainsKey(customer))
        {
            rand = Random.Range(0, customerLookUp[customer].Count);
        }
        else
            rand = Random.Range(0, customerLookUp[COMMON].Count);

        return customerLookUp[customer][rand];
    }

    private void LoadContainer()
    {
        for(int i = 0; i < foodLoader.AllFoods.Count; i++)
        {
            for( int j = 0; j < foodLoader.AllFoods[i].CustomersWhoLikeThis.Count; j++)
            {
                if (!customerLookUp.ContainsKey(foodLoader.AllFoods[i].CustomersWhoLikeThis[j]))
                    customerLookUp.Add(foodLoader.AllFoods[i].CustomersWhoLikeThis[j], new List<Food>());

                customerLookUp[foodLoader.AllFoods[i].CustomersWhoLikeThis[j]].Add(foodLoader.AllFoods[i]);
            }
        }
    }


}
