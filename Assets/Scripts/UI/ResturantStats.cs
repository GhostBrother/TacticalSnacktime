using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResturantStats : MonoBehaviour
{
    [SerializeField]
    Text goldCount;

    [SerializeField]
    Text influenceCount;

    // Wish I could do this a bit better
    [SerializeField]
    ActionButton startDayButton;

    public delegate void NextDay();
    public NextDay startNextDay;

    public string GoldCounter {get{ return goldCount.text; } set{ goldCount.text = FormatMoneyText(value); }}

    public string ReputationCounter { get { return influenceCount.text; } set { influenceCount.text = value; } }

    private string FormatMoneyText(string s)
    {
        return string.Format("{0:0.00}", s);
    }

    public void ShowEndPage(bool ShowPage)
    {
        this.gameObject.SetActive(ShowPage);
    }

    public void init()
    {
        // Refactor this to some sort of bututton super class that does not have this, or better yet factor out the onAction taken part. 
        startDayButton.StoredCommand = new StartNextDay(startNextDay.Invoke);
        startDayButton.onActionTaken = startNextDay.Invoke;
    }

}
