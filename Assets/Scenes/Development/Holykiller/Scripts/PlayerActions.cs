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
    public PokeParty PokePT { get { return pokept; } set { pokept = value; } }


    void Start()
    {
        NxtUiManager.instance.PlayerPokePt = pokept;
    }

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
            pokept.AddAPokemon(new Pokemon((25)));
            //NxtUiManager.instance.PlayerPokePt = pokept;
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            pokept.AddAPokemon(new Pokemon(1));
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            pokept.AddAPokemon(new Pokemon(4));
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            pokept.AddAPokemon(new Pokemon(10));
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            pokept.AddAPokemon(new Pokemon(14));
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            pokept.AddAPokemon(new Pokemon(50));
        }
    }


}
