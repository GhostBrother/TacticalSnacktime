using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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

    [SerializeField]
    Text MoneyText;

    [SerializeField]
    Text ReputationText;

    public Money money { get; set; }

    public Reputation reputation { get; private set; }

    public Action startNextDay;

    public void Init(CharacterRoster characterRoster)
    {

        money = new Money();
        money.textRefrence = MoneyText;

        reputation = new Reputation();
        reputation.textRefrence = ReputationText;


        startDayButton.StoredCommand = new StartNextDay(startNextDay.Invoke);

        StatsButton.StoredCommand = new ChangeEndOfDayState(this, _Stats);
        _Stats.ButtonForState = StatsButton;
       

        MapButton.StoredCommand = new ChangeEndOfDayState(this, _EditMap);
        _EditMap.ButtonForState = MapButton;
        

        _Supply.EndOfDayPannel = this;
        _Supply.Init(money);
        OrderSupplyButton.StoredCommand = new ChangeEndOfDayState(this, _Supply);
        _Supply.ButtonForState = OrderSupplyButton;
        

         scheduleButton.StoredCommand = new ChangeEndOfDayState(this, _Schedule);
        _Schedule.SetRoster(characterRoster);
        _Schedule.ButtonForState = scheduleButton;
        

        _curState = _Stats;
    }

    public void ShowEndOfDayPage()
    {
        this.gameObject.SetActive(true);
        _Supply.LoadStoreItems();
    }

    public void HideEndOfDayPage()
    {
        ChangeState(_Stats);
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
