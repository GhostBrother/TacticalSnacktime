using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StaffSetup : MonoBehaviour, iEndOfDayState
{

    CharacterRoster _characterRoster;

    public ActionButton ButtonForState { private get;  set; }

    [SerializeField]
    List<StaffEntry> staffEntries;

    Money moneyOnHand;


    public void InitState( Money money)
    {
        moneyOnHand = money;

        
        foreach (StaffEntry s in staffEntries)
        {
            s.CheckTotal = ChangeMoneyBalance;
        }
    }

    public void DisplayProps()
    {
        gameObject.SetActive(true);
        for (int i = 0; i < staffEntries.Count; i++)
        {
            staffEntries[i].gameObject.SetActive(true);
        }
        labelList();
    }

    public void HideProps()
    {
        gameObject.SetActive(false);

        for (int i = 0; i < staffEntries.Count; i++)
        {
            staffEntries[i].gameObject.SetActive(false);
        }

    }

    void labelList()
    {
        int i = 0;
        foreach(PlayercontrolledCharacter c in _characterRoster.employedCharacters)
        {
            staffEntries[i]._characterToShow = c;
            // staffEntries[i].actionButton.StoredCommand = new ShowEmployeeStats(this, staffEntries[i]._characterToShow);
            staffEntries[i].gameObject.SetActive(true);
            staffEntries[i].LabelEntry();
            i++;
        }               
    }

    void ChangeMoneyBalance(decimal moneyBalance)
    {
        moneyOnHand.valueToStore += moneyBalance;
        moneyOnHand.updateTextRefrence();
    }

    public void SetRoster(CharacterRoster characterRoster)
    {
        _characterRoster = characterRoster;
    }


    public void OnStartNextDay()
    {
        // Give game manager who will be on staff to open for the next day. 

        for (int i = 0; i < staffEntries.Count; i++)
        {
          staffEntries[i]._characterToShow.IsGoingToWork = staffEntries[i].totalTimeWorked > new System.TimeSpan(0, 0, 0);
        }
    }
}
