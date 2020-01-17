using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerScheduler 
{

    readonly int[] _HOURSToInclude = { 9, 10, 11, 12, 1, 2, 3, 4, 5, 6 }; 
    CharacterLoader<AICharacter> _customerLoader;
    int _debugMultiplier = 1; 

    public CustomerScheduler()
    {
        _customerLoader = new CharacterLoader<AICharacter>();
    }

    public List<AICharacter> MakeListOfCustomers()
    {
        int numberOfCustomersForDay = Random.Range(1, 10);
        List<AICharacter> listToReturn = new List<AICharacter>();
        for(int i = 0; i < numberOfCustomersForDay; i++)
        {
            listToReturn.Add(_customerLoader.GetRandomCharacter());
            listToReturn[i].ArrivalTime = BuildArrivalTime();
            Debug.Log(listToReturn[i].Race + " At " + listToReturn[i].ArrivalTime);
        }
        return listToReturn;
    }


    string BuildArrivalTime()
    {
        int hourIndex = Random.Range(0, _HOURSToInclude.Length);

        string time = _HOURSToInclude[hourIndex].ToString().PadLeft(2, '0'); 
        time += ":" + (Random.Range(0,4) * 15).ToString().PadLeft(2,'0');
        return time;
    }

}
