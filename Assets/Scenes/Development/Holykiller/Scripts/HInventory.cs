using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

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
    // [SerializeField]
    public List<InventoryItem> _invPokeballs = new List<InventoryItem>();
    // [SerializeField]
    public List<InventoryItem> _invPotions = new List<InventoryItem>();
    // [SerializeField]
    public List<InventoryItem> _invMtsTms = new List<InventoryItem>();
    // [SerializeField]
    public List<InventoryItem> _invBerrys = new List<InventoryItem>();
    // [SerializeField]
    public List<InventoryItem> _invKeyItems = new List<InventoryItem>();

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


    private float m_Money = 1000;
    public float Money { get { return m_Money; } set { m_Money = value; } }
    #endregion

    public static HInventory instance;

    void Awake()
    {
        if (instance = null)
        {
            instance = this;
        }
        ShowMoney();
    }

    void Update()
    {
        //Shows and hide the inventory window if press I
        if (Input.GetKeyUp(KeyCode.I))
        {
            if (InventoryUI != null)
            {
                InventoryUI.SetActive(!InventoryUI.activeSelf);
            }
        }
        //Testing Adding Items
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            addtest(10);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            addtest(101);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            addtest(5);
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            addtest(400);
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            addtest(500);
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            addtest(600);
        }


    }

    // [ContextMenu("test adding item")]
    // Using this to test adding items
    public void addtest(int x)
    {
        //each time we make a new item we create a new item instance... but im not sure if this will create garbage...
        InventoryItem item = new InventoryItem(db.GetByID(x));
        AddItemToInventory(item);
        //Maybe its better if we do this so we dont make new items each time anyways the AddANewItem() makes a new item and boton after it check its ItemType
        //AddItemToInventory(db.GetByID(x));
    }
    //We call this to add items to the inventory
    public void AddItemToInventory(InventoryItem item)
    {
        switch (item.ItemType)
        {
            case ItemType.GeneralItem:
                LootAtArrayOf(_invItems, _itemsInventorySize, item);
                break;
            case ItemType.Pokeball:
                LootAtArrayOf(_invPokeballs, _pokeballsInventorySize, item);
                break;
            case ItemType.Potion:
                LootAtArrayOf(_invPotions, _potionsInventorySize, item);
                // Debug.Log("It Was a potion and the array have " + _invPotions.Count );
                break;
            case ItemType.MtTm:
                LootAtArrayOf(_invMtsTms, _mtsMtsInventorySize, item);
                break;
            case ItemType.Berry:
                LootAtArrayOf(_invBerrys, _berrysInventorySize, item);
                break;
            case ItemType.KeyItem:
                LootAtArrayOf(_invKeyItems, _keyItemsInventorySize, item);
                break;
        }
        CapasitySize();

    }
    //Here we check if there is space in the Inventory Slot that the item that we want to add
    private void LootAtArrayOf(List<InventoryItem> items, int limit, InventoryItem item)
    {
        // Debug.Log("Looking at the list");
        if (items.Count >= limit)
        {
            int allitems = items.Count;
            for (int asd = 0; asd < items.Count; asd++)
            {
                if (items[asd].StacksAtm == items[asd].StacksUpTo)
                {
                    allitems--;
                }

            }
            if (allitems == 0)
            {
                Debug.Log("Inventory full");
                return;
            }

        }
        if (items.Count == 0)
        {
            //   Debug.Log("The list was empty so make a new");

            items.Add(item);
            AddANewItem(item);
            return;
        }
        //this look if there is another item like the one we want to add and if there is we just increase the stack count 
        for (int asd = 0; asd < items.Count; asd++)
        {

            if (items[asd].ID == item.ID && items[asd].StacksAtm < items[asd].StacksUpTo)
            {
                items[asd].StacksAtm++;
                //this update the UI text for the amount of items that are in the inventory
                for (int i = 0; i < BottonsManager.instance._bottons.Count; i++)
                {
                    if (BottonsManager.instance._bottons[i].TheName() == item.Name && BottonsManager.instance._bottons[i].StacksAtm < BottonsManager.instance._bottons[i].MaxStacks)
                    {
                        BottonsManager.instance._bottons[i].Amount(items[asd].StacksAtm, items[asd].StacksUpTo);
                        return;
                    }
                }
                return;
            }
        }
        //if there was another item like the one we want to add but its stack is the same as its max amount that it can stack,so we create a new item 
        //and added to the corresponding array and make a new boton and add it to its tab depending of the itemtype of the new item
        if (items.Count < limit)
        {
            items.Add(item);
            AddANewItem(item);
        }
        else
            Debug.Log("cant add more items");


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
            b.BotonInfo(item.icon, item.Name, item.StacksAtm, item.StacksUpTo, item.Description, item.ID);
            itemToMake.transform.SetParent(parent.transform);
            CapasitySize();
            // Debug.Log("Created new item");
        }
    }
    //Shows the current size of the inventory tab selected(items,pokeballs.potions...)
    private void CapasitySize()
    {
        if (_itemsPanel.activeSelf == true)
        {
            InventoryCapasity.text = _invItems.Count + " / " + _itemsInventorySize;
        }
        if (_pokeballsPanel.activeSelf == true)
        {
            InventoryCapasity.text = _invPokeballs.Count + " / " + _pokeballsInventorySize;
        }
        if (_potionPanel.activeSelf == true)
        {
            InventoryCapasity.text = _invPotions.Count + " / " + _potionsInventorySize;
        }
        if (_mtsTmsPanel.activeSelf == true)
        {
            InventoryCapasity.text = _invMtsTms.Count + " / " + _mtsMtsInventorySize;
        }
        if (_berrysPanel.activeSelf == true)
        {
            InventoryCapasity.text = _invBerrys.Count + " / " + _berrysInventorySize;
        }

        if (_keyItemsPanel.activeSelf == true)
        {
            InventoryCapasity.text = _invKeyItems.Count + " / " + _keyItemsInventorySize;
        }

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

    #region References

    public void SetValue()
    {

    }

    #endregion
}
