using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public  class BottonsManager : MonoBehaviour 
{
    public static BottonsManager instance;
    public List<Bottons> _bottons = new List<Bottons>();

    public void OnEnable()
    {
        if (instance == null)
            instance = this;
    }
    public void AddBoton(Bottons b)
    {
       if(!_bottons.Contains(b))
       {
           _bottons.Add(b);
       }
    }
    public void RemoveBoton(Bottons b)
    {
        _bottons.Remove(b);
    }
}
