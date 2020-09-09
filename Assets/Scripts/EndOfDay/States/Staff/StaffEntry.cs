using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaffEntry : MonoBehaviour
{
   public PlayercontrolledCharacter _characterToShow { get; set; }
    public ActionButton actionButton { get { return this.GetComponent<ActionButton>(); }}
    
    [SerializeField]
    Text _CharacterName;
 
    [SerializeField]
    TimeEntry _TimeIn;

    [SerializeField]
    TimeEntry _TimeOut;

    [SerializeField]
    Image _Frame;

    [SerializeField]
    Image _CharacterArt;

    [SerializeField]
    Text _RatePerHour;

    [SerializeField]
    Text _TotalForDay;

    [SerializeField]
    ActionButton _MoreInfoButton;


    public TimeSpan totalTimeWorked { get; private set; }

    public Action<decimal> CheckTotal { get; set; }

    public Func<decimal> GetCurrentValue;

    public void LabelEntry()
    {
        _CharacterName.text = _characterToShow.Name;
        _CharacterArt.sprite = _characterToShow.characterArt;

        _RatePerHour.text = _characterToShow.payPerHour.Format();

        _TimeIn.FindDiffrence = CalculatePayPerHour;
        _TimeOut.FindDiffrence = CalculatePayPerHour;

        _TimeIn.init();
        _TimeOut.init();
    }

    void CalculatePayPerHour()
    {
        _characterToShow.ArrivalTime = _TimeIn.storedTime;
        TimeSpan totalTime = _TimeOut.storedTime.Subtract(_TimeIn.storedTime);
        decimal payForHours = 0; 

        if (totalTime.TotalHours <= 0)
        {
            totalTime = new TimeSpan(0, 0, 0);
        }

        payForHours = (decimal)(totalTimeWorked.TotalHours - totalTime.TotalHours) * _characterToShow.payPerHour.valueToStore;
        totalTimeWorked = totalTime;
        CheckTotal.Invoke(payForHours);
    }


    //void CheckIfCanAfford(decimal payForHours)
    //{
    //    //// TODO, figgure out math to make it set time out to the max number it could be.
    //    //double placeHolder =  ((double)GetCurrentValue.Invoke() + (double)payForHours);
    //    //if (placeHolder < 0 )
    //    //{
    //    //        TimeSpan temp =  TimeSpan.FromHours(Math.Ceiling((double)(placeHolder / (double)_characterToShow.payPerHour.valueToStore)));
    //    //        Debug.Log(temp);
    //    //        _TimeOut.UpdateHour(_TimeIn.storedTime.Hours - temp.Hours);
    //    //        _TimeOut.UpdateMinute(_TimeIn.storedTime.Minutes - temp.Minutes);
        
    //    //}

    //}
}
