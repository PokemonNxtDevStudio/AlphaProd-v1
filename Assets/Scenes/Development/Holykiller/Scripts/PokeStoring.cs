using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PokeStoring : MonoBehaviour
{
    [SerializeField]
    private PokeParty pokept;

    [SerializeField]
    private int curPage;
    

    
	void Start () 
    {
        
	}
	

	void Update () 
    {
	
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
           pokept = other.GetComponent<PlayerActions>().PokePT;
           if (pokept == null)
               return;
            else
           {
               //Debug.Log("IT Had a pokept in the PlayerActions");
               curPage = pokept.CurPage;
               //enable the current pages or the last one that the trainer had open
               NxtUiManager.instance.StorePages[pokept.CurPage].transform.parent.gameObject.SetActive(true);

            
               //Populate the current pase with the pokemons that owns the player
               for (int i = 0; i < pokept.PokeStoringPages[pokept.CurPage].PagesSlot.Count; i++)
               {
                   NxtUiManager.instance.StorePages[pokept.CurPage].PokemonsInPage[i].THEPoke = pokept.PokeStoringPages[pokept.CurPage].PagesSlot[i];
                   
               }
               NxtUiManager.instance.StorePages[pokept.CurPage].ChangeIcons();
              // pokept.PokeStoringPages[pokept.CurPage].PagesSlot
           }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
           // Debug.Log("Exit Trigger");

            //pokept = other.GetComponent<PlayerActions>().PokePT;
            SetUiToPT();
            //disable the current pages or the last one that the trainer had open
            NxtUiManager.instance.StorePages[pokept.CurPage].transform.parent.gameObject.SetActive(false);

        }
        
    }

    private void SetUiToPT()
    {
        if (pokept == null)
            return;
        else
        {
            for(int i = 0; i < pokept.PokeStoringPages[pokept.CurPage].PagesSlot.Count;i++)
            {
                pokept.PokeStoringPages[pokept.CurPage].PagesSlot[i] = NxtUiManager.instance.StorePages[pokept.CurPage].PokemonsInPage[i].THEPoke;
                if(NxtUiManager.instance.StorePages[pokept.CurPage].PokemonsInPage[i].THEPoke != null)
                {
                  //  Debug.Log("Pokemon" + NxtUiManager.instance.StorePages[pokept.CurPage].PokemonsInPage[i].THEPoke.Name + " Was added");
                }
                
            }
           // pokept.UiUpdate();
            //NxtUiManager.instance.StorePages[pokept.CurPage].SetPokesToPT(pokept, pokept.CurPage);
        }
    }
}
