using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class NxtUiManager : MonoBehaviour 
{



    public GameObject ItemsUI;//{get;set;}
    public GameObject PokemonsUI;//{get;set;}
    public GameObject PlayerUI;//{get;set;}
    [Header("Top Tabs(Items,Pokemons,Player")]


    //[SerializeField]
    //private Sprite[] PokemonIcons;

    [Header("Inventory")]
    [SerializeField]
    private GameObject _inventoryUI;
    public GameObject InventoryUI { get { return _inventoryUI; } set { _inventoryUI = value; } }

    [SerializeField]
    private Text _inventoryCapasity;
    public Text InventoryCapasity { get { return _inventoryCapasity; } set { _inventoryCapasity = value; } }

    [Header("Inventory Tabs")]
    [SerializeField]
    private GameObject _itemsPanel;
    public GameObject ItemsPanel { get { return _itemsPanel; } set { _itemsPanel = value; } }
    [SerializeField]
    private GameObject _itemsButtonsPanel;
    public GameObject ItemsButtonsPanel { get { return _itemsButtonsPanel; } set { _itemsButtonsPanel = value; } }
    [SerializeField]
    private GameObject _pokeballsPanel;
    public GameObject PokeballsPanel { get { return _pokeballsPanel; } set { _pokeballsPanel = value; } }
    [SerializeField]
    private GameObject _pokeballsButtonsPanel;
    public GameObject PokeballsButtonsPanel { get { return _pokeballsButtonsPanel; } set { _pokeballsButtonsPanel = value; } }
    [SerializeField]
    private GameObject _potionPanel;
    public GameObject PotionPanel { get { return _potionPanel; } set { _potionPanel = value; } }
    [SerializeField]
    private GameObject _potionButtonsPanel;
    public GameObject PotionButtonsPanel { get { return _potionButtonsPanel; } set { _potionButtonsPanel = value; } }
    [SerializeField]
    private GameObject _mtsTmsPanel;
    public GameObject MtsTmsPanel { get { return _mtsTmsPanel; } set { _mtsTmsPanel = value; } }
    [SerializeField]
    private GameObject _mtsTmsButtonsPanel;
    public GameObject MtsTmsButtonsPanel { get { return _mtsTmsButtonsPanel; } set { _mtsTmsButtonsPanel = value; } }
    [SerializeField]
    private GameObject _berrysPanel;
    public GameObject BerrysPanel { get { return _berrysPanel; } set { _berrysPanel = value; } }
    [SerializeField]
    private GameObject _berrysButtonsPanel;
    public GameObject BerrysButtonsPanel { get { return _berrysButtonsPanel; } set { _berrysButtonsPanel = value; } }
    [SerializeField]
    private GameObject _keyItemsPanel;
    public GameObject KeyItemsPanel { get { return _keyItemsPanel; } set { _keyItemsPanel = value; } }
    [SerializeField]
    private GameObject _keyItemsButtonsPanel;
    public GameObject KeyItemsButtonsPanel { get { return _keyItemsButtonsPanel; } set { _keyItemsButtonsPanel = value; } }

    [SerializeField]
    private Text m_moneytext;
    [SerializeField]
    private Text m_playerMoneyText;
    public static NxtUiManager instance;

    [Header("Pokemons Tabs")]
    [SerializeField]
    private GameObject PokemonStatusUI;
    [SerializeField]
    private GameObject PokemonSkillsUI;
    [Header("Store")]
    [SerializeField]
    private Bottons m_SelectedItem;
    public Bottons SelectedItem { get { return m_SelectedItem; } set { m_SelectedItem = value; } }
    [SerializeField]
    private GameObject ShopUI;
    [Header("Pokemons Icons In Pt")]
    [SerializeField]
    private List<PokeUI> PokemonsInPtIcons = new List<PokeUI>();
    public List<PokeUI> PokemonsInPtUI { get { return PokemonsInPtIcons; } set { PokemonsInPtIcons = value; } }
    [Header("Pokemons Status")]    
    [SerializeField]
    private Text m_pokeName;
    [SerializeField]
    private Text m_pokeNick;
    [SerializeField]
    private Text m_pokelevel;
    [SerializeField]
    private Text m_pokeExpToLevel;
    [SerializeField]
    private Text m_pokeStatus;
    [SerializeField]
    private Text m_pokeType1;
    [SerializeField]
    private Text m_pokeType2;
    [SerializeField]
    private Text m_pokeHealth;
    [SerializeField]
    private Text m_pokeAttack;
    [SerializeField]
    private Text m_pokeDef;
    [SerializeField]
    private Text m_pokePP;
    [SerializeField]
    private Text m_pokeSpeed;
    [Header("General UI")]
    [SerializeField]
    private List<GPokeUi> m_gpokeUis = new List<GPokeUi>();

    [SerializeField]
    private ItemAssetDatabase db;
    public ItemAssetDatabase DB { get { return db; }/* set { db = value; }*/ }

    [SerializeField]
    PokeAssetDatabase PokeDB;
    //[SerializeField]
    //MoveAssetDatabase MovesDB;

    private GameObject player;

    public PokeParty PlayerPokePt { get; set; }

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
	void Start () 
    {

        
        player = GameObject.FindGameObjectWithTag("Player");
        NXT.EventHandler.RegisterEvent(player, "ShowInventory", new Action(ShowInventory));


        /*
        PokeUI poke1 = GameObject.Find("PokemonPt1").GetComponent<PokeUI>();
        PokeUI poke2 = GameObject.Find("PokemonPt2").GetComponent<PokeUI>();
        PokeUI poke3 = GameObject.Find("PokemonPt3").GetComponent<PokeUI>();
        PokeUI poke4 = GameObject.Find("PokemonPt4").GetComponent<PokeUI>();
        PokeUI poke5 = GameObject.Find("PokemonPt5").GetComponent<PokeUI>();
        PokeUI poke6 = GameObject.Find("PokemonPt6").GetComponent<PokeUI>();
        PokemonsInPtIcons.Add(poke1);
        PokemonsInPtIcons.Add(poke2);
        PokemonsInPtIcons.Add(poke3);
        PokemonsInPtIcons.Add(poke4);
        PokemonsInPtIcons.Add(poke5);
        PokemonsInPtIcons.Add(poke6);
         * */

	}
    public void ShowMoney(float Money)
    {
        if (m_moneytext == null || m_playerMoneyText == null)
        {
            Debug.Log("Add Money Text and Player Money Text");
            return;
        }
        m_moneytext.text = "$" + Money;
        m_playerMoneyText.text = "$" + Money;
    }

    private void ShowInventory()
    {
        InventoryUI.SetActive(!InventoryUI.activeSelf);
    }
    public bool ShopIsOpen()
    {
        bool itsOpen = false;
        if (ShopUI.activeSelf == true)
        {
            itsOpen = true;
        }
        return itsOpen;
    }

    public void ShowSellingPrice()
    {
        if (ShopUI.activeSelf == true)
        {
            for (int x = 0; x < BottonsManager.instance._bottons.Count; x++)
            {
                if (BottonsManager.instance._bottons[x].ItOWnByPlayer == true)
                {
                    BottonsManager.instance._bottons[x].ShowSellingText(true);
                }
            }
        }
        else
        {
            for (int x = 0; x < BottonsManager.instance._bottons.Count; x++)
            {
                if (BottonsManager.instance._bottons[x].ItOWnByPlayer == true)
                {
                    BottonsManager.instance._bottons[x].ShowSellingText(false);
                }
            }
        }

    }
    public void HideSelectedItemInfo()
    {
        m_SelectedItem.BDisable();
    }

    //This takes care of showing what to show by enableling and disableling all but what we realy want to see
    #region InventorySwitchs
    public void ShowTypeOfItemInventory(ItemType type)
    {
        switch (type)
        {
            case ItemType.GeneralItem:
                ShowItems();
                break;
            case ItemType.Pokeball:
                ShowPokeballs();
                break;
            case ItemType.Potion:
                ShowPotions();
                break;
            case ItemType.MtTm:
                ShowMtsTms();
                break;
            case ItemType.Berry:
                ShowBerrys();
                break;
            case ItemType.KeyItem:
                ShowKeyItems();
                break;
        }
    }
    
    

    public void ShowItems()
    {
        _itemsPanel.SetActive(true);
        _pokeballsPanel.SetActive(false);
        _potionPanel.SetActive(false);
        _mtsTmsPanel.SetActive(false);
        _berrysPanel.SetActive(false);
        _keyItemsPanel.SetActive(false);
        HInventory.instance.ThisCapasitySize();

    }
    public void ShowPokeballs()
    {
        _itemsPanel.SetActive(false);
        _pokeballsPanel.SetActive(true);
        _potionPanel.SetActive(false);
        _mtsTmsPanel.SetActive(false);
        _berrysPanel.SetActive(false);
        _keyItemsPanel.SetActive(false);
        HInventory.instance.ThisCapasitySize();
    }
    public void ShowPotions()
    {
        _itemsPanel.SetActive(false);
        _pokeballsPanel.SetActive(false);
        _potionPanel.SetActive(true);
        _mtsTmsPanel.SetActive(false);
        _berrysPanel.SetActive(false);
        _keyItemsPanel.SetActive(false);
        HInventory.instance.ThisCapasitySize();
    }
    public void ShowMtsTms()
    {
        _itemsPanel.SetActive(false);
        _pokeballsPanel.SetActive(false);
        _potionPanel.SetActive(false);
        _mtsTmsPanel.SetActive(true);
        _berrysPanel.SetActive(false);
        _keyItemsPanel.SetActive(false);
        HInventory.instance.ThisCapasitySize();
    }
    public void ShowBerrys()
    {
        _itemsPanel.SetActive(false);
        _pokeballsPanel.SetActive(false);
        _potionPanel.SetActive(false);
        _mtsTmsPanel.SetActive(false);
        _berrysPanel.SetActive(true);
        _keyItemsPanel.SetActive(false);
        HInventory.instance.ThisCapasitySize();
    }
    public void ShowKeyItems()
    {
        _itemsPanel.SetActive(false);
        _pokeballsPanel.SetActive(false);
        _potionPanel.SetActive(false);
        _mtsTmsPanel.SetActive(false);
        _berrysPanel.SetActive(false);
        _keyItemsPanel.SetActive(true);
        HInventory.instance.ThisCapasitySize();
    }
    public void ShowPokemonStatus()
    {
        PokemonStatusUI.SetActive(true);
        PokemonSkillsUI.SetActive(false);
    }
    public void ShowPokemonSkills()
    {
        PokemonStatusUI.SetActive(false);
        PokemonSkillsUI.SetActive(true);
    }
    public void ShowPokemonsTab()
    {
        ItemsUI.SetActive(false);
        PokemonsUI.SetActive(true);
        PlayerUI.SetActive(false);
    }
    public void ShowPlayerTab()
    {
        ItemsUI.SetActive(false);
        PokemonsUI.SetActive(false);
        PlayerUI.SetActive(true);
    }
    public void ShowItemsTab()
    {
        ItemsUI.SetActive(true);
        PokemonsUI.SetActive(false);
        PlayerUI.SetActive(false);
        HInventory.instance.ThisCapasitySize();
    }

    public void CapasitySize( float amount , float total)
    {
        _inventoryCapasity.text = amount + " / " + total;
       
        ShowSellingPrice();
    }
	
	#endregion

    public void ShowCurPokemonStatus(string name,int level,float curexp,float exptolevels,string status,string type1,string type2,float HP,float attack,float Def,float PP,float speed)
    {
        m_pokeName.text = "Name :" + name;
        m_pokelevel.text = "Level : " + level.ToString();
        m_pokeExpToLevel.text = "Exp : " + curexp.ToString() + " / " + exptolevels.ToString();
        m_pokeStatus.text = "Status :" + status;
        m_pokeType1.text = "Type1 : " + type1;
        m_pokeType2.text = "Type2 : " + type2;
        m_pokeHealth.text = "Health : " + HP.ToString();
        m_pokeAttack.text = "Attack : " + attack.ToString();
        m_pokeDef.text = "Defence : " + Def.ToString();
        m_pokePP.text = "PP : " + PP.ToString();
        m_pokeSpeed.text = "Speed : " + speed.ToString();
    }

    public void SetCurPokesUis(List<PokeSlot> slots)
    {
        for(int i = 0 ; i < slots.Count ;i++)
        {
            PokemonsInPtIcons[i].SetThisPokemonIconAndName(slots[i].pokemon);
            
        }
        for(int x = 0; x < slots.Count;x++)
        {
            if(slots[x].pokemon != null)
            {
                m_gpokeUis[x].gameObject.SetActive(true);
                m_gpokeUis[x].SetValues(slots[x].pokemon);
            }
            else
            {
                m_gpokeUis[x].gameObject.SetActive(false);
            }
            
        }
       // PokemonsInPtIcons
    }
    public void CurSelectedPoke(int index)
    {
        for(int i = 0;i < m_gpokeUis.Count;i++)
        {
            m_gpokeUis[i].SelectedisThis(false);
            if(i == index)
            {
                m_gpokeUis[i].SelectedisThis(true);
            }
        }
    }

    public Pokemon NewPoke(int id)
    {
       

        Pokemon poke = new Pokemon();
        //PokeAssetDatabase pokeAssetDatabase = (PokeAssetDatabase)AssetDatabase.LoadAssetAtPath("Assets/Database/PokemonAssetDatabase.asset", typeof(PokeAssetDatabase));
        //PokeAssetDatabase pokeAssetDatabase = ScriptableObject.CreateInstance("PokeAssetDatabase") as PokeAssetDatabase;
        // PokeAssetDatabase pokeAssetDatabase = (PokeAssetDatabase)Resources.Load("Database/PokemonAssetDatabase") as PokeAssetDatabase;
        // Pokemon poke =  new Pokemon( pokeAssetDatabase.GetByID(id));
        poke.ID = PokeDB.GetByID(id).ID;
        poke.Name = PokeDB.GetByID(id).Name;
        poke.PP = PokeDB.GetByID(id).PP;
        poke.Icon = PokeDB.GetByID(id).Icon;
        poke.Type1 = PokeDB.GetByID(id).Type1;
        poke.Type2 = PokeDB.GetByID(id).Type2;
        poke.PokemonPrefab = PokeDB.GetByID(id).PokemonPrefab;
        poke.Moves = PokeDB.GetByID(id).Moves;
        poke.LearnMovesLevels = PokeDB.GetByID(id).LearnMovesLevels;

        return poke;
    }
}
