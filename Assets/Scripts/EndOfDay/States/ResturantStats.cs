using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResturantStats : MonoBehaviour, iEndOfDayState
{
    [SerializeField]
    GameObject _statsTextHolder;

    [SerializeField]
    Text ReputationCount;

    public EndOfDayPannel EndOfDayPannel { private get;  set; }

    public ActionButton ButtonForState { private get;  set; }

    void UpdateReputation()
    {
        ReputationCount.text = string.Format("Reputation {0}",EndOfDayPannel.reputation.Format());
    }

    public void DisplayProps()
    {
        _statsTextHolder.SetActive(true);
        UpdateReputation();
    }

    public void HideProps()
    {
        _statsTextHolder.SetActive(false);
    }

    public void OnStartNextDay()
    {
      
    }
}
