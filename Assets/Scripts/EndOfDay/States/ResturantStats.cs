using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResturantStats : MonoBehaviour, iEndOfDayState
{
    [SerializeField]
    Text goldCount;

    [SerializeField]
    Text ReputationCount;

    public EndOfDayPannel EndOfDayPannel { private get;  set; }

    public ActionButton ButtonForState { private get;  set; }

    void UpdateGoldText()
    {
        goldCount.text = string.Format("Gold {0:0.00}", EndOfDayPannel.Money);
    }

    void UpdateReputation()
    {
        ReputationCount.text = string.Format("Reputation {0}",EndOfDayPannel.Reputation);
    }

    public void DisplayProps()
    {
        goldCount.gameObject.SetActive(true);
        UpdateGoldText();
        ReputationCount.gameObject.SetActive(true);
        UpdateReputation();

    }

    public void HideProps()
    {
        goldCount.gameObject.SetActive(false);
        ReputationCount.gameObject.SetActive(false);
    }
}
