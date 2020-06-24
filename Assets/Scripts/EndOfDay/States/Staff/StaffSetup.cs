using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StaffSetup : MonoBehaviour, iEndOfDayState
{

    CharacterRoster _characterRoster;

    public ActionButton ButtonForState { private get;  set; }

    [SerializeField]
    List<StaffEntry> staffEntries;

    public void DisplayProps()
    {
        gameObject.SetActive(true);

        for(int i = 0; i < staffEntries.Count; i++)
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
        while (!_characterRoster.IsListEmpty())
        {
            staffEntries[i]._characterToShow = _characterRoster.GetCharacterOnTopOfList();
            // staffEntries[i].actionButton.StoredCommand = new ShowEmployeeStats(this, staffEntries[i]._characterToShow);
            staffEntries[i].gameObject.SetActive(true);
            staffEntries[i].LabelEntry();
            i++;
        }

        for (int j = i; j < staffEntries.Count; j++)
        {
            //staffEntries[j].actionButton.StoredCommand = new HireNewEmployee();
            staffEntries[i].gameObject.SetActive(false);
        }
                
    }

    public void SetRoster(CharacterRoster characterRoster)
    {
        _characterRoster = characterRoster;
    }


    public void OnStartNextDay()
    {
        // Give game manager who will be on staff to open for the next day. 
    }
}
