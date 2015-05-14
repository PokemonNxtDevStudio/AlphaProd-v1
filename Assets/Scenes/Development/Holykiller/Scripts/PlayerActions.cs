using UnityEngine;
using System.Collections;

public class PlayerActions : MonoBehaviour
{
    //private HInventory inventory;

    //public static HInventory instance;

   // void Awake()
   // {
   //     if(instance == null)
   //     {
    //        instance = inventory;
    //    }
  //  }

  //  void Start()
  //  {
     //   NxtUiManager.instance.ShowMoney(inventory.Money);       
   // }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            NXT.EventHandler.ExecuteEvent(this.gameObject, "OnShopRequest");
        }
        if(Input.GetKeyDown(KeyCode.I))
        {
            NXT.EventHandler.ExecuteEvent(this.gameObject, "ShowInventory");
        }
    }
}
