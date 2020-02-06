using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResturantStats : MonoBehaviour, iEndOfDayState
{
    [SerializeField]
    GameObject _statsTextHolder;

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
        _statsTextHolder.SetActive(true);
        UpdateGoldText();
        UpdateReputation();
    }

    public void HideProps()
    {
        _statsTextHolder.SetActive(false);
    }
}
