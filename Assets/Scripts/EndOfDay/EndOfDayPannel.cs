using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfDayPannel : MonoBehaviour
{
    iEndOfDayState _curState;

    [SerializeField]
    ResturantStats _Stats;

    [SerializeField]
    EditMap _EditMap;

    [SerializeField]
    OrderingSupplys _Supply;

    [SerializeField]
    StaffSetup _Schedule;

    // Wish I could do this a bit better
    [SerializeField]
    ActionButton startDayButton;

    [SerializeField]
    ActionButton StatsButton;

    [SerializeField]
    ActionButton scheduleButton;

    [SerializeField]
    ActionButton MapButton;

    [SerializeField]
    ActionButton OrderSupplyButton;

    public decimal Money { get; private set; }

    public int Reputation { get; private set; }

    public delegate void NextDay();
    public NextDay startNextDay;

    public void Init()
    {
        startDayButton.StoredCommand = new StartNextDay(startNextDay.Invoke);

        _Stats.EndOfDayPannel = this;
        StatsButton.StoredCommand = new ChangeEndOfDayState(this, _Stats);
        _Stats.ButtonForState = StatsButton;
       

        _EditMap.EndOfDayPannel = this;
        MapButton.StoredCommand = new ChangeEndOfDayState(this, _EditMap);
        _EditMap.ButtonForState = MapButton;
        

        _Supply.EndOfDayPannel = this;
        OrderSupplyButton.StoredCommand = new ChangeEndOfDayState(this, _Supply);
        _Supply.ButtonForState = OrderSupplyButton;
        

        _Schedule.EndOfDayPannel = this;
         scheduleButton.StoredCommand = new ChangeEndOfDayState(this, _Schedule);
        _Schedule.ButtonForState = scheduleButton;
        

        _curState = _Stats;
    }

    public void AddMoney(decimal price)
    {
        Money += price;
    }

    public void SubtractMoney(decimal cost)
    {
        Money -= cost;
    }

    public void AddReputation(int satisfaction)
    {
        Reputation += satisfaction;
    }

    public void SubtractReputation(int satisfaction)
    {
        Reputation -= satisfaction;
    }

    public void ShowEndOfDayPage()
    {
        this.gameObject.SetActive(true);   
    }

    public void HideEndOfDayPage()
    {
        this.gameObject.SetActive(false);
    }

    public void ChangeState(iEndOfDayState newState)
    {
        _curState = newState;
    }

    public void ShowPropsForState()
    {
        _curState.DisplayProps();
    }

    public void HidePropsForState()
    {
        _curState.HideProps();
    }
}
