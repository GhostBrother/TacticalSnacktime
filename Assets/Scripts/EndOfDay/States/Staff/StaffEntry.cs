using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaffEntry : MonoBehaviour
{
   public PlayercontrolledCharacter _characterToShow { get; set; }
    public ActionButton actionButton { get { return this.GetComponent<ActionButton>(); }}


    [SerializeField]
    InputField _TimeIn;

    [SerializeField]
    InputField _TimeOut;

    public void IsTimeSheetShown(bool yesNo)
    {
        _TimeIn.gameObject.SetActive(yesNo);
        _TimeOut.gameObject.SetActive(yesNo);
    }

}
