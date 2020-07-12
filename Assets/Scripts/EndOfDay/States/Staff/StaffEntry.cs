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


    TimeSpan totalTimeWorked;

    public Action<decimal> CheckTotal { get; set; }



    public void LabelEntry()
    {
        _CharacterName.text = _characterToShow.Name;
        _CharacterArt.sprite = _characterToShow.PawnSprite;

        _RatePerHour.text = _characterToShow.payPerHour.Format();

        _TimeIn.FindDiffrence = CalculatePayPerHour;
        _TimeOut.FindDiffrence = CalculatePayPerHour;

        _TimeIn.init();
        _TimeOut.init();
    }

    void CalculatePayPerHour()
    {
        
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

//{
        //    decimal payForhours = 0;

        //    if (totalTimeWorked <= totalTime && totalTime.TotalHours >= 0)
        //    {
        //        payForhours = ((-(decimal)totalTimeWorked.TotalHours - (decimal)totalTime.TotalHours) * _characterToShow.payPerHour.valueToStore);
        //    }

        //    if (totalTimeWorked > totalTime && totalTime.TotalHours >= 0)
        //    {
        //        payForhours = (((decimal)totalTimeWorked.TotalHours - (decimal)totalTime.TotalHours) * _characterToShow.payPerHour.valueToStore);
        //    }

        //    _characterToShow.ArrivalTime = _TimeIn.storedTime.ToString();
        //    _characterToShow.LeaveTime = _TimeOut.storedTime.ToString();
        //    _TotalForDay.text = payForhours.ToString();
        //    totalTimeWorked = totalTime;
        //    CheckTotal.Invoke(payForhours);





    //TimeSpan CheckIfCanAfford(TimeSpan totalTime)
    //{
    //    // TODO, figgure out math to make it set time out to the max number it could be. 
    //    //decimal placeHolder = CheckTotal.Invoke();
    //    //placeHolder += -((decimal)totalTime.TotalHours * _characterToShow.payPerHour.valueToStore);
    //    //if (placeHolder < 0)
    //    //{
    //    //    return new TimeSpan(0, 0, 0);
    //    //}
        
    //    //return totalTime;
    //}
}
