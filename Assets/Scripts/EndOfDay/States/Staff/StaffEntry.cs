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
        if (totalTime.TotalHours <= 0)
        {
            totalTime = TimeSpan.Zero;
            _characterToShow.ArrivalTime = TimeSpan.Zero.ToString();
            _characterToShow.LeaveTime = TimeSpan.Zero.ToString();
        }
        else
        {
            _characterToShow.ArrivalTime = _TimeIn.storedTime.ToString();
            _characterToShow.LeaveTime = _TimeOut.storedTime.ToString();
        }
        _TotalForDay.text = (totalTime.TotalHours * Decimal.ToDouble(_characterToShow.payPerHour.valueToStore)).ToString();
    }

}
