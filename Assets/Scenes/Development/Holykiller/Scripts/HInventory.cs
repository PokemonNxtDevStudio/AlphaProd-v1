using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class HInventory : MonoBehaviour
{
    #region GameObjets to show and hide
    [SerializeField]
    private GameObject Botton;
    [Header("Top Tabs(Items,Pokemons,Player")]
    [SerializeField]
    private GameObject ItemsUI;
    [SerializeField]
    private GameObject PokemonsUI;
    [SerializeField]
    private GameObject PlayerUI;

    //[SerializeField]
    //private Sprite[] PokemonIcons;

    [Header("Inventory")]
    [SerializeField]
    public GameObject InventoryUI;
    [SerializeField]
    private Text InventoryCapasity;
    [Header("Inventory Tabs")]
    [SerializeField]
    private GameObject _itemsPanel;
    [SerializeField]
    private GameObject _itemsButtonsPanel;
    [SerializeField]
    private GameObject _pokeballsPanel;
    [SerializeField]
    private GameObject _pokeballsButtonsPanel;
    [SerializeField]
    private GameObject _potionPanel;
    [SerializeField]
    private GameObject _potionButtonsPanel;
    [SerializeField]
    private GameObject _mtsTmsPanel;
    [SerializeField]
    private GameObject _mtsTmsButtonsPanel;
    [SerializeField]
    private GameObject _berrysPanel;
    [SerializeField]
    private GameObject _berrysButtonsPanel;
    [SerializeField]
    private GameObject _keyItemsPanel;
    [SerializeField]
    private GameObject _keyItemsButtonsPanel;

    [SerializeField]
    private Text m_moneytext;
    [SerializeField]
    private Text m_playerMoneyText;
    [Header("Store")]
    [SerializeField]
    private Bottons m_SelectedItem;
    public Bottons SelectedItem { get { return m_SelectedItem; } set { m_SelectedItem = value; } }
    [SerializeField]
    private GameObject ShopUI;


    [Header("Pokemons Tabs")]
    [SerializeField]
    private GameObject PokemonStatusUI;
    [SerializeField]
    private GameObject PokemonSkillsUI;
    [SerializeField]
    private ItemAssetDatabase db;
    [Header("References")]
    //[SerializeField]
    //private Bottons m_SelectedItem;
    //public Bottons SelectedItem { get { return m_SelectedItem; } set { m_SelectedItem = value; } }

    #endregion
    #region Inventory Items
    // [SerializeField]
    public List<InventoryItem> _invItems = new List<InventoryItem>();


    private int generalitems = 0;
    private int pokeballs = 0;
    private int potions = 0;
    private int mttms = 0;
    private int berrys = 0;
    private int keyitems = 0;
    // [SerializeField]
  //  public List<InventoryItem> _invPokeballs = new List<InventoryItem>();
    // [SerializeField]
   // public List<InventoryItem> _invPotions = new List<InventoryItem>();
    // [SerializeField]
   // public List<InventoryItem> _invMtsTms = new List<InventoryItem>();
    // [SerializeField]
    //public List<InventoryItem> _invBerrys = new List<InventoryItem>();
    // [SerializeField]
   // public List<InventoryItem> _invKeyItems = new List<InventoryItem>();

    private int _itemsInventorySize = 10;
    public int ItemISize { get { return _itemsInventorySize; } set { _itemsInventorySize = value; } }
    private int _pokeballsInventorySize = 10;
    public int PokeballsInvSize { get { return _pokeballsInventorySize; } set { _pokeballsInventorySize = value; } }
    private int _potionsInventorySize = 10;
    public int PotionsInvSize { get { return _potionsInventorySize; } set { _potionsInventorySize = value; } }
    private int _mtsMtsInventorySize = 10;
    public int MtsTmsInvSize { get { return _mtsMtsInventorySize; } set { _mtsMtsInventorySize = value; } }
    private int _berrysInventorySize = 10;
    public int BerrysInvSize { get { return _berrysInventorySize; } set { _berrysInventorySize = value; } }
    private int _keyItemsInventorySize = 10;
    public int KeyItemsInvSize { get { return _keyItemsInventorySize; } set { _keyItemsInventorySize = value; } }

    private bool m_spaceFree = true;
    private bool m_ItemAdded = false;
    private float m_Money = 1000;
    public float Money { get { return m_Money; } set { m_Money = value; } }
    #endregion

    public static HInventory instance;

    private GameObject player;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
    }
    void Start()
    {
        ShowMoney();
        player = GameObject.FindGameObjectWithTag("Player");
        NXT.EventHandler.RegisterEvent(player, "ShowInventory", new Action(ShowInventory));

    }
    private void ShowInventory()
    {
        InventoryUI.SetActive(!InventoryUI.activeSelf);
    }
    
    void Update()
    {
        //Testing Adding Items
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            AddItemWithID(1);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            AddItemWithID(2);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            AddItemWithID(8);
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            AddItemWithID(9);
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            AddItemWithID(300);
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            AddItemWithID(301);
        }


    }

    // [ContextMenu("test adding item")]
    // Using this to test adding items

    public void BuySelectedItem()
    {
        if (m_SelectedItem.ItemID != 0)
        {
           if(db.GetByID(m_SelectedItem.ItemID).BuyingPrice <= m_Money && (m_Money - db.GetByID(m_SelectedItem.ItemID).BuyingPrice) > -1)
           {
               ShowTypeOfItemInventory(db.GetByID(m_SelectedItem.ItemID).ItemType);
              
               AddItemWithID(m_SelectedItem.ItemID);
              if(m_ItemAdded == true)
               m_Money -= db.GetByID(m_SelectedItem.ItemID).BuyingPrice;
               ShowMoney();
           }
           else
           {

               ShowNotEnoughMoney();
           }
        }       
        else
        {
            Debug.Log("ID of the item Is 0");
        }
    }
    private void ShowNotEnoughMoney()
    {
        Debug.Log("Not Enought Money To Buy Item");
    }
    public bool ShopIsOpen()
    {
        bool itsOpen = false;
        if(ShopUI.activeSelf == true)
        {
            itsOpen = true;
        }
        return itsOpen;
    }

    public void SellItem(int itemID)
    {
        bool have = false;
        for(int i = 0; i < _invItems.Count;i++)
        {
            if(_invItems[i].ID == itemID)
            {
                have = true;
            }
            if(have)
            {
                m_Money += _invItems[i].SellingPrice;
                ShowMoney();
                if(_invItems[i].StacksAtm > 0)
                {
                    _invItems[i].StacksAtm--;
                    for (int x = 0; x < BottonsManager.instance._bottons.Count; x++)
                    {
                        if (BottonsManager.instance._bottons[x].ItemID == itemID && BottonsManager.instance._bottons[x].StacksAtm > 0)
                        {
                            BottonsManager.instance._bottons[x].Amount(_invItems[i].StacksAtm, _invItems[i].StacksUpTo);
                            m_ItemAdded = true;
                            if(_invItems[i].StacksAtm == 0)
                            {
                                Debug.Log("RemoveItem");
                                DestroyImmediate(BottonsManager.instance._bottons[x].gameObject);
                            }
                            
                            return;
                        }
                    }
                   /* if(_invItems[i].StacksAtm == 0)
                    {
                        Debug.Log("RemoveItem");
                        _invItems.Remove(_invItems[i]);
                        for (int x = 0; x < BottonsManager.instance._bottons.Count; x++)
                        {
                            if (BottonsManager.instance._bottons[x].ItemID == itemID && BottonsManager.instance._bottons[x].StacksAtm == 0)
                            {
                                DestroyImmediate(BottonsManager.instance._bottons[x].gameObject);
                                return;
                            }
                        }
                    }*/
                }
                else
                {
                    Debug.Log("Item NOT EXISTING");

                }
            }
        }
        
        if(!have)
        {
            Debug.Log("Item Not Exist in Inventory");
        }
    }
    public void ShowSellingPrice()
    {        
        if(ShopUI.activeSelf == true)
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
    private void AddItemWithID(int x)
    {
        //each time we make a new item we create a new item instance...
        InventoryItem item = new InventoryItem(db.GetByID(x));
        AddItemToInventory(item);
    }
    //We call this to add items to the inventory
    public void AddItemToInventory(InventoryItem item)
    {
        LootAtArrayOf(item);
        CapasitySize();

    }
    //Here we check if there is space in the Inventory Slot that the item that we want to add
    private void LootAtArrayOf(InventoryItem item)
    {
        CheckInvStatus();
        ShowTypeOfItemInventory(item.ItemType);
        //Check if there a free space in the inventory even if it is in a stackable one 
        m_spaceFree = true;
        if (item.ItemType == ItemType.GeneralItem && generalitems >= _itemsInventorySize)
        {
            LookForSpace(item);            
        }
         if( item.ItemType == ItemType.Pokeball && pokeballs >= _pokeballsInventorySize)
        {
            LookForSpace(item);
        }
         if (item.ItemType == ItemType.Potion && potions >= _potionsInventorySize)
        {
            LookForSpace(item);
        }
         if(item.ItemType == ItemType.MtTm && mttms >= _mtsMtsInventorySize)
        {
            LookForSpace(item);
        }
         if(item.ItemType == ItemType.Berry && berrys >= _berrysInventorySize)
        {
            LookForSpace(item);
        }
        if(item.ItemType == ItemType.KeyItem && keyitems >= _keyItemsInventorySize)
        {

            LookForSpace(item);
        }
        if (m_spaceFree == false)
        {
            Debug.Log("No More Space for that item in the inventory");
            m_ItemAdded = false;
            return;
        }
        else
        {
            //Check if ther is a space in a stackable item
            for (int i = 0; i < _invItems.Count; i++)
            {
                if (_invItems[i].ID == item.ID && _invItems[i].StacksAtm < _invItems[i].StacksUpTo)
                {
                    _invItems[i].StacksAtm++;
                    for (int x = 0; x < BottonsManager.instance._bottons.Count; x++)
                    {
                        if (BottonsManager.instance._bottons[x].ItemID == item.ID && BottonsManager.instance._bottons[x].StacksAtm < BottonsManager.instance._bottons[x].MaxStacks)
                        {
                            BottonsManager.instance._bottons[x].Amount(_invItems[i].StacksAtm, _invItems[i].StacksUpTo);
                            m_ItemAdded = true;
                            return;
                        }
                    }
                    return;
                }
            }

            _invItems.Add(item);
            AddANewItem(item);
            m_ItemAdded = true;
        }
    }

    private void LookForSpace(InventoryItem item)
    {

        int allitems = 0;
        for (int i = 0; i < _invItems.Count;i++ )
        {
            if(_invItems[i].ID == item.ID)
            {
                allitems++;
            }
        }
            for (int asd = 0; asd < _invItems.Count; asd++)
            {
                if (_invItems[asd].StacksAtm >= _invItems[asd].StacksUpTo && _invItems[asd].ID == item.ID)
                {
                    allitems--;
                }
            }
        if (allitems == 0)
        {
            m_spaceFree = false;
        }
        
    }

    //Here we create the new buttons and set its values and added it to the correct Inventory Slots(items,pokeballs,potions....)
    private void AddANewItem(InventoryItem item)
    {
        // Debug.Log("making new boton");
        if (Botton == null)
        {
            Debug.Log("Add Botton Prefab its at Resources/UI/ItemBotton");
            return;
        }
        GameObject itemToMake = (GameObject)Instantiate(Botton);
        GameObject parent = null;
        switch (item.ItemType)
        {
            case ItemType.GeneralItem:
                parent = _itemsButtonsPanel;
                break;
            case ItemType.Pokeball:
                parent = _pokeballsButtonsPanel;
                break;
            case ItemType.Potion:
                parent = _potionButtonsPanel;
                break;
            case ItemType.MtTm:
                parent = _mtsTmsButtonsPanel;
                break;
            case ItemType.Berry:
                parent = _berrysButtonsPanel;
                break;
            case ItemType.KeyItem:
                parent = _keyItemsButtonsPanel;
                break;
        }
        if (parent != null)
        {
            Bottons b = itemToMake.GetComponent<Bottons>();
            b.BotonInfo(item.Icon, item.Name, item.StacksAtm, item.StacksUpTo, item.Description, item.ID,item.SellingPrice,true);
            itemToMake.transform.SetParent(parent.transform);
            CapasitySize();
            // Debug.Log("Created new item");
        }
    }
    //Shows the current size of the inventory tab selected(items,pokeballs.potions...)
    
    private void CheckInvStatus()
    {
        generalitems = 0;
        pokeballs = 0;
        potions = 0;
        mttms = 0;
        berrys = 0;
        keyitems = 0;

        for (int i = 0; i < _invItems.Count; i++)
        {
            if (_invItems[i].ItemType == ItemType.GeneralItem)
                generalitems++;
            if (_invItems[i].ItemType == ItemType.Pokeball)
                pokeballs++;
            if (_invItems[i].ItemType == ItemType.Potion)
                potions++;
            if (_invItems[i].ItemType == ItemType.MtTm)
                mttms++;
            if (_invItems[i].ItemType == ItemType.Berry)
                berrys++;
            if (_invItems[i].ItemType == ItemType.KeyItem)
                keyitems++;
        }
    }
    
    private void CapasitySize()
    {
        CheckInvStatus();            
        if (_itemsPanel.activeSelf == true)            
        {
            InventoryCapasity.text = generalitems + " / " + _itemsInventorySize;           
        }
        if (_pokeballsPanel.activeSelf == true)
        {
            InventoryCapasity.text = pokeballs + " / " + _pokeballsInventorySize;
        }
        if (_potionPanel.activeSelf == true)
        {
            InventoryCapasity.text = potions + " / " + _potionsInventorySize;
        }
        if (_mtsTmsPanel.activeSelf == true)
        {
            InventoryCapasity.text = mttms + " / " + _mtsMtsInventorySize;
        }
        if (_berrysPanel.activeSelf == true)
        {
            InventoryCapasity.text = berrys + " / " + _berrysInventorySize;
        }

        if (_keyItemsPanel.activeSelf == true)
        {
            InventoryCapasity.text = keyitems + " / " + _keyItemsInventorySize;
        }
        ShowSellingPrice();

    }
    private void ShowMoney()
    {
        if (m_moneytext == null || m_playerMoneyText == null)
        {
            Debug.Log("Add Money Text and Player Money Text");
            return;
        }
        m_moneytext.text = "$" + m_Money;
        m_playerMoneyText.text = "$" + m_Money;
    }
    //This takes care of showing what to show by enableling and disableling all but what we realy want to see
    #region InventorySwitchs
    private void ShowTypeOfItemInventory(ItemType type)
    {
        switch(type)
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
        CapasitySize();

    }
    public void ShowPokeballs()
    {
        _itemsPanel.SetActive(false);
        _pokeballsPanel.SetActive(true);
        _potionPanel.SetActive(false);
        _mtsTmsPanel.SetActive(false);
        _berrysPanel.SetActive(false);
        _keyItemsPanel.SetActive(false);
        CapasitySize();
    }
    public void ShowPotions()
    {
        _itemsPanel.SetActive(false);
        _pokeballsPanel.SetActive(false);
        _potionPanel.SetActive(true);
        _mtsTmsPanel.SetActive(false);
        _berrysPanel.SetActive(false);
        _keyItemsPanel.SetActive(false);
        CapasitySize();
    }
    public void ShowMtsTms()
    {
        _itemsPanel.SetActive(false);
        _pokeballsPanel.SetActive(false);
        _potionPanel.SetActive(false);
        _mtsTmsPanel.SetActive(true);
        _berrysPanel.SetActive(false);
        _keyItemsPanel.SetActive(false);
        CapasitySize();
    }
    public void ShowBerrys()
    {
        _itemsPanel.SetActive(false);
        _pokeballsPanel.SetActive(false);
        _potionPanel.SetActive(false);
        _mtsTmsPanel.SetActive(false);
        _berrysPanel.SetActive(true);
        _keyItemsPanel.SetActive(false);
        CapasitySize();
    }
    public void ShowKeyItems()
    {
        _itemsPanel.SetActive(false);
        _pokeballsPanel.SetActive(false);
        _potionPanel.SetActive(false);
        _mtsTmsPanel.SetActive(false);
        _berrysPanel.SetActive(false);
        _keyItemsPanel.SetActive(true);
        CapasitySize();
    }
    public void ShowItemsTab()
    {
        ItemsUI.SetActive(true);
        PokemonsUI.SetActive(false);
        PlayerUI.SetActive(false);
        CapasitySize();
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
    #endregion
}
