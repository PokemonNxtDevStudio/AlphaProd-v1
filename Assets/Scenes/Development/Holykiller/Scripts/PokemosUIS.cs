using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class PokemosUIS : MonoBehaviour , IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //[SerializeField]
    private Pokemon poke;
    public Pokemon THEPoke { get { return poke; } set { poke = value; } }

    private Image thepokeIcon;

    public int ID = 0;

    Pokemon tem;

    void Start()
    {
        thepokeIcon = gameObject.GetComponent<Image>();
        
        if(ID > 0)
        {
            poke = NxtUiManager.instance.PokemonDB.GetByID(ID);
            if (poke != null)
                Debug.Log("Poke Wasnt Null");            
        }
        setthisIcon();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {        
        if (poke == null)
            return;
        tem = poke;
        NxtUiManager.instance.PokePic.gameObject.SetActive(true);
        NxtUiManager.instance.PokePic.sprite = poke.Icon;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (poke == null)
            return;

        NxtUiManager.instance.PokePic.transform.position = eventData.position;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (poke == null)
            return;
        NxtUiManager.instance.PokePic.gameObject.SetActive(false);
        PokemosUIS other = eventData.pointerEnter.gameObject.transform.GetComponent<PokemosUIS>();
        if (other == null)
            return;
        else 
        {
            //PokemosUIS other = eventData.pointerCurrentRaycast.gameObject.transform.GetComponent<PokemosUIS>();
            poke = other.THEPoke;
            // Debug.Log("Hit" + other.gameObject.name /*eventData.pointerCurrentRaycast.gameObject.name*/);        
            setthisIcon();
            other.THEPoke = tem;
            other.setthisIcon();
            tem = null;
            return;
        }
      /*  PokeUI otherInPT = eventData.pointerEnter.gameObject.transform.GetComponent<PokeUI>();
        if (otherInPT == null)
            return;
        else
        {
            poke = otherInPT.Pokemon;
            setthisIcon();
            otherInPT.SetThisPokemonIconAndName(tem);
        }*/
    }
    public void setthisIcon()
    {
        if(poke == null)
        {
            thepokeIcon.sprite = NxtUiManager.instance.EmptyPokeball;
            //Debug.Log("THe Poke Was NUll in Setting the Icon" + gameObject.name);           
        }
        else
        {
            thepokeIcon.sprite = poke.Icon;
            //Debug.Log("Slot " + gameObject.name + " Have Pokemon" + poke.Name);
        }       
    }

    public void PokeInthis()
    {
        if (poke != null)
        {
            Debug.Log("This Slot Have " + poke.Name);
        }
        else
        {
            Debug.Log("This Slot is empty");
        }            
    }
    public void SetThisIConInfo(Pokemon pokemon)
    {
        poke = pokemon;
        PokeInthis();
    }
}
