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

    public string GoldCounter {get{ return goldCount.text; } set{ goldCount.text = FormatMoneyText(value); }}

    public string ReputationCounter { get { return influenceCount.text; } set { influenceCount.text = value; } }

    private string FormatMoneyText(string s)
    {
        Debug.Log(s);
        Debug.Log(string.Format("{0:0.00}", s));
        return string.Format("{0:0.00}", s);
    }

}
