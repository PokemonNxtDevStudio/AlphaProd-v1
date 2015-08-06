using UnityEngine;
using System.Collections;
using NXT.Inventory;
public class ConsumableItem : InventoryItem
{


    public float Power;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public override void Use(Object obj)
    {

//     Pokemon myPokemon = (Pokemon)obj;
 	 base.Use(obj);
    } 
}
