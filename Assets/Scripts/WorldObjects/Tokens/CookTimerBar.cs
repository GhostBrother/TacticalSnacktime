using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookTimerBar : MonoBehaviour
{
    Gradient gradient;
    GradientColorKey[] colorKey;

    private void Start()
    {
        gradient = new Gradient();

        colorKey = new GradientColorKey[2];

        colorKey[0].color = Color.red;
        colorKey[1].color = Color.black;
        
    }
}
