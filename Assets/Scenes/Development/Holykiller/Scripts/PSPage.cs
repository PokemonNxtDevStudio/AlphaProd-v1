using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PSPage 
{
    private List<Pokemon> m_PageSlot;
    public List<Pokemon> PagesSlot { get { return m_PageSlot; } set { m_PageSlot = value; } }
    private int PageSize = 60;
    public PSPage()
    {
        m_PageSlot = new List<Pokemon>();
        for (int i = 0; i < PageSize; i++)
        {
            Pokemon noll = null;
            m_PageSlot.Add(noll);
        }
    }
    

}
