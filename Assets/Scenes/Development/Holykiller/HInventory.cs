using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using NXT;
using NXT.Inventory;

public class HInventory : MonoBehaviour 
{
    [SerializeField]
    private Sprite[] PokemonIcons;

    [SerializeField]
    private GameObject InventoryUI;
    [SerializeField]
    private Text InventoryCapasity;

    [SerializeField]
    private GameObject _itemsPanel;
    [SerializeField]
    private GameObject _pokeballsPanel;
    [SerializeField]
    private GameObject _potionPanel;
    [SerializeField]
    private GameObject _mtsTmsPanel;
    [SerializeField]
    private GameObject _berrysPanel;
    [SerializeField]
    private GameObject _keyItemsPanel;


    private HItem[] _invItems;
    private HItem[] _invPokeballs;
    private HItem[] _invPotions;
    private HItem[] _invMtsTms;
    private HItem[] _invBerrys;
    private HItem[] _invKeyItems;

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

	void Start () 
    {
       
        _invItems = new HItem[_itemsInventorySize];
        _invPokeballs = new HItem[_pokeballsInventorySize];
        _invPotions = new HItem[_potionsInventorySize];
        _invMtsTms = new HItem[_mtsMtsInventorySize];
        _invBerrys = new HItem[_berrysInventorySize];
        _invKeyItems = new HItem[_keyItemsInventorySize];


	}
	
	
	void Update () 
    {

        if (Input.GetKeyUp(KeyCode.I))
        {
            if(InventoryUI != null)
            {
                InventoryUI.SetActive(!InventoryUI.activeSelf);
            }
        }

	}
}
