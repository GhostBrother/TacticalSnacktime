using System;
using System.Collections.Generic;
using UnityEngine;

public class CustomerScheduler 
{
    readonly int[] _HOURSToInclude = { 8 ,9, 10, 11, 12, 1, 2, 3, 4, 5, 6 }; 
    CharacterLoader<AICharacter> _customerLoader;

    public CustomerScheduler()
    {
        _customerLoader = new CharacterLoader<AICharacter>();
    }

    public List<AICharacter> MakeListOfCustomers()
    {
        int numberOfCustomersForDay = 2; //Random.Range(1, 10);
        List<AICharacter> listToReturn = new List<AICharacter>();
        for(int i = 0; i < numberOfCustomersForDay; i++)
        {
            listToReturn.Add(_customerLoader.GetRandomCharacter());
            listToReturn[i].ArrivalTime = MakeTimeSpan(8,30);
            Debug.Log(listToReturn[i].Race + " At " + listToReturn[i].ArrivalTime);
        }
        return listToReturn;
    }

    TimeSpan BuildArrivalTime()
    {
        int hourIndex = UnityEngine.Random.Range(0, _HOURSToInclude.Length);
        return MakeTimeSpan(_HOURSToInclude[hourIndex], UnityEngine.Random.Range(0, 4) * 15);
    }

    TimeSpan MakeTimeSpan(int hour, int minute)
    {
        return new TimeSpan(hour,minute,0);
    }

}
