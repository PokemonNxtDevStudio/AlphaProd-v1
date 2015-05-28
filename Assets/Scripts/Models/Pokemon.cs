using UnityEngine;

using UnityEngine.UI;
using System.Collections.Generic;
using System;

[Serializable]
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
   // private int m_pokedexnumber = 0;
   // public int PokedexNumber { get { return m_pokedexnumber; } set { m_pokedexnumber = value; } }
    [SerializeField]
    private int m_level = 5;
    public int level { get { return m_level; } set { m_level = value; } }

    [SerializeField]
    private float m_exp = 0;
    public float exp { get { return m_exp; } set { m_exp = value; } }

    [SerializeField]
    private float m_pp = 30;
    public float PP { get { return m_pp; } set { m_pp = value; } }

    [SerializeField]
    private float m_health = 10;
    public float Health { get { return m_health; } set { m_health = value; } }

    [SerializeField]
    private float m_attack = 10;
    public float Attack{get{return m_attack;}set{m_attack = value;}}

    [SerializeField]
    private float m_defence = 10;
    public float defence { get { return m_defence; } set { m_defence = value; } }

    [SerializeField]
    private float m_demage = 0;
    public float damage { get { return m_demage; } set { m_demage = value; } }

    [SerializeField]
    private float m_speed = 10;
    public float speed { get { return m_speed; } set { m_speed = value; } }

    [SerializeField]
    private float m_currenthealth = 10;
    public float CurrentHealth { get { return m_currenthealth; } set { m_currenthealth = value; } }

    [SerializeField]
    private float m_currentexp = 0;
    public float currentEXP { get { return m_currentexp; } set { m_currentexp = value; } }

    [SerializeField]
    private float m_currentpp = 30;
    public float currentPP { get { return m_currentpp; } set { m_currentpp = value; } }

    [SerializeField]
    private PokemonType m_type1;
    public PokemonType Type1 { get { return m_type1; } set { m_type1 = value; } }

    [SerializeField]
    private PokemonType m_type2;
    public PokemonType Type2 { get { return m_type2; } set { m_type2 = value; } }

    [SerializeField]
    private GameObject m_pokemonprefab;
    public GameObject PokemonPrefab { get { return m_pokemonprefab; } set { m_pokemonprefab = value; } }

    [SerializeField]
    private bool m_released = false;
    public bool Released { get { return m_released; } set { m_released = value; } }

    [SerializeField]
    private bool m_isPlayer = false;
    public bool isPlayer { get { return m_isPlayer; } set { m_isPlayer = value; } }

    [SerializeField]
    private List<MoveData> m_moves = new List<MoveData>();
    public List<MoveData> Moves { get { return m_moves; } set { m_moves = value; } }

    [SerializeField]
    private List<int> m_learnmovelevel;
    public List<int> LearnMovesLevels { get { return m_learnmovelevel; } set { m_learnmovelevel = value; } }

    [SerializeField]
    private bool m_isCapture = false;
    public bool IsCapture { get { return m_isCapture; } set { m_isCapture = value; } }

    [SerializeField] //pokeball that is trap on
    private PokemonPokeball m_pokeball = PokemonPokeball.None;
    public PokemonPokeball Pokeball { get { return m_pokeball; } set { m_pokeball = value; } }

    public Pokemon()
    {
        this.ID = 0;
        this.Name = "";
        // m_pokedexnumber = PokedexNum;
        this.PP = 0;
        this.Icon = null;
        this.Type1 = PokemonType.None;
        this.Type2 = PokemonType.None;
        this.PokemonPrefab = null;
       // this.m_moves = null;
       // this.m_learnmovelevel = learnMovesAtLevel;
    }
    public Pokemon(int id,string name/*,int PokedexNum*/,float PP,Sprite Icon,PokemonType type1,PokemonType type2,GameObject PokePrefab,List<MoveData> moves,List<int> learnMovesAtLevel)
    {
        this.ID = id;
        this.Name = name;
       // m_pokedexnumber = PokedexNum;
        this.PP = PP;
        this.Icon = Icon;
        this.Type1 = type1;
        this.Type2 = type2;
        this.PokemonPrefab = PokePrefab;
        this.Moves = moves;
        this.LearnMovesLevels = learnMovesAtLevel;
    }
    
    public Pokemon(Pokemon poke)
    {
        this.ID = poke.ID;
        this.Name = poke.Name;
        this.PP = poke.PP;
        this.Icon = poke.Icon;
        this.Type1 = poke.Type1;
        this.Type2 = poke.Type2;
        this.PokemonPrefab = poke.PokemonPrefab;
        this.Moves = poke.Moves;
        this.LearnMovesLevels = poke.LearnMovesLevels;
    }
    
    
}
[Serializable]
public enum PokemonType
{
    None,
    Normal,
    Fire,
    Water,
    Ground,
    Bug,
    Ghost,
    Electricity,
    Grass,
    Poison,
    Flying,
    Steel
}
[Serializable]
public enum PokemonPokeball
{
    None,
    Pokeball,
    GreatBall,
    UltraBall
}