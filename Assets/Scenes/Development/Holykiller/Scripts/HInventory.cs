using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class HInventory : MonoBehaviour
{
    #region GameObjets to show and hide
    
    private GameObject m_buttonPrefab;   
    
    
    //[SerializeField]
    //private Bottons m_SelectedItem;
    //public Bottons SelectedItem { get { return m_SelectedItem; } set { m_SelectedItem = value; } }

    #endregion
    #region Inventory Items
    // [SerializeField]
    private List<InventoryItem> _invItems = new List<InventoryItem>();


    private int generalitems = 0;
    private int pokeballs = 0;
    private int potions = 0;
    private int mttms = 0;
    private int berrys = 0;
    private int keyitems = 0;

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

    private float m_Money = 100000;
    public float Money { get { return m_Money; } set { m_Money = value; } }
   
    #endregion

    public static HInventory instance;
   

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
    }
    void Start()
    {
        m_buttonPrefab = (GameObject)Resources.Load("UI/ItemButton");
        NxtUiManager.instance.ShowMoney(m_Money);
      
    }
    
    /*
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


    }*/

    // [ContextMenu("test adding item")]
    // Using this to test adding items

    public void BuySelectedItem()
    {
        if ( NxtUiManager.instance.SelectedItem.ItemID != 0)
        {
            if (NxtUiManager.instance.ItemsDB.GetByID(NxtUiManager.instance.SelectedItem.ItemID).BuyingPrice <= m_Money && (m_Money - NxtUiManager.instance.ItemsDB.GetByID(NxtUiManager.instance.SelectedItem.ItemID).BuyingPrice) > -1)
           {
               NxtUiManager.instance.ShowItemsTab();
               NxtUiManager.instance.ShowTypeOfItemInventory(NxtUiManager.instance.ItemsDB.GetByID(NxtUiManager.instance.SelectedItem.ItemID).ItemType);

               AddItemWithID(NxtUiManager.instance.SelectedItem.ItemID);
              if(m_ItemAdded == true)
                  m_Money -= NxtUiManager.instance.ItemsDB.GetByID(NxtUiManager.instance.SelectedItem.ItemID).BuyingPrice;
              NxtUiManager.instance.ShowMoney(m_Money);
              ThisCapasitySize();
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
                NxtUiManager.instance.ShowMoney(m_Money);
                //ThisCapasitySize();
                if(_invItems[i].StacksAtm > 0)
                {
                    _invItems[i].StacksAtm--;
                    for (int x = 0; x < BottonsManager.instance._bottons.Count; x++)
                    {
                        if (BottonsManager.instance._bottons[x].ItemID == itemID && BottonsManager.instance._bottons[x].StacksAtm > 0)
                        {
                            BottonsManager.instance._bottons[x].Amount(_invItems[i].StacksAtm, _invItems[i].StacksUpTo);
                            m_ItemAdded = true;
                            ThisCapasitySize();
                            if(_invItems[i].StacksAtm == 0)
                            {
                                _invItems.Remove(_invItems[i]);                               
                                Debug.Log("RemoveItem");
                                DestroyImmediate(BottonsManager.instance._bottons[x].gameObject);
                                
                                ThisCapasitySize();
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
    
    
    private void AddItemWithID(int x)
    {
        //each time we make a new item we create a new item instance...
        InventoryItem item = new InventoryItem(NxtUiManager.instance.ItemsDB.GetByID(x));
        AddItemToInventory(item);
    }
    //We call this to add items to the inventory
    public void AddItemToInventory(InventoryItem item)
    {
        LootAtArrayOf(item);
        ThisCapasitySize();

    }
    //Here we check if there is space in the Inventory Slot that the item that we want to add
    private void LootAtArrayOf(InventoryItem item)
    {
        CheckInvStatus();
        NxtUiManager.instance.ShowTypeOfItemInventory(item.ItemType);
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
        if (m_buttonPrefab == null)
        {
            Debug.Log("Add Botton Prefab its at Resources/UI/ItemBotton");
            return;
        }
        GameObject itemToMake = (GameObject)Instantiate(m_buttonPrefab);
        GameObject parent = null;
        switch (item.ItemType)
        {
            case ItemType.GeneralItem:
                parent =  NxtUiManager.instance.ItemsButtonsPanel;//itemsButtonsPanel;
                break;
            case ItemType.Pokeball:
                parent = NxtUiManager.instance.PokeballsButtonsPanel;//  _pokeballsButtonsPanel;
                break;
            case ItemType.Potion:
                parent = NxtUiManager.instance.PotionButtonsPanel;// _potionButtonsPanel;
                break;
            case ItemType.MtTm:
                parent = NxtUiManager.instance.MtsTmsButtonsPanel;// _mtsTmsButtonsPanel;
                break;
            case ItemType.Berry:
                parent = NxtUiManager.instance.BerrysButtonsPanel;// _berrysButtonsPanel;
                break;
            case ItemType.KeyItem:
                parent = NxtUiManager.instance.KeyItemsButtonsPanel;// _keyItemsButtonsPanel;
                break;
        }
        if (parent != null)
        {
            Bottons b = itemToMake.GetComponent<Bottons>();
            b.BotonInfo(item.Icon, item.Name, item.StacksAtm, item.StacksUpTo, item.Description, item.ID,item.SellingPrice,true);
            itemToMake.transform.SetParent(parent.transform);
            ThisCapasitySize();
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
    
    public void ThisCapasitySize()
    {
        CheckInvStatus();            
        if (NxtUiManager.instance.ItemsPanel.activeSelf == true)            
        {
            NxtUiManager.instance.CapasitySize(generalitems, _itemsInventorySize);           
        }
        if (NxtUiManager.instance.PokeballsPanel.activeSelf == true)
        {
            NxtUiManager.instance.CapasitySize(pokeballs , _pokeballsInventorySize);
        }
        if (NxtUiManager.instance.PotionPanel.activeSelf == true)
        {
            NxtUiManager.instance.CapasitySize(potions , _potionsInventorySize);
        }
        if (NxtUiManager.instance.MtsTmsPanel.activeSelf == true)
        {
           NxtUiManager.instance.CapasitySize(mttms, _mtsMtsInventorySize);
        }
        if (NxtUiManager.instance.BerrysPanel.activeSelf == true)
        {
            NxtUiManager.instance.CapasitySize(berrys, _berrysInventorySize);
        }

        if (NxtUiManager.instance.KeyItemsPanel.activeSelf == true)
        {
            NxtUiManager.instance.CapasitySize(keyitems , _keyItemsInventorySize);
        }
        NxtUiManager.instance.ShowSellingPrice();
        CheckInvStatus();    
    }
    
   
}
