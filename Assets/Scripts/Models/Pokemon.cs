using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class Pokemon : AssetItem
{
    /*
    //public float Id { get; set; }
    public string IconName { get; set; }
    public float Weight { get; set; }
    public float Height { get; set; }
    public float BaseXP { get; set; }
    public float BasePP { get; set; }
    public float CaptureRate { get; set; }
    public string PokeType { get; set; }
//    public string Name { get; set; }
    public float Speed { get; set; }
    public float HP { get; set; }
    public float Attack { get; set; }
    public float Defense { get; set; }
    public float spAttack { get; set; }
    public float spDefense { get; set; }

    public List<MoveData> moves = new List<MoveData>();
    public bool isPlayer = false;
    public bool thrown = false;
    */
   /*
    //public PokeBattler obj = null;
    public int PokedexNumber = 0;
    //public string name = "";
    public Sprite Icon;
    public string iconName;
    public int level = 5;
    public float xp = 0;
    public float hp = 1;
    private float pp;
    public float PP
    {
        get { return pp; }
        set { pp = value; currentPP = value; }
    }    
    //public List<Move> moves = new List<Move> ();
    public List<MoveData> moves = new List<MoveData>();
    public bool isPlayer = false;
    public float currentHealth = 10;
    public float currentXP = 0;
    public float currentPP = 30;
    public float health = 10;
    public float attack = 10;
    public float defence = 10;
    public float damage = 0;
    public float speed = 10;
    //public string moveCast;

    public bool Released = false;
    * */

    public int PokedexNumber = 0;    
    public int level = 5;
    public float exp = 0;
    private float PP = 30;
    public float health = 10;
    public float attack = 10;
    public float defence = 10;
    public float damage = 0;
    public float speed = 10;

    public float currentHealth = 10;
    public float currentEXP = 0;
    public float currentPP = 30;

    public Sprite Icon;   

    public PokemonType Type1;
    public PokemonType Type2;

    public GameObject PokemonPrefab;

    public bool Released = false;
    public bool isPlayer = false;     
    
    public List<MoveData> moves = new List<MoveData>();

    public Pokemon(int pid,string pname,int pPokedexNum,float pPP,Sprite pIcon,PokemonType ptype1,PokemonType ptype2,GameObject pPPrefab)
    {
        this.ID = pid;
        this.Name = pname;
        this.PokedexNumber = pPokedexNum;
        this.PP = pPP;
        this.Icon = pIcon;
        this.Type1 = ptype1;
        this.Type2 = ptype2;
        this.PokemonPrefab = pPPrefab;
    }
    
    
}
public enum PokemonType
{
    None,
    Normal,
    Fire,
    Water,
    Ground,
    Bug,
    Ghost,
    Electricity
}