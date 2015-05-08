using System;
using System.Security.Policy;
using UnityEngine;


public class _delete2 : MonoBehaviour
   {

       private int x;
     
     
       void Start()
       {
           x = 5;
       }

       public void SpeedUP(int y)
       {
           Debug.Log("Speedig up " + (x= x +y));
       }
    }

