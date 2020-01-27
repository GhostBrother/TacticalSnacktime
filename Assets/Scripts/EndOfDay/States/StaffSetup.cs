using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaffSetup : MonoBehaviour, iEndOfDayState
{
    [SerializeField]
    List<Image> _frames;
    [SerializeField]
    List<Image> _characterDisplay;
    [SerializeField]
    List<InputField> _TimesIn;
    [SerializeField]
    List<InputField> _TimesOut;

    public EndOfDayPannel EndOfDayPannel { private get; set; }

    public ActionButton ButtonForState { private get;  set; }

    public void DisplayProps()
    {
        for (int i = 0; i < _characterDisplay.Count; i++)
        {
            _frames[i].gameObject.SetActive(true);
            _characterDisplay[i].gameObject.SetActive(true);
            _TimesIn[i].gameObject.SetActive(true);
            _TimesOut[i].gameObject.SetActive(true);
        }
    }

    public void HideProps()
    {
        for (int i = 0; i < _characterDisplay.Count; i++)
        {
            _frames[i].gameObject.SetActive(false);
            _characterDisplay[i].gameObject.SetActive(false);
            _TimesIn[i].gameObject.SetActive(false);
            _TimesOut[i].gameObject.SetActive(false);
        }
    }


}
