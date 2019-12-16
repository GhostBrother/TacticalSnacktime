using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResturantStats : MonoBehaviour
{
    [SerializeField]
    Text goldCount;

    public string goldCounter {get{ return goldCount.text; } set{ goldCount.text = value; }}

    public int ReputationPoints { get; set; }

}
