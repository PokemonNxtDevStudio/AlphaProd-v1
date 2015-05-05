using System;
using UnityEngine;
using UnityEngine.Rendering;


public class RestorePPBehavior : ItemBehavior
{
    //FUNC
    public Action<int> customMethod;

    void Start()
    {
        customMethod = GetComponent<_delete2>().SpeedUP;
    }
      public override void DoMethod<T>(T Object)
    {
        customMethod.Invoke(10);
    }
}

