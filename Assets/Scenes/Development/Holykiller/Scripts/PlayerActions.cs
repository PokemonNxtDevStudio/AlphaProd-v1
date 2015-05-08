using UnityEngine;
using System.Collections;

public class PlayerActions : MonoBehaviour
{

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            NXT.EventHandler.ExecuteEvent(this.gameObject, "OnShopRequest");
        }

    }
}
