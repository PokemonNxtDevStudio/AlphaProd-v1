using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public  class BotonsManager : MonoBehaviour 
{    
    public static BotonsManager instance;
    public List<Botons> _botons = new List<Botons>();

    public void OnEnable()
    {
        if (instance == null)
            instance = this;
    }
    public void AddBoton(Botons b)
    {
       if(!_botons.Contains(b))
       {
           _botons.Add(b);
       }
    }
    public void RemoveBoton(Botons b)
    {
        _botons.Remove(b);
    }
}
