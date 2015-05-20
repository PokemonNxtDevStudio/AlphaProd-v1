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
        
        if(ID != 0)        
            poke = NxtUiManager.instance.PokemonDB.GetByIDInList(ID);
          
        if (poke != null)                
            Debug.Log("Poke Wasnt Null");        
        SetThisIConInfo(poke);
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
            SetThisIConInfo(other.THEPoke);
            other.THEPoke = tem;
            other.SetThisIConInfo(tem);
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
        if (poke == null)
        {
            thepokeIcon.sprite = NxtUiManager.instance.EmptyPokeball;
            //Debug.Log("THe Poke Was NUll in Setting the Icon" + gameObject.name);           
        }
        else
        {
            thepokeIcon.sprite = poke.Icon;
            //Debug.Log("Slot " + gameObject.name + " Have Pokemon" + poke.Name);
        }  
       // PokeInthis();
    }
 



    public void ShowThisPokemonInfo()
    {
        //AddToCheck if its on the pokept window
        if (poke != null)
        {
            string ty1 = "";
            switch (poke.Type1)
            {
                case PokemonType.Normal:
                    ty1 = "Normal";
                    break;
                case PokemonType.Bug:
                    ty1 = "Bug";
                    break;
                case PokemonType.Electricity:
                    ty1 = "Electic";
                    break;
                case PokemonType.Fire:
                    ty1 = "Fire";
                    break;
                case PokemonType.Flying:
                    ty1 = "Flying";
                    break;
                case PokemonType.Ghost:
                    ty1 = "Ghost";
                    break;
                case PokemonType.Grass:
                    ty1 = "Grass";
                    break;
                case PokemonType.Ground:
                    ty1 = "Ground";
                    break;
                case PokemonType.None:
                    ty1 = "None";
                    break;
                case PokemonType.Poison:
                    ty1 = "Poison";
                    break;
                case PokemonType.Steel:
                    ty1 = "Steel";
                    break;
                case PokemonType.Water:
                    ty1 = "Water";
                    break;

            }
            string ty2 = "";
            switch (poke.Type2)
            {
                case PokemonType.Normal:
                    ty2 = "Normal";
                    break;
                case PokemonType.Bug:
                    ty2 = "Bug";
                    break;
                case PokemonType.Electricity:
                    ty2 = "Electic";
                    break;
                case PokemonType.Fire:
                    ty2 = "Fire";
                    break;
                case PokemonType.Flying:
                    ty2 = "Flying";
                    break;
                case PokemonType.Ghost:
                    ty2 = "Ghost";
                    break;
                case PokemonType.Grass:
                    ty2 = "Grass";
                    break;
                case PokemonType.Ground:
                    ty2 = "Ground";
                    break;
                case PokemonType.None:
                    ty2 = "None";
                    break;
                case PokemonType.Poison:
                    ty2 = "Poison";
                    break;
                case PokemonType.Steel:
                    ty2 = "Steel";
                    break;
                case PokemonType.Water:
                    ty2 = "Water";
                    break;

            }

            NxtUiManager.instance.ShowCurPokemonStatus(poke.Name, poke.level, poke.currentEXP, 100, "normal", ty1, ty2, poke.Health, poke.Attack, poke.defence, poke.PP, poke.speed);

        }
        else
        {
            Debug.Log("None Existing Pokemon");
        }

    }
}
