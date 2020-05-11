using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaffSetup : MonoBehaviour, iEndOfDayState
{
    [SerializeField]
    ActionButton LeftStaffArrow;

    [SerializeField]
    ActionButton RightStaffArrow;

    [SerializeField]
    Image _frame;
    [SerializeField]
    Image _characterDisplay;

    [SerializeField]
    GameObject _staffPage;

    CharacterRoster _characterRoster;

    public ActionButton ButtonForState { private get;  set; }

    [SerializeField]
    List<StaffEntry> staffEntries;

    public void DisplayProps()
    {
        _staffPage.gameObject.SetActive(true);
        LeftStaffArrow.gameObject.SetActive(true);
        RightStaffArrow.gameObject.SetActive(true);
        _frame.gameObject.SetActive(true);
        _characterDisplay.gameObject.SetActive(true);

        for(int i = 0; i < staffEntries.Count; i++)
        {
            staffEntries[i].gameObject.SetActive(true);
        }
        labelList();
    }

    public void HideProps()
    {
        _staffPage.gameObject.SetActive(false);
        LeftStaffArrow.gameObject.SetActive(false);
        RightStaffArrow.gameObject.SetActive(false);
        _frame.gameObject.SetActive(false);
        _characterDisplay.gameObject.SetActive(false);

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
            staffEntries[i].actionButton.StoredCommand = new ShowEmployeeStats(this, staffEntries[i]._characterToShow);
            staffEntries[i].IsTimeSheetShown(true);
            i++;
        }

        for (int j = i; j < staffEntries.Count; j++)
        {
            staffEntries[j].actionButton.StoredCommand = new HireNewEmployee();
            staffEntries[j].IsTimeSheetShown(false);
        }
                
    }

    public void SetRoster(CharacterRoster characterRoster)
    {
        _characterRoster = characterRoster;
    }

    public void ShowCharacterArt(Sprite sprite)
    {
        _characterDisplay.sprite = sprite;
    }


}
