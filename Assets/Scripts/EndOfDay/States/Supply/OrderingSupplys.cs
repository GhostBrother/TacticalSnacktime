using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderingSupplys : MonoBehaviour, iEndOfDayState
{

    [SerializeField]
    SupplyStore _orderSupplyPage;

    public EndOfDayPannel EndOfDayPannel { private get; set; }
    public ActionButton ButtonForState { private get;  set; }

    public void Init()
    {
        _orderSupplyPage.Init();
    }

    public void DisplayProps()
    {
        _orderSupplyPage.gameObject.SetActive(true);
    }

    public void HideProps()
    {
        _orderSupplyPage.gameObject.SetActive(false);
    }

    public void LoadStoreItems()
    {
        _orderSupplyPage.LoadShop();
    }
}
