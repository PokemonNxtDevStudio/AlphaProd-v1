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
            pokept.AddPokemon(new Pokemon((25)));
            //NxtUiManager.instance.PlayerPokePt = pokept;
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

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            pokept.SelectedIndex = 0;
            NxtUiManager.instance.CurSelectedPoke(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            pokept.SelectedIndex = 1;
            NxtUiManager.instance.CurSelectedPoke(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            pokept.SelectedIndex = 2;
            NxtUiManager.instance.CurSelectedPoke(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            pokept.SelectedIndex = 3;
            NxtUiManager.instance.CurSelectedPoke(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            pokept.SelectedIndex = 4;
            NxtUiManager.instance.CurSelectedPoke(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            pokept.SelectedIndex = 5;
            NxtUiManager.instance.CurSelectedPoke(5);
        }
    }


}
