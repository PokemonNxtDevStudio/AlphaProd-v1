using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PokeParty
{
    public const int PARTY_MAX = 6;

    Trainer Trainer = new Trainer(); //Enables it to be usable by any trainer in multiplayer (independant)
    
    List<PokeSlot> slots;
    private int selectedIndex;

    PokemonsInPt pokePT;
    public PokemonsInPt PokemonsParty { get { return pokePT; } set { pokePT = value; } }
   

    private List<PSPage> PsPages;
    public List<PSPage> PokeStoringPages { get { return PsPages; } set { PsPages = value;} }

    private int m_curPage = 0;
    public int CurPage { get { return m_curPage; } set { m_curPage = value; } }
    private int m_PagesUnlocked = 1;

    #region NewCodes

    public PokeParty()
    {
        PsPages = new List<PSPage>();
        PSPage page = new PSPage();
        PsPages.Add(page);

        pokePT = new PokemonsInPt();        
    }

    public void AddAStoringPage()
    {
        m_PagesUnlocked++;

        PSPage page = new PSPage();
        PsPages.Add(page);
    }

    public void AddToPSS(Pokemon poke)
    {
        if (poke == null)
            return;
        Pokemon temp = poke;
        bool findspace = false;
        for(int i = 0; i < PsPages.Count;i++)
        {
            for(int x = 0;x < PsPages[i].PagesSlot.Count;x++)
            {
                if(PsPages[i].PagesSlot[x] == null)
                {
                    PsPages[i].PagesSlot[x] = temp;
                    poke = null;
                    findspace = true;
                    Debug.Log("Added to page " + (i + 1).ToString() + " Slot " + (x + 1).ToString());
                    return;
                }
            }
        }
        //If didnt find any free space
        if(findspace == false)
        {

            Debug.Log("cant Add this pokemon");
        }
        
        
    }


    public void AddToPokePtByID(int id)
    {

        if (pokePT.CanAddPoke() == false)
        {
            Debug.Log("Cant Add More Pokemons");
            return;
        }
        else
        {
            Pokemon poke = new Pokemon();
            if (NxtUiManager.instance.PokemonDB.GetByIDInList(id) == null)
            {
                Debug.Log("Pokemon with id :" + id + " Is Null");
            }
            /*
            poke.ID = NxtUiManager.instance.PokemonDB.GetByIDInList(id).ID;
            poke.Name = NxtUiManager.instance.PokemonDB.GetByIDInList(id).Name;
            poke.PP = NxtUiManager.instance.PokemonDB.GetByIDInList(id).PP;
            poke.Icon = NxtUiManager.instance.PokemonDB.GetByIDInList(id).Icon;
            poke.Type1 = NxtUiManager.instance.PokemonDB.GetByIDInList(id).Type1;
            poke.Type2 = NxtUiManager.instance.PokemonDB.GetByIDInList(id).Type2;
            poke.PokemonPrefab = NxtUiManager.instance.PokemonDB.GetByIDInList(id).PokemonPrefab;
            poke.Moves = NxtUiManager.instance.PokemonDB.GetByIDInList(id).Moves;
            poke.LearnMovesLevels = NxtUiManager.instance.PokemonDB.GetByIDInList(id).LearnMovesLevels;
            */
            AddToPokePT(poke);
        }       
    }    
    public void AddToPokePT(Pokemon poke)
    {
        for (int i = 0; i < pokePT.PokemonsInThePt.Count; i++)
        {
            if (pokePT.PokemonsInThePt[i] == null)
            {
                pokePT.PokemonsInThePt[i] = poke;
                PokePTUIUpdate();
                return;
            }
        }
    }
    public void PokePTUIUpdate()
    {
        NxtUiManager.instance.PokePtUIUpdate();
    }

    #endregion


    #region OldCode
    public void SetPokeSlotsInActive()
    {
        selectedIndex = -1;

    }
    public int SelectedIndex
    {
        get { return selectedIndex; }
        set { /*if (SlotCount() >= selectedIndex)*/ selectedIndex = value; }
    }
    public PokeParty(Trainer trainer)
    {
        this.Trainer.ID = trainer.ID;
        this.Trainer = trainer;
        slots = new List<PokeSlot>();
        GetPokeSlot(-1); //Assume the trainer has no pokemon
    }
   

    
    /*
    public void AddAPokemon(Pokemon pokemon)
    {
        if(slots.Count >= PARTY_MAX)
        {
            Debug.Log("Cant Add more pokemons");
            Debug.Log("Send to the pokeStoringPc");
        }
        else 
        {
            PokeSlot slot = new PokeSlot(this, pokemon);
            GetSlots().Add(slot);
            NxtUiManager.instance.PokemonsInPtUI[slots.Count - 1].SetThisPokemonIconAndName(pokemon);
           
            // if (selectedIndex == -1)
               // GetPokeSlot(slot.index); //Select by default if no pokemon is selectedIndex

            //return true;
        }
    }*/
    public int SlotCount()
    {
        return slots.Count;
    }

    public bool HasPokemon()
    {
        return SlotCount() > 0;
    }

    public List<PokeSlot> GetSlots()
    {
        return slots;
    }

    public PokeSlot GetSlot(int index)
    {
        return GetSlots()[index];
    }

    public PokeSlot GetActive()
    {
        if (selectedIndex == -1)
            return null;

        var slot = GetSlot(selectedIndex);
        return (slot != null) ? slot : null;
    }

    public Pokemon GetActivePokemon()
    {
        var slot = GetActive();
        return (slot != null) ? slot.pokemon : null;
    }

    public bool IsActive(Pokemon pokemon)
    {
        return GetActivePokemon() == pokemon;
    }

    public bool CanAddPokemon()
    {
        return SlotCount()/* + 1 */< PARTY_MAX;
    }

    public bool AddPokemon(Pokemon pokemon)
    {
        if (!CanAddPokemon())
            return false;

        PokeSlot slot = new PokeSlot(this, pokemon);
        GetSlots().Add(slot);
        UiUpdate();
        //NxtUiManager.instance.PokemonsInPtUI[slots.Count - 1].SetThisPokemonIconAndName(pokemon);

        if (selectedIndex == -1)
            GetPokeSlot(slot.index); //Select by default if no pokemon is selectedIndex

        return true;
    }
    public bool AddPokemonByID(int id)
    {
        /*
        if (!CanAddPokemon())
            return false;
        Pokemon poke = new Pokemon();
        if(NxtUiManager.instance.PokemonDB.GetByIDInList(id) == null)
        {
            Debug.Log("Pokemon with id :" + id + " Is Null");
        }
        poke.ID = NxtUiManager.instance.PokemonDB.GetByIDInList(id).ID;
        poke.Name = NxtUiManager.instance.PokemonDB.GetByIDInList(id).Name;
        poke.PP = NxtUiManager.instance.PokemonDB.GetByIDInList(id).PP;
        poke.Icon = NxtUiManager.instance.PokemonDB.GetByIDInList(id).Icon;
        poke.Type1 = NxtUiManager.instance.PokemonDB.GetByIDInList(id).Type1;
        poke.Type2 = NxtUiManager.instance.PokemonDB.GetByIDInList(id).Type2;
        poke.PokemonPrefab = NxtUiManager.instance.PokemonDB.GetByIDInList(id).PokemonPrefab;
        poke.Moves = NxtUiManager.instance.PokemonDB.GetByIDInList(id).Moves;
        poke.LearnMovesLevels = NxtUiManager.instance.PokemonDB.GetByIDInList(id).LearnMovesLevels;
        PokeSlot slot = new PokeSlot(this, poke);
        GetSlots().Add(slot);
        UiUpdate();
        //NxtUiManager.instance.PokemonsInPtUI[slots.Count - 1].SetThisPokemonIconAndName(pokemon);

        if (selectedIndex == -1)
            GetPokeSlot(slot.index); //Select by default if no pokemon is selectedIndex

     
         */   return true;
    }
    public void UiUpdate()
    {
        NxtUiManager.instance.SetCurPokesUis(slots);
    }
    public void RemovePokemon(int index)
    {
       // PokeSlot slot = GetSlot(index);
        slots.RemoveAt(index);

        if (selectedIndex == index) //If the current Pokemon was removed, select the previous. (If there are none left, it will set it correctly to -1)
            GetPokeSlot(SlotCount() - 1);
    }
    public int GetSelectedIndex()
    {
        return selectedIndex;
    }
    public int GetActiveSlotIndex()
    {
        return selectedIndex;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public PokeSlot GetPokeSlot(int index)
    {
        if (index < 0 || index >= SlotCount())
        { //Check if it's an invalid slot
            selectedIndex = -1;
            return null;
        }

        PokeSlot slot = GetSlots()[index];
        selectedIndex = index;

        return slot;
    }
    public Pokemon GetPokeBySlot(int index)
    {
        if (index < 0 || index >= SlotCount())
        { //Check if it's an invalid slot
            selectedIndex = -1;
            return null;
        }

        PokeSlot slot = GetSlots()[index];

        return slot.pokemon;
    }

    public PokeSlot SelectNext()
    {
        var index = (selectedIndex - 1) % SlotCount();  //Loop slot index when beyond bounds
        return GetPokeSlot(index);
    }

    public PokeSlot SelectPrev()
    {
        var index = ((selectedIndex - 1) + SlotCount() - 1) % SlotCount();  //Loop slot index when below bounds
        return GetPokeSlot(index);
    }

    public void Swap(int index1, int index2)
    {
        var slots = GetSlots();

        if (System.Diagnostics.Debugger.IsAttached)
        {
            if ((index1 < 0 || index1 >= SlotCount()) || (index2 < 0 || index2 >= SlotCount()))
                throw new Exception("Error: A PokeParty swap index is invalid.");
        }

        PokeSlot slot = slots[index1];
        slots[index1] = slots[index2];
        slots[index2] = slot;
        UiUpdate();
    }

    public Pokemon GetPokemon(int id)
    {
        foreach (var slot in slots)
        {
           // if (slot.pokemon.PokedexNumber == id)
                return slot.pokemon;
        }

        return null;
    }



    //public string GetPokeSlotIcon(int index)
    //{
       // return GetPokeSlot(index).pokemon.iconName;
    //}
  
   // public int GetPokeSlotLevel(int index)
    //{
       // return GetPokeSlot(index).pokemon.level;
    //}
    //public Sprite GetPokemonSlotIconSprite(int index)
    //{
        //return GetPokeSlot(index).pokemon.Icon;
    //}
    public string GetPokeSlotName(int index)
    {
        return GetPokeSlot(index).pokemon.Name;
    }
    #endregion
}
   
public class PokeSlot
    
{
    public int index
    {
        get { return pokeParty.GetSlots().FindIndex(v => (v != null) ? v == this : false); }
        set { }
    } //Directly refer to List<> for the index
    public Pokemon pokemon;

    private PokeParty pokeParty;

    public PokeSlot(PokeParty pokeParty, Pokemon pokemon)
    {
        this.pokeParty = pokeParty;
        this.index = -1;
        this.pokemon = pokemon;
    }
    public PokeSlot(Pokemon pokemon)
    {          
        this.pokemon = pokemon;
    }
   
   
}

public class PokemonsInPt
{
    List<Pokemon> pokesInPt = new List<Pokemon>();
    public List<Pokemon> PokemonsInThePt { get { return pokesInPt; } set { pokesInPt = value; } }


    public PokemonsInPt()
    {
        Pokemon p1 = null;
        Pokemon p2 = null;
        Pokemon p3 = null;
        Pokemon p4 = null;
        Pokemon p5 = null;
        Pokemon p6 = null;
        pokesInPt.Add(p1);
        pokesInPt.Add(p2);
        pokesInPt.Add(p3);
        pokesInPt.Add(p4);
        pokesInPt.Add(p5);
        pokesInPt.Add(p6);
    }

    public bool CanAddPoke()
    {
        for(int i = 0; i < pokesInPt.Count;i++)
        {
            if(pokesInPt[i] == null)
            {
                return true;
            }
        }
        return false;
    }


}

   