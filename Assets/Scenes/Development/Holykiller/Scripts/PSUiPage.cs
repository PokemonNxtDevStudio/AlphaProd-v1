using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PSUiPage : MonoBehaviour
{
    [SerializeField]
    private List<PokemosUIS> m_PokemonsInPage = new List<PokemosUIS>();
    public List<PokemosUIS> PokemonsInPage { get { return m_PokemonsInPage; } set { m_PokemonsInPage = value; } }


	void Start () 
    {
	
	}
    public void ChangeIcons()
    {
        for(int i = 0 ;i < m_PokemonsInPage.Count;i ++)
        {
            m_PokemonsInPage[i].SetThisIConInfo(m_PokemonsInPage[i].THEPoke);
        }
    }
	
    public void SetPokesToPT(PokeParty pokept,int page)
    {
        for(int i = 0 ;i < m_PokemonsInPage.Count;i++)
        {
            pokept.PokeStoringPages[page].PagesSlot[i] = m_PokemonsInPage[i].THEPoke;
        }       
    }
    public void AddToList(PokemosUIS poke)
    {
        if(!m_PokemonsInPage.Contains(poke))
        {
            m_PokemonsInPage.Add(poke);
        }
    }
    void OnDisable()
    {

    }
}
