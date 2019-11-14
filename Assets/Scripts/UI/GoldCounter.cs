using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldCounter : MonoBehaviour
{
    [SerializeField]
    Text goldCount;

    public string goldCounter {get{ return goldCount.text; } set{ goldCount.text = value; }}
}
