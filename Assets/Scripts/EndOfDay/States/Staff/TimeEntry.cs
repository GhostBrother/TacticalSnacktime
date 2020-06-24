using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeEntry : MonoBehaviour
{
    [SerializeField]
    InputField _Hour;

    [SerializeField]
    InputField _Minute;

    public TimeSpan storedTime { get; private set; }

    public Action FindDiffrence { get; set; } 

    public void init()
    {
        const int _maxNumChars = 2;
        const int _maxHour = 23;
        const int _maxMinute = 59;

        _Hour.text = "00";
        _Hour.contentType = InputField.ContentType.IntegerNumber;
        _Hour.characterLimit = _maxNumChars;
        _Hour.onEndEdit.AddListener(delegate { CheckInput(_Hour, _maxHour); });

        _Minute.text = "00";
        _Minute.contentType = InputField.ContentType.IntegerNumber;
        _Minute.characterLimit = _maxNumChars;
        _Minute.onEndEdit.AddListener(delegate { CheckInput(_Minute, _maxMinute); });

        UpdateTimespan();
    }

    void CheckInput(InputField input, int limit)
    {
        int numToCheck = 0;
        
        if(!int.TryParse(input.text, out numToCheck) || numToCheck > limit)
        {
           input.text = "00";
        }

        UpdateTimespan();
    }

    void UpdateTimespan()
    {
        storedTime = new TimeSpan(int.Parse(_Hour.text),int.Parse( _Minute.text),0);
        FindDiffrence.Invoke();
    }
}
