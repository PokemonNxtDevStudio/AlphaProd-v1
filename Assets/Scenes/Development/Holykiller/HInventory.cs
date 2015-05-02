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
    private Inventory _Inv;
    
	
	void Start () 
    {
        _Inv = new Inventory();
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
