using UnityEngine;
using UnityEditor;
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
   // private int m_pokedexnumber = 0;
   // public int PokedexNumber { get { return m_pokedexnumber; } set { m_pokedexnumber = value; } }
    private int m_level = 5;
    public int level { get { return m_level; } set { m_level = value; } }
    private float m_exp = 0;
    public float exp { get { return m_exp; } set { m_exp = value; } }
    private float m_pp = 30;
    public float PP { get { return m_pp; } set { m_pp = value; } }
    private float m_health = 10;
    public float Health { get { return m_health; } set { m_health = value; } }
    private float m_attack = 10;
    public float Attack{get{return m_attack;}set{m_attack = value;}}
    private float m_defence = 10;
    public float defence { get { return m_defence; } set { m_defence = value; } }
    private float m_demage = 0;
    public float damage { get { return m_demage; } set { m_demage = value; } }
    private float m_speed = 10;
    public float speed { get { return m_speed; } set { m_speed = value; } }

    private float m_currenthealth = 10;
    public float CurrentHealth { get { return m_currenthealth; } set { m_currenthealth = value; } }
    private float m_currentexp = 0;
    public float currentEXP { get { return m_currentexp; } set { m_currentexp = value; } }
    private float m_currentpp = 30;
    public float currentPP { get { return m_currentpp; } set { m_currentpp = value; } }

    private PokemonType m_type1;
    public PokemonType Type1 { get { return m_type1; } set { m_type1 = value; } }
    private PokemonType m_type2;
    public PokemonType Type2 { get { return m_type2; } set { m_type2 = value; } }
    private GameObject m_pokemonprefab;
    public GameObject PokemonPrefab { get { return m_pokemonprefab; } set { m_pokemonprefab = value; } }

    private bool m_released = false;
    public bool Released { get { return m_released; } set { m_released = value; } }
    private bool m_isPlayer = false;
    public bool isPlayer { get { return m_isPlayer; } set { m_isPlayer = value; } }

    private List<MoveData> m_moves = new List<MoveData>();
    public List<MoveData> Moves { get { return m_moves; } set { m_moves = value; } }

    private List<int> m_learnmovelevel;

    public List<int> LearnMovesLevels { get { return m_learnmovelevel; } set { m_learnmovelevel = value; } }

    private bool m_isCapture = false;
    public bool IsCapture { get { return m_isCapture; } set { m_isCapture = value; } }
    //pokeball that is trap on
    private PokemonPokeball m_pokeball = PokemonPokeball.None;
    public PokemonPokeball Pokeball { get { return m_pokeball; } set { m_pokeball = value; } }

    public Pokemon(int id,string name/*,int PokedexNum*/,float PP,Sprite Icon,PokemonType type1,PokemonType type2,GameObject PokePrefab,List<MoveData> moves,List<int> learnMovesAtLevel)
    {
        m_id = id;
        m_name = name;
       // m_pokedexnumber = PokedexNum;
        m_pp = PP;
        m_icon = Icon;
        m_type1 = type1;
        m_type2 = type2;
        m_pokemonprefab = PokePrefab;
        m_moves = moves;
        m_learnmovelevel = learnMovesAtLevel;
    }
    public Pokemon(int id)
    {
        PokeAssetDatabase pokeAssetDatabase = (PokeAssetDatabase)AssetDatabase.LoadAssetAtPath("Assets/Database/PokemonAssetDatabase.asset", typeof(PokeAssetDatabase));
        Pokemon poke = pokeAssetDatabase.GetByID(id);
        m_id = poke.ID;
        m_name = poke.Name;
        m_pp = poke.PP;
        m_icon = poke.Icon;
        m_type1 = poke.Type1;
        m_type2 = poke.Type2;
        m_pokemonprefab = poke.PokemonPrefab;
        m_moves = poke.Moves;
        m_learnmovelevel = poke.LearnMovesLevels;
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
    Electricity,
    Grass,
    Poison,
    Flying,
    Steel
}
public enum PokemonPokeball
{
    None,
    Pokeball,
    GreatBall,
    UltraBall
}