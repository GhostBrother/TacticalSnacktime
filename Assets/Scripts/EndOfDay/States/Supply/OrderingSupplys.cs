using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderingSupplys : MonoBehaviour, iEndOfDayState
{

    [SerializeField]
    GameObject _orderSupplyPage;

    public EndOfDayPannel EndOfDayPannel { private get; set; }
    public ActionButton ButtonForState { private get;  set; }

    public void DisplayProps()
    {
        _orderSupplyPage.SetActive(true);
    }

    public void HideProps()
    {
        _orderSupplyPage.SetActive(false);
    }


}
