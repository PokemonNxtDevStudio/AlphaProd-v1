using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using UnityEngine;


public abstract class ItemBehavior : MonoBehaviour
{

    private InventoryItem m_item;
    public GameObject target;

    
 

    void Start()
    {
       
        m_item = GetComponent<InventoryItem>();

    }


    public void DeMethod(int x) { }
    public void DeMethod(int x,int y) { }

    public abstract void DoMethod<T>(T Object);  //Generic abstract

}

