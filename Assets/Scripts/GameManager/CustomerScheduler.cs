using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomerScheduler 
{

    readonly int[] _HOURSToInclude = { 8 ,9, 10, 11, 12, 1, 2, 3, 4, 5, 6 }; 
    CharacterLoader<AICharacter> _customerLoader;
    int _debugMultiplier = 1; 

    public CustomerScheduler()
    {
        _customerLoader = new CharacterLoader<AICharacter>();
    }

    public List<AICharacter> MakeListOfCustomers()
    {
        int numberOfCustomersForDay = 2;//Random.Range(1, 10);
        List<AICharacter> listToReturn = new List<AICharacter>();
        for(int i = 0; i < numberOfCustomersForDay; i++)
        {
            listToReturn.Add(_customerLoader.GetRandomCharacter());
            listToReturn[i].ArrivalTime = BuildSpecificTime(8,30);//BuildArrivalTime();
            Debug.Log(listToReturn[i].Race + " At " + listToReturn[i].ArrivalTime);
        }
        return listToReturn;
    }


    string BuildArrivalTime()
    {
        int hourIndex = Random.Range(0, _HOURSToInclude.Length);

        // string time = _HOURSToInclude[hourIndex].ToString().PadLeft(2, '0'); 
        // time += ":" + (Random.Range(0,4) * 15).ToString().PadLeft(2,'0');
        return BuildSpecificTime(_HOURSToInclude[hourIndex], Random.Range(0, 4) * 15);//time;
    }

    string BuildSpecificTime(int hour, int minute)
    {
        string time = string.Empty;
        if (_HOURSToInclude.Contains(hour))
            time = hour.ToString().PadLeft(2, '0');

        if (minute % 15 == 0)
            time += ":" + minute.ToString().PadLeft(2, '0');
         
        return time;
    }

}
