using UnityEngine;
using System.Collections;

public class PlayerActions : MonoBehaviour
{
    //private HInventory inventory;

    //public static HInventory instance;

   // void Awake()
   // {
   //     if(instance == null)
   //     {
    //        instance = inventory;
    //    }
  //  }

  //  void Start()
  //  {
     //   NxtUiManager.instance.ShowMoney(inventory.Money);       
   // }
    private PokeParty pokept = new PokeParty();
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            NXT.EventHandler.ExecuteEvent(this.gameObject, "OnShopRequest");
        }
        if(Input.GetKeyDown(KeyCode.I))
        {
            NXT.EventHandler.ExecuteEvent(this.gameObject, "ShowInventory");
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            pokept.AddPokemon(new Pokemon((25)));
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            pokept.AddPokemon(new Pokemon(1));
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            pokept.AddPokemon(new Pokemon(4));
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            pokept.AddPokemon(new Pokemon(10));
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            pokept.AddPokemon(new Pokemon(14));
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            pokept.AddPokemon(new Pokemon(50));
        }
    }


}
