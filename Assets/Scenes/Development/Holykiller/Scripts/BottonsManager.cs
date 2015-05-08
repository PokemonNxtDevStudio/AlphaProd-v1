using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public  class BottonsManager : MonoBehaviour 
{
    public static BottonsManager instance;
    public List<Bottons> _bottons = new List<Bottons>();
    public List<NPCStore> m_npcStores = new List<NPCStore>();

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
    public void AddNPCStore(NPCStore n)
    {
        if(!m_npcStores.Contains(n))
        {
            m_npcStores.Add(n);
            n.InventoryUI = gameObject.GetComponent<HInventory>().InventoryUI;
        }
        
    }
    public void RemoveNPCStore(NPCStore n)
    {
        m_npcStores.Remove(n);
    }
}
