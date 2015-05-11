using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class MockData  
{

    //private string movesURL = "http://mjbcdn.com/nxt/login/getMoveInfo.php";
   // private string basicURL = "http://mjbcdn.com/nxt/login/getBasicInfo.php";

    public static List<Pokemon> pokeData;
    public static List<MoveData> moveData;
   // public PokeAssetDatabase pokeAssetDatabase;
    //Items DataBase
    private ItemAssetDatabase itemAssetDatabase;
    private List<InventoryItem> items;
    private Sprite[] itemsIcon;
    private Sprite[] pokeballIcons;
    private ItemType general = ItemType.GeneralItem;
    private ItemType pokeball = ItemType.Pokeball;
    private string m_pokemonsPath = "Prefabs/Characters/Pokemon/";

    private PokemonType Typegrass = PokemonType.Grass;
    private PokemonType TypePoison = PokemonType.Poison;



    private MoveAssetDatabase moveAssetDatabase;
    private Sprite[] pokemonIcons;
    private Sprite[] moveIcons;
    private DeamageType normal = DeamageType.None;
 //   private DeamageType fire = DeamageType.Fire;
 //   private DeamageType water = DeamageType.Water;
 //   private DeamageType ground = DeamageType.Ground;
 
    private DeamageType grass = DeamageType.Grass;
 //   private DeamageType electic = DeamageType.Electricity;
 //   private DeamageType bug = DeamageType.Bug;
 //   private DeamageType dark = DeamageType.Dark;
 //   private DeamageType ghost = DeamageType.Ghost;
 //   private DeamageType ice = DeamageType.Ice;
 //   private DeamageType fighting = DeamageType.Fighting;
//    private DeamageType steel = DeamageType.Steel;
    private DeamageType poison = DeamageType.Poison;

    private MoveEffect none = MoveEffect.None;
   // private MoveEffect burn = MoveEffect.Burn;
    private MoveEffect poisons = MoveEffect.Poisons;
   // private MoveEffect lowerdef = MoveEffect.LowerDef;
    private MoveEffect loweratk = MoveEffect.LowerAtk;
    private MoveEffect drain = MoveEffect.Drain;
    private MoveEffect slow = MoveEffect.Slow;
   // private MoveEffect blind = MoveEffect.Blind;
    private MoveEffect raiseSAtk = MoveEffect.RaiseSpecialAttack;
  //  private MoveEffect raiseAtk = MoveEffect.RaiseAttack;
    private MoveEffect sleep = MoveEffect.Sleep;


   // private ItemType potion = ItemType.Potion;
//    private ItemType mttm = ItemType.MtTm;
   // private ItemType berry = ItemType.Berry;
   // private ItemType keyitem = ItemType.KeyItem;
    
    
    public static MockData instance;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        //StartCoroutine(populateDB ());

        //Mock data populate
       // PokemonmockData();

       // pokeAssetDatabase.Pokemons = new Pokemon[151];

        //for (int i = 0; i < pokeData.Count; i++)
        //{
         //   if (pokeAssetDatabase.Pokemons[i] == null)
        //        pokeAssetDatabase.Pokemons[i] = pokeData[i];
        //}        
    }

    public void PokemonmockData(PokeAssetDatabase pokeAssetDatabase)
    {
       // pokeAssetDatabase = (PokeAssetDatabase)AssetDatabase.LoadAssetAtPath("Assets/Database/PokemonAssetDatabase.asset", typeof(PokeAssetDatabase));
        pokeAssetDatabase.Pokemons = new Pokemon[151];
        
        pokeData = new List<Pokemon>();

        pokemonIcons = Resources.LoadAll<Sprite>("UI/Icons/Pokemons");
        moveAssetDatabase = (MoveAssetDatabase)AssetDatabase.LoadAssetAtPath("Assets/Database/MovesAssetDatabase.asset", typeof(MoveAssetDatabase));

        List<MoveData> BulbasaurMoves = new List<MoveData> 
        {
            moveAssetDatabase.GetByID(33),
            moveAssetDatabase.GetByID(45),      
            moveAssetDatabase.GetByID(73),
            moveAssetDatabase.GetByID(77),
            moveAssetDatabase.GetByID(75),
            moveAssetDatabase.GetByID(74),
            moveAssetDatabase.GetByID(79),
            moveAssetDatabase.GetByID(76)
        };

        AddPokemon(1, "Bulbasaur", 30, pokemonIcons[1], Typegrass, TypePoison, "001_Bulbasaur", BulbasaurMoves);



        #region oldCode
        /*
        moveData.Add(new MoveData
        {
            Id = 33,
            Name = "Tackle",
            LongText = "Tackle mock textS",
            PP = 1f,
            Accuracy = 100f,
            DamageType = "Physical",
            Power = 50f,
            PokemonId = 1,
            ShortEffect = "Tackle mock textL",
            Cooldown = 4
        });

        moveData.Add(new MoveData
        {
            Id = 45,
            Name = "Growl",
            LongText = "Growl mock textS",
            PP = 1f,
            Accuracy = 100f,
            DamageType = "Physical",
            Power = 0f,
            PokemonId = 1,
            ShortEffect = "Growl mock textL",
            Cooldown = 4
        });

        moveData.Add(new MoveData
        {
            Id = 73,
            Name = "Leech Seed",
            LongText = "Leech Seed mock textS",
            PP = 1f,
            Accuracy = 90f,
            DamageType = "Physical",
            Power = 0f,
            PokemonId = 1,
            ShortEffect = "Leech Seed mock textL",
            Cooldown = 4
        });

        moveData.Add(new MoveData
        {
            Id = 22,
            Name = "Vine Whip",
            LongText = "Vine Whip mock textS",
            PP = 1f,
            Accuracy = 100f,
            DamageType = "Grass",
            Power = 45f,
            PokemonId = 1,
            ShortEffect = "Vine Whip mock textL",
            Cooldown = 4

        });

        moveData.Add(new MoveData
        {
            Id = 98,
            Name = "Quick Attack",
            LongText = "Quick Attack mock textS",
            PP = 3f,
            Accuracy = 100f,
            DamageType = "Physical",
            Power = 40f,
            PokemonId = 25,
            ShortEffect = "Quick Attack mock textL",
            Cooldown = 2
        });

        moveData.Add(new MoveData
        {
            Id = 25,
            Name = "Thunder",
            LongText = "Thunderbolt mock textS",
            PP = 15f,
            Accuracy = 100f,
            DamageType = "Electric",
            Power = 90f,
            PokemonId = 25,
            ShortEffect = "Thunderbolt mock textL",
            Cooldown = 2
        });

        moveData.Add(new MoveData
        {
            Id = 104,
            Name = "Double Team",
            LongText = "Double Team mock textS",
            PP = 15f,
            Accuracy = 100f,
            DamageType = "Physical",
            Power = 0f,
            PokemonId = 25,
            ShortEffect = "Double Team mock textL",
            Cooldown = 4
        });

        moveData.Add(new MoveData
        {
            Id = 45,
            Name = "Growl",
            LongText = "Growl mock textS",
            PP = 40f,
            Accuracy = 100f,
            DamageType = "Physical",
            Power = 0f,
            PokemonId = 25,
            ShortEffect = "Growl mock textL",
            Cooldown = 4
        });

        moveData.Add(new MoveData
        {
            Id = 55,
            Name = "Water Gun",
            LongText = "Water Gun mock textS",
            PP = 25f,
            Accuracy = 100f,
            DamageType = "Water",
            Power = 40f,
            PokemonId = 7,
            ShortEffect = "Water Gun mock textL"
        });

        moveData.Add(new MoveData
        {
            Id = 45,
            Name = "Growl",
            LongText = "Growl mock textS",
            PP = 40f,
            Accuracy = 100f,
            DamageType = "Physical",
            Power = 0f,
            PokemonId = 7,
            ShortEffect = "Growl mock textL"
        });

        moveData.Add(new MoveData
        {
            Id = 33,
            Name = "Tackle",
            LongText = "Tackle mock textS",
            PP = 35f,
            Accuracy = 100f,
            DamageType = "Physical",
            Power = 50f,
            PokemonId = 7,
            ShortEffect = "Tackle mock textL"
        });

        moveData.Add(new MoveData
        {
            Id = 110,
            Name = "Withdraw",
            LongText = "Withdraw mock textS",
            PP = 40f,
            Accuracy = 100f,
            DamageType = "Defense",
            Power = 0f,
            PokemonId = 7,
            ShortEffect = "Withdraw mock textL"
        });

        moveData.Add(new MoveData
        {
            Id = 10,
            Name = "Scratch",
            LongText = "Scratch mock textS",
            PP = 35f,
            Accuracy = 100f,
            DamageType = "Physical",
            Power = 40f,
            PokemonId = 4,
            ShortEffect = "Scratch mock textL"
        });

        moveData.Add(new MoveData
        {
            Id = 45,
            Name = "Growl",
            LongText = "Growl mock textS",
            PP = 40f,
            Accuracy = 100f,
            DamageType = "Physical",
            Power = 0f,
            PokemonId = 4,
            ShortEffect = "Growl mock textL"
        });

        moveData.Add(new MoveData
        {
            Id = 52,
            Name = "Ember",
            LongText = "Ember mock textS",
            PP = 25f,
            Accuracy = 100f,
            DamageType = "Fire",
            Power = 40f,
            PokemonId = 4,
            ShortEffect = "Ember mock textL"
        });

        moveData.Add(new MoveData
        {
            Id = 108,
            Name = "Smokescreen",
            LongText = "Smokescreen mock textS",
            PP = 20f,
            Accuracy = 100f,
            DamageType = "Defense",
            Power = 0f,
            PokemonId = 4,
            ShortEffect = "Smokescreen mock textL"
        });

        moveData.Add(new MoveData
        {
            Id = 32,
            Name = "Double Team",
            LongText = "Double Team mock textS",
            PP = 15f,
            Accuracy = 100f,
            DamageType = "Defense",
            Power = 0f,
            PokemonId = 25,
            ShortEffect = "Double Team mock textL"
        });

        pokeData.Add(new Pokemon
        {
            ID = 1,
            IconName = "BulbasaurIcon",
            Name = "Bulbasaur",
            HP = 45f,
            Attack = 49f,
            Defense = 49f,
            spAttack = 65f,
            spDefense = 65f,
            Speed = 45f,
            Weight = 69f,
            Height = 7f,
            CaptureRate = 45f,
            BaseXP = 64f,
            BasePP = 30f
        });

        pokeData.Add(new Pokemon
        {
            ID = 4,
            IconName = "CharmanderIcon",
            Name = "Charmander",
            HP = 39f,
            Attack = 52f,
            Defense = 43f,
            spAttack = 60f,
            spDefense = 50f,
            Speed = 65f,
            Weight = 85f,
            Height = 6f,
            CaptureRate = 45f,
            BaseXP = 62f,
            BasePP = 30f
        });

        pokeData.Add(new Pokemon
        {
            ID = 7,
            IconName = "SquirtleIcon",
            Name = "Squirtle",
            HP = 44f,
            Attack = 48f,
            Defense = 65f,
            spAttack = 50f,
            spDefense = 64f,
            Speed = 43f,
            Weight = 90f,
            Height = 5f,
            CaptureRate = 45f,
            BaseXP = 63f,
            BasePP = 30f,

        });

        pokeData.Add(new Pokemon
        {
            ID = 25,
            IconName = "PikachuIcon",
            Name = "Pikachu",
            HP = 35f,
            Attack = 55f,
            Defense = 40f,
            spAttack = 50f,
            spDefense = 50f,
            Speed = 90f,
            Weight = 60f,
            Height = 4f,
            CaptureRate = 190f,
            BaseXP = 105f,
            BasePP = 30f
        });
        * */
        //StaticGlobalVariables.pokeData = pokeData;
        // StaticGlobalVariables.moveData = moveData;
        #endregion

        for (int i = 0; i < pokeData.Count; i++)
        {
            if (pokeAssetDatabase.Pokemons[pokeData[i].ID] == null)
                pokeAssetDatabase.Pokemons[pokeData[i].ID] = pokeData[i];
        }
       // AddSkillsTo(1, 33);
       // AddSkillsTo(1, 45);
       // AddSkillsTo(1, 73);

    }
    /*
    private void AddSkillsTo(int id,int skillID)
    {
        moveAssetDatabase = (MoveAssetDatabase)AssetDatabase.LoadAssetAtPath("Assets/Database/MovesAssetDatabase.asset", typeof(MoveAssetDatabase));
        pokeData[id].Moves.Add(moveAssetDatabase.GetByID(skillID));
    }*/

    private void AddPokemon(int id, string name, float PP, Sprite Icon, PokemonType type1, PokemonType type2, string PokePrefabName, List<MoveData> moves)
    {       
        GameObject pokeprefab = (GameObject)Resources.Load(m_pokemonsPath + PokePrefabName);
        Pokemon poke = new Pokemon(id, name, PP, Icon, type1, type2, pokeprefab, moves);
        pokeData.Add(poke);
       
    }
    
    
    public void ItemMockData()
    {
        itemAssetDatabase = (ItemAssetDatabase)AssetDatabase.LoadAssetAtPath("Assets/Database/ItemAssetDatabase.asset", typeof(ItemAssetDatabase));

        itemAssetDatabase.items = new InventoryItem[1500];
        items = new List<InventoryItem>();
        itemsIcon = Resources.LoadAll<Sprite>("UI/Icons/ItemsIcons");
        pokeballIcons = Resources.LoadAll<Sprite>("UI/Icons/pokeballs");
        

        AddItem(1, "Antidote", itemsIcon[0], general, 100, 50, 5, "Heals a Pokemon With Poison");
        AddItem(2, "Paralyze Heal", itemsIcon[1], general, 200, 100, 5, "Heals a Pokemon With Paralysis");
        AddItem(3, "Awakening", itemsIcon[2], general, 250, 125, 5, "Wake Up a Pokemon With Sleep");
        AddItem(4, "Burn Heal", itemsIcon[3], general, 250, 125, 5, "Heals a Pokemon With Burn");
        AddItem(5, "Ice Heal", itemsIcon[4], general, 250, 125, 5, "Heals a Pokemon With Freeze");
        AddItem(6, "Full Heal", itemsIcon[5], general, 600, 300, 5, "Heals a Pokemon from All Bad Status");
        AddItem(7, "Ether", itemsIcon[6], general, 1200, 600, 5, "Restor 10 PP of One Pokemon");
        AddItem(8,"Max Ether",itemsIcon[7],general,2000,1000,5,"Full Restore PP Of One Pokemon");
        AddItem(9,"Elixir",itemsIcon[8],general,3000,1500,5,"Restore 10 HP,10PP to a Pokemon and All Bad Status");
        AddItem(10,"Max Elixir", itemsIcon[9], general, 9800,4900, 5, "FullyRestore a Pokemon HP,PP and Cures All Bad Status");
        AddItem(11, "Hp Up - Vitamin", itemsIcon[10], general, 9800, 4900, 5, "Add +10 HP to Max HP  of  Pokemon (100 HP Max)");
        AddItem(12, "Protein - Vitamin", itemsIcon[11], general, 9800, 4900, 5, "Add +10 Attack to Max Attack  of  Pokemon (100 Attack Max)");
        AddItem(13, "Iron - Vitamin", itemsIcon[12], general, 9800, 4900, 5, "Add +10 Defense to Max Defense of  Pokemon (100 Defense Max)");
        AddItem(14, "Calcium - Vitamin", itemsIcon[13], general, 9800, 4900, 5, "Add +10 Special Attack to Max SA of  Pokemon (100 SA Max)");
        AddItem(15, "Carbos - Vitamin", itemsIcon[14], general, 9800, 4900, 5, "Add +10 Speed to Max Speed  of  Pokemon (100 Speed Max)");
        AddItem(16, "PP Up - Vitamin", itemsIcon[15], general, 9800, 4900, 5, "Add ⅕ PP to Max PP  of  Pokemon (3 PP Up Max)");
        AddItem(17, "Rare Candy - Vitamin", itemsIcon[16], general, 999999, 2400, 5, "Raise Pokemon 1 Level");

        AddItem(300, "PokeBall", pokeballIcons[0], pokeball, 200, 100, 5, "Allows To Catch Pokemons ( Rate x1)");
        AddItem(301, "Great Ball", pokeballIcons[1], pokeball, 600, 300, 5, "Allows To Catch Pokemons ( Rate x1.5)");
        AddItem(302, "Ultra Ball", pokeballIcons[2], pokeball, 1200, 600, 5, "Allows To Catch Pokemons ( Rate x2)");

        for (int i = 0; i < items.Count; i++)
        {
            if (itemAssetDatabase.items[items[i].ID] == null)
                itemAssetDatabase.items[items[i].ID] = items[i];
        }
 
    }

    private void AddItem(int id,string name,Sprite icon,ItemType type,float buy,float sell,int stacksupto,string description)
    {
        InventoryItem item = new InventoryItem(id, name, icon, type, buy, sell, stacksupto, description);
        items.Add(item);
    }
        
   
    public void MoveMockData()
    {
        moveAssetDatabase = (MoveAssetDatabase)AssetDatabase.LoadAssetAtPath("Assets/Database/MovesAssetDatabase.asset", typeof(MoveAssetDatabase));
        moveAssetDatabase.Moves = new MoveData[621];
        moveData = new List<MoveData> ();

        AddMove(22, "Vine Whip", 1, 45, 4, "The foe is struck with slender, whiplike vines to inflict damage", grass, none);
        AddMove(33, "Tackle", 1, 50, 4, "Tackle deals damage and has no secondary effect", normal, none);
        AddMove(36, "Take Down", 1, 90, 4, "Deals a high damage but also inflicts damage to the user after successful usage", normal, none);
        AddMove(38, "Double-Edge", 1, 120, 4, "A reckless, life-risking tackle. It also damages the user by a fairly large amount, however", normal, none);
        AddMove(45, "Growl", 1, 0, 4, "The user growls in an endearing way, making the foe less wary. The target's Attack stat is lowered.", normal, loweratk);
        AddMove(73, "Leech Seed", 1, 0, 4, "A seed is planted on the foe. It steals some HP from the for to heal the user on every turn", grass, drain);
        AddMove(74, "Growth", 1, 0, 4, "The user's body is forced to grow all at once. It raises the Sp. Atk stat", normal, raiseSAtk);
        AddMove(75, "Razor Leaf", 1, 55, 4, "Sharp-edged leaves are launched to slash at the foe. It has a high critical-hit ratio", grass, none);
        AddMove(76, "Solar Beam", 1, 120, 4, "A two-turn attack. The user gathers light, then blasts a bundled beam on the second turn", grass, none);
        AddMove(77, "Poison Powder", 1, 0, 4, "A cloud of poisonous dust is scattered on the foe. It may poison the target", poison, poisons);
       
        AddMove(79, "Sleep Powder", 1, 0, 4, "The user scatters a big cloud of sleep-inducing dust around the foe", grass, sleep);
        AddMove(230, "Sweet Scent", 1, 0, 4, "A sweet scent that Slows the foe", normal, slow);

        for (int i = 0; i < moveData.Count; i++)
        {
            if (moveAssetDatabase.Moves[moveData[i].ID] == null)
                moveAssetDatabase.Moves[moveData[i].ID] = moveData[i];
        }
    }
    private void AddMove(int Id, string Name, float PP, float Power, float Cooldown, string Description, DeamageType Type, MoveEffect Effect)
    {
        MoveData move = new MoveData(Id, Name, PP, Power, Cooldown, Description, Type, Effect);
        moveData.Add(move);
    }
    /*
    public Pokemon getBasicPokemon(int id)
    {
        Pokemon result = new Pokemon();

        foreach (var i in pokeData.Where(e => e.Id == id))
        {

            result.Attack = i.Attack;
            result.HP = i.HP;
            result.Attack = i.Attack;
            result.Defense = i.Defense;
            result.HP = i.HP;
            result.HP = i.HP;
            result.Name = i.Name;
            result.IconName = i.IconName;
            result.Id = int.Parse(i.Id.ToString());
            result.Speed = i.Speed;
            result.BaseXP = i.BaseXP;
            result.BasePP = i.BasePP;

        }
        int baseMoveCounter = 0;


        foreach (var i in moveData.Where(e => e.PokemonId == id))
        {
            if (baseMoveCounter < 4)
            {
                result.moves.Add(i);

                // ******************************************************

                //result.moves[baseMoveCounter].IconName = moveAssetDatabase.GetByID(result.moves[baseMoveCounter].Id).IconName;
                //result.moves[baseMoveCounter].VFXPrefab = moveAssetDatabase.GetByID(result.moves[baseMoveCounter].Id).VFXPrefab;
                //result.moves[baseMoveCounter].MoveUI = moveAssetDatabase.GetByID(result.moves[baseMoveCounter].Id).MoveUI;

            }
            else
            {
                break;
            }
            baseMoveCounter++;
        }
     
        return result;
    }
     * */
        

}
