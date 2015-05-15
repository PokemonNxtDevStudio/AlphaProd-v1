using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class MockData  
{

    //private string movesURL = "http://mjbcdn.com/nxt/login/getMoveInfo.php";
   // private string basicURL = "http://mjbcdn.com/nxt/login/getBasicInfo.php";
    public static MockData mockdata;
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
    private PokemonType TypeFire = PokemonType.Fire;
    private PokemonType TypeNone = PokemonType.None;
    private PokemonType TypeBug = PokemonType.Bug;
    private PokemonType TypeFlying = PokemonType.Flying;
    private PokemonType TypeNormal = PokemonType.Normal;
    private PokemonType TypeElectic = PokemonType.Electricity;
    private PokemonType TypeGround = PokemonType.Ground;
    private PokemonType TypeWater = PokemonType.Water;
    private PokemonType TypeSteel = PokemonType.Steel;


    private MoveAssetDatabase moveAssetDatabase;
    private Sprite[] pokemonIcons;
    private Sprite[] moveIcons;
    private DeamageType normal = DeamageType.None;
    private DeamageType fire = DeamageType.Fire;
 //   private DeamageType water = DeamageType.Water;
    private DeamageType ground = DeamageType.Ground;
 
    private DeamageType grass = DeamageType.Grass;
    private DeamageType electic = DeamageType.Electricity;
    private DeamageType bug = DeamageType.Bug;
 //   private DeamageType dark = DeamageType.Dark;
 //   private DeamageType ghost = DeamageType.Ghost;
 //   private DeamageType ice = DeamageType.Ice;
 //   private DeamageType fighting = DeamageType.Fighting;
//    private DeamageType steel = DeamageType.Steel;
    private DeamageType poison = DeamageType.Poison;
    private DeamageType flying = DeamageType.Flying;
    private DeamageType psychic = DeamageType.Psychic;
    private DeamageType water = DeamageType.Water;

    private MoveEffect none = MoveEffect.None;
    private MoveEffect burn = MoveEffect.Burn;
    private MoveEffect poisons = MoveEffect.Poisons;
    private MoveEffect lowerdef = MoveEffect.LowerDef;
    private MoveEffect loweratk = MoveEffect.LowerAtk;
    private MoveEffect drain = MoveEffect.Drain;
    private MoveEffect slow = MoveEffect.Slow;
   // private MoveEffect blind = MoveEffect.Blind;
    private MoveEffect raiseSAtk = MoveEffect.RaiseSpecialAttack;
    private MoveEffect raiseAtk = MoveEffect.RaiseAttack;
    private MoveEffect sleep = MoveEffect.Sleep;
    private MoveEffect raiseDef = MoveEffect.RaiseDefence;
    private MoveEffect raiseSPDef = MoveEffect.RaiseSpecialDefence;
    private MoveEffect raiseSpeed = MoveEffect.RaiseSpeed;
    private MoveEffect copycat = MoveEffect.CopyCat;
    private MoveEffect reducehpbyhalf = MoveEffect.ReduceFoeHPToHalf;
    private MoveEffect paralyze = MoveEffect.Paralyse;
    private MoveEffect nevermiss = MoveEffect.NeverMiss;
    private MoveEffect protectfromsp = MoveEffect.ProtectFromSpecialAttacks;
    private MoveEffect makeacopyofhisself = MoveEffect.MakesACopyOfHisSelf;
    private MoveEffect areaeffect = MoveEffect.AreaEffect;
    private MoveEffect constantdamage = MoveEffect.ConstantDamage;
    private MoveEffect confuse = MoveEffect.Confuse;

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

    public  void PokemonmockData(/*PokeAssetDatabase pokeAssetDatabase*/)
    {
        MoveMockData();
        //moveAssetDatabase = (MoveAssetDatabase)Resources.Load("Database/MovesAssetDatabase", typeof(MoveAssetDatabase));
       // moveAssetDatabase = (MoveAssetDatabase)AssetDatabase.LoadAssetAtPath("Assets/Database/MovesAssetDatabase.asset", typeof(MoveAssetDatabase));
        //PokeAssetDatabase pokeAssetDatabase = (PokeAssetDatabase)AssetDatabase.LoadAssetAtPath("Assets/Database/PokemonAssetDatabase.asset", typeof(PokeAssetDatabase));
       
        PokeAssetDatabase pokeAssetDatabase = (PokeAssetDatabase)Resources.Load("Database/PokemonAssetDatabase", typeof(PokeAssetDatabase));
        pokeAssetDatabase.Pokemons = new Pokemon[151];
        
        pokeData = new List<Pokemon>();

        pokemonIcons = Resources.LoadAll<Sprite>("UI/Icons/Pokemons");
        //moveAssetDatabase = (MoveAssetDatabase)AssetDatabase.LoadAssetAtPath("Assets/Database/MovesAssetDatabase.asset", typeof(MoveAssetDatabase));

        if(moveAssetDatabase == null)
        {
            Debug.Log("MovesDatabase is null");
            return;
        }

        MoveData DoubleSlap = new MoveData(moveAssetDatabase.GetByID(3));
        MoveData Scratch = new MoveData(moveAssetDatabase.GetByID(10));
        MoveData Gust = new MoveData(moveAssetDatabase.GetByID(16));
        MoveData Whirlwind = new MoveData(moveAssetDatabase.GetByID(18));
        MoveData WingAttack = new MoveData(moveAssetDatabase.GetByID(17));
        MoveData Slam = new MoveData(moveAssetDatabase.GetByID(21));
        MoveData VineWhip = new MoveData(moveAssetDatabase.GetByID(22));
        MoveData SandAttack = new MoveData(moveAssetDatabase.GetByID(28));
        MoveData Tackle = new MoveData(moveAssetDatabase.GetByID(33));
        MoveData BodySlam = new MoveData(moveAssetDatabase.GetByID(34));
        MoveData TailWhip = new MoveData(moveAssetDatabase.GetByID(39));
        MoveData Leer = new MoveData(moveAssetDatabase.GetByID(43));
        MoveData Growl = new MoveData( moveAssetDatabase.GetByID(45));
        MoveData Supersonic = new MoveData(moveAssetDatabase.GetByID(48));
        MoveData SonicBoom = new MoveData(moveAssetDatabase.GetByID(49));
        MoveData Ember = new MoveData(moveAssetDatabase.GetByID(52));
        MoveData Flamethrower = new MoveData(moveAssetDatabase.GetByID(53));
        MoveData WaterGun = new MoveData(moveAssetDatabase.GetByID(55));
        MoveData HydroPump = new MoveData(moveAssetDatabase.GetByID(56));
        MoveData LeechSeed = new MoveData(moveAssetDatabase.GetByID(73));
        MoveData Growth = new MoveData(moveAssetDatabase.GetByID(74));  
        MoveData RazorLeaf = new MoveData(moveAssetDatabase.GetByID(75));
        MoveData SolarBeam = new MoveData(moveAssetDatabase.GetByID(76));
        MoveData PoisonPoweder = new MoveData( moveAssetDatabase.GetByID(77));  
        MoveData SleepPower = new MoveData( moveAssetDatabase.GetByID(79));
        MoveData StringShot = new MoveData(moveAssetDatabase.GetByID(81));
        MoveData FireSpin = new MoveData(moveAssetDatabase.GetByID(83));
        MoveData ThunderShock = new MoveData(moveAssetDatabase.GetByID(84));
        MoveData Thunderbolt = new MoveData(moveAssetDatabase.GetByID(85));
        MoveData ThunderWave = new MoveData(moveAssetDatabase.GetByID(86));
        MoveData Thunder = new MoveData(moveAssetDatabase.GetByID(87));
        MoveData Earthquake = new MoveData(moveAssetDatabase.GetByID(89));
        MoveData Dig = new MoveData(moveAssetDatabase.GetByID(91));
        MoveData Hypnosis = new MoveData(moveAssetDatabase.GetByID(95));
        MoveData Agility = new MoveData(moveAssetDatabase.GetByID(97));
        MoveData QuickAttack = new MoveData(moveAssetDatabase.GetByID(98));
        MoveData Rage = new MoveData(moveAssetDatabase.GetByID(99));
        MoveData Screech = new MoveData(moveAssetDatabase.GetByID(103));
        MoveData DoubleTeam = new MoveData(moveAssetDatabase.GetByID(104));
        MoveData Harden = new MoveData(moveAssetDatabase.GetByID(106));
        MoveData LightScreen = new MoveData(moveAssetDatabase.GetByID(113));
        MoveData FocusEnergy = new MoveData(moveAssetDatabase.GetByID(116));
        MoveData MirrorMove = new MoveData(moveAssetDatabase.GetByID(119));
        MoveData Swift = new MoveData(moveAssetDatabase.GetByID(129));
        MoveData Amnesia = new MoveData(moveAssetDatabase.GetByID(133));
        MoveData Bubble = new MoveData(moveAssetDatabase.GetByID(145));
        MoveData Splash = new MoveData(moveAssetDatabase.GetByID(150));
        MoveData HyperFang = new MoveData(moveAssetDatabase.GetByID(158));
        MoveData SuperFang = new MoveData(moveAssetDatabase.GetByID(162));
        MoveData Slash = new MoveData(moveAssetDatabase.GetByID(163));
        //Debug.Log("SomeMove is null");
        

        #region 1 Bulbasaur
        List<MoveData> BulbasaurMoves = new List<MoveData>();
        BulbasaurMoves.Add(Tackle);
        BulbasaurMoves.Add(Growl);
        BulbasaurMoves.Add(LeechSeed);
        BulbasaurMoves.Add(VineWhip);
        BulbasaurMoves.Add(PoisonPoweder);
        BulbasaurMoves.Add(RazorLeaf);
        BulbasaurMoves.Add(Growth);
        BulbasaurMoves.Add(SleepPower);
        BulbasaurMoves.Add(SolarBeam);
        List<int> BulbaMovesLevels = new List<int> { 0, 0, 7, 13, 20, 27, 34, 41, 48 };
        AddPokemon(1, "Bulbasaur", 30, pokemonIcons[1], Typegrass, TypePoison, "001_Bulbasaur", BulbasaurMoves, BulbaMovesLevels);
        #endregion
        #region 4 Charmander
        List<MoveData> CharmanderMoves = new List<MoveData>();
        CharmanderMoves.Add(Scratch);
        CharmanderMoves.Add(Growl);
        CharmanderMoves.Add(Ember);
        CharmanderMoves.Add(Leer);
        CharmanderMoves.Add(Rage);
        CharmanderMoves.Add(Slash);
        CharmanderMoves.Add(Flamethrower);
        CharmanderMoves.Add(FireSpin);
        List<int> CharMovesLevels = new List<int> { 0, 0, 9, 15, 22, 30, 38, 46 };
        AddPokemon(4, "Charmander", 30, pokemonIcons[4], TypeFire, TypeNone, "004_Charmander", CharmanderMoves, CharMovesLevels);
        #endregion
        #region 10 Caterpie
        List<MoveData> CaterpieMoves = new List<MoveData>();
        CaterpieMoves.Add(Tackle);
        CaterpieMoves.Add(StringShot);
        List<int> CaterpMovesLevels = new List<int> { 0, 0 };
        AddPokemon(10, "Caterpie", 30, pokemonIcons[10], TypeBug, TypeNone, "010_Caterpie", CaterpieMoves, CaterpMovesLevels);
        #endregion
        #region 14 Kakuna
        List<MoveData> KakunaMoves = new List<MoveData>();
        KakunaMoves.Add(Harden);
        List<int> KakunMovesLevels = new List<int> { 0 };
        AddPokemon(14, "Kakuna", 30, pokemonIcons[14], TypeBug, TypePoison, "014 _Kakuna", KakunaMoves, KakunMovesLevels);
        #endregion
        #region 16 Pidgey
        List<MoveData> PidgeyMoves = new List<MoveData>();
        PidgeyMoves.Add(Gust);
        PidgeyMoves.Add(SandAttack);
        PidgeyMoves.Add(QuickAttack);
        PidgeyMoves.Add(Whirlwind);
        PidgeyMoves.Add(WingAttack);
        PidgeyMoves.Add(Agility);
        PidgeyMoves.Add(MirrorMove);
        List<int> PidgeMovesLevels = new List<int> { 0, 5, 12, 19, 28, 36, 44 };
        AddPokemon(16, "Pidgey", 30, pokemonIcons[16], TypeNormal, TypeFlying,"016_Pidgey",PidgeyMoves,PidgeMovesLevels);
        #endregion
        #region 19 Rattata
        List<MoveData> RattataMoves = new List<MoveData>();
        RattataMoves.Add(Tackle);
        RattataMoves.Add(TailWhip);
        RattataMoves.Add(QuickAttack);
        RattataMoves.Add(HyperFang);
        RattataMoves.Add(FocusEnergy);
        RattataMoves.Add(SuperFang);
        List<int> RattatMovesLevels = new List<int> { 0, 0, 7, 14, 23, 34 };
        AddPokemon(19, "Rattata", 30, pokemonIcons[19], TypeNormal, TypeNone, "019_Rattata", RattataMoves, RattatMovesLevels);
        #endregion
        #region 25 Pikachu
        List<MoveData> PikachuMoves = new List<MoveData>();
        PikachuMoves.Add(ThunderShock);
        PikachuMoves.Add(Growl);
        PikachuMoves.Add(TailWhip);
        PikachuMoves.Add(ThunderWave);
        PikachuMoves.Add(QuickAttack);
        PikachuMoves.Add(DoubleTeam);
        PikachuMoves.Add(Slam);
        PikachuMoves.Add(Swift);
        PikachuMoves.Add(Thunderbolt);
        PikachuMoves.Add(Agility);
        PikachuMoves.Add(Thunder);
        PikachuMoves.Add(LightScreen);
        List<int> PikaMovesLevels = new List<int> { 0, 0, 6, 8, 11, 15, 20, 26, 26, 33, 41, 50 };
        AddPokemon(25, "Pikachu", 30, pokemonIcons[25], TypeElectic, TypeNone, "025_Pikachu", PikachuMoves, PikaMovesLevels);
        #endregion
        #region 50 Diglett
        List<MoveData> DiglettMoves = new List<MoveData>();
        DiglettMoves.Add(Scratch);
        DiglettMoves.Add(Growl);
        DiglettMoves.Add(Dig);
        DiglettMoves.Add(SandAttack);
        DiglettMoves.Add(Slash);
        DiglettMoves.Add(Earthquake);
        List<int> DiglMovesLevels = new List<int> { 0, 15, 19, 24, 31, 40 };
        AddPokemon(50, "Diglett", 30, pokemonIcons[50], TypeGround, TypeNone, "050_Diglett", DiglettMoves, DiglMovesLevels);
        #endregion
        #region 60 Poliwag
        List<MoveData> PoliwagMoves = new List<MoveData>();
        PoliwagMoves.Add(Bubble);
        PoliwagMoves.Add(Hypnosis);
        PoliwagMoves.Add(WaterGun);
        PoliwagMoves.Add(DoubleSlap);
        PoliwagMoves.Add(BodySlam);
        PoliwagMoves.Add(Amnesia);
        PoliwagMoves.Add(HydroPump);
        List<int> PoliwaMovesLevels = new List<int> { 0, 16, 19, 25, 31, 38, 45 };
        AddPokemon(60, "Poliwag", 30, pokemonIcons[60], TypeWater, TypeNone, "060_Polywag", PoliwagMoves, PoliwaMovesLevels);
        #endregion
        #region 81 Magnemite
        List<MoveData> MagnemiteMoves = new List<MoveData>();
        MagnemiteMoves.Add(Tackle);
        MagnemiteMoves.Add(SonicBoom);
        MagnemiteMoves.Add(ThunderShock);
        MagnemiteMoves.Add(Supersonic);
        MagnemiteMoves.Add(ThunderWave);
        MagnemiteMoves.Add(Swift);
        MagnemiteMoves.Add(Screech);
        List<int> MagnemMovesLevels = new List<int> { 0, 21, 25, 29, 35, 41, 47 };
        AddPokemon(81, "Magnemite", 30, pokemonIcons[81], TypeElectic, TypeSteel, "081_Magnemite", MagnemiteMoves, MagnemMovesLevels);
        #endregion
        #region 129 Magikarp
        List<MoveData> MagikarpMoves = new List<MoveData>();
        MagikarpMoves.Add(Splash);
        MagnemiteMoves.Add(Tackle);
        List<int> MagikarMovesLevels = new List<int> { 0, 15 };
        AddPokemon(129, "Magikarp", 30, pokemonIcons[129], TypeWater, TypeNone, "129_Magikarp", MagikarpMoves, MagikarMovesLevels);
        #endregion
        //Debug.Log("Some Pokemon is null");
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
        AssetDatabase.SaveAssets();

    }

    private void AddPokemon(int id, string name, float PP, Sprite Icon, PokemonType type1, PokemonType type2, string PokePrefabName, List<MoveData> moves,List<int> levelforMoves)
    {       
        GameObject pokeprefab = (GameObject)Resources.Load(m_pokemonsPath + PokePrefabName);
        Pokemon poke = new Pokemon(id, name, PP, Icon, type1, type2, pokeprefab, moves,levelforMoves);
        pokeData.Add(poke);       
    }    
    public void ItemMockData()
    {
       // itemAssetDatabase = (ItemAssetDatabase)AssetDatabase.LoadAssetAtPath("Assets/Database/ItemAssetDatabase.asset", typeof(ItemAssetDatabase));
        itemAssetDatabase = (ItemAssetDatabase)Resources.Load("Database/ItemAssetDatabase", typeof(ItemAssetDatabase)); 
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
        AssetDatabase.SaveAssets();
 
    }
    private void AddItem(int id,string name,Sprite icon,ItemType type,float buy,float sell,int stacksupto,string description)
    {
        InventoryItem item = new InventoryItem(id, name, icon, type, buy, sell, stacksupto, description);
        items.Add(item);
    }  
    public void MoveMockData()
    {
        moveAssetDatabase = (MoveAssetDatabase)Resources.Load("Database/MovesAssetDatabase", typeof(MoveAssetDatabase));
        //moveAssetDatabase = (MoveAssetDatabase)AssetDatabase.LoadAssetAtPath("Assets/Database/MovesAssetDatabase.asset", typeof(MoveAssetDatabase));
        moveAssetDatabase.Moves = new MoveData[621];
        moveData = new List<MoveData> ();

        AddMove(3, "Double Slap", 1, 15, 4, "The foe is slapped repeatedly, back and forth, two to five times in a row", normal, none);

        AddMove(10, "Scratch", 1, 40, 4, "Hard, pointed, and sharp claws rake the foe to inflict damage", normal, none);
        AddMove(16, "Gust", 1, 40, 4, "A gust of wind is whipped up by wings and launched at the foe to inflict damage", flying, none);
        AddMove(17, "Wing Attack", 1, 60, 4, "Demage the foe using the pokemos wings", flying, none);
        AddMove(18, "Whirlwind", 1, 40, 4, "Create a Whirlwind that demage the foe", normal, none);
        AddMove(21, "Slam", 1, 80, 4, "The foe is slammed with a long tail, vines, etc., to inflict damage", normal, none);
        AddMove(22, "Vine Whip", 1, 45, 4, "The foe is struck with slender, whiplike vines to inflict damage", grass, none);
        AddMove(28, "Sand Attack", 1, 0, 4, "Sand is hurled in the foe's face, reducing its accuracy", ground, slow);
        AddMove(33, "Tackle", 1, 50, 4, "Tackle deals damage and has no secondary effect", normal, none);
        AddMove(34, "Body Slam", 1, 85, 4, "30% chance of paralyzing Target", normal, paralyze);
        AddMove(36, "Take Down", 1, 90, 4, "Deals a high damage but also inflicts damage to the user after successful usage", normal, none);
        AddMove(38, "Double-Edge", 1, 120, 4, "A reckless, life-risking tackle. It also damages the user by a fairly large amount, however", normal, none);
        AddMove(39, "Tail Whip", 1, 0, 4, "Lowers foe Defense", normal, lowerdef);
        AddMove(43, "Leer", 1, 0, 4, "The foe is given an intimidating leer with sharp eyes. The target's Defense stat is reduced", normal, lowerdef);
        AddMove(45, "Growl", 1, 0, 4, "The user growls in an endearing way, making the foe less wary. The target's Attack stat is lowered.", normal, loweratk);
        AddMove(48, "Supersonic", 1, 0, 4, "The user generates odd sound waves from its body. It may confuse the target", normal, confuse);
        AddMove(49, "Sonic Boom", 1, 20, 4, "The foe is hit with a destructive shock wave that always inflicts 20 HP damage", normal, constantdamage);
        AddMove(52, "Ember", 1, 40, 4, "The foe is attacked with small flames. The target may also be left with a burn", fire, burn);
        AddMove(53, "Flamethrowe", 1, 90, 4, "The foe is scorched with an intense blast of fire. The target may also be left with a burn", fire, burn);
        AddMove(55, "Water Gun", 1, 40, 4, "Inflicts damage to the foe with a stream of water", water, none);
        AddMove(56, "Hydro Pump", 1, 110, 4, "The foe is blasted by a huge volume of water launched under great pressure", water, none);
        AddMove(73, "Leech Seed", 1, 0, 4, "A seed is planted on the foe. It steals some HP from the for to heal the user on every turn", grass, drain);
        AddMove(74, "Growth", 1, 0, 4, "The user's body is forced to grow all at once. It raises the Sp. Atk stat", normal, raiseSAtk);
        AddMove(75, "Razor Leaf", 1, 55, 4, "Sharp-edged leaves are launched to slash at the foe. It has a high critical-hit ratio", grass, none);
        AddMove(76, "Solar Beam", 1, 120, 4, "A two-turn attack. The user gathers light, then blasts a bundled beam on the second turn", grass, none);
        AddMove(77, "Poison Powder", 1, 0, 4, "A cloud of poisonous dust is scattered on the foe. It may poison the target", poison, poisons);       
        AddMove(79, "Sleep Powder", 1, 0, 4, "The user scatters a big cloud of sleep-inducing dust around the foe", grass, sleep);
        AddMove(81, "String Shot", 1, 0, 4, "The foe is bound with silk blown from the user’s mouth. It reduces the target’s Speed stat", bug, slow);
        AddMove(83, "Fire Spin", 1, 35, 4, "The foe becomes trapped within a fierce vortex of fire that rages for two to five turns", fire,slow);
        AddMove(84, "Thunder Shock", 1, 40, 4, "Inflicts damage to the foe with a small shock", electic, paralyze);
        AddMove(85, "Thunderbolt", 1, 90, 4, "A Thunderbolt that hits hard to the foe and can also inflict paralyz", electic, paralyze);
        AddMove(86, "Thunder Wave", 1, 0, 4, "Paralyze the foe", electic, paralyze);
        AddMove(87, "Thunder", 1, 110, 4, "One of the most strong attacks of electricity", electic, paralyze);
        AddMove(89, "Earthquake", 1, 100, 4, "The user sets off an earthquake that hits all the Pokémon in the battle", ground, areaeffect);
        AddMove(91, "Dig", 1, 80, 4, "The user burrows then attacks on the second turn. It can also be used to exit dungeons", ground, none);
        AddMove(95, "Hypnosis", 1, 0, 4, "The user employs hypnotic suggestion to make the target fall into a deep sleep", psychic, sleep);
        AddMove(97, "Agility", 1, 0, 4, "Raises the Pokemon's speed", psychic, raiseSpeed);
        AddMove(98, "Quick Attack", 1, 40, 4, "The user lunges at the foe at a speed that makes it almost invisible. It is sure to strike first", normal, none);
        AddMove(99, "Rage", 1, 40, 4, "While this move is in use, it gains attack power each time the user is hit in battle", normal, raiseAtk);
        AddMove(103, "Screech", 1, 0, 4, "An earsplitting screech is emitted to sharply reduce the foe’s Defense stat", normal, lowerdef);
        AddMove(104, "Double Team", 1, 0, 4, "By moving rapidly, the user makes illusory copies of itself to raise its evasiveness", normal, makeacopyofhisself);
        AddMove(106, "Harden", 1, 0, 4, "The user stiffens all the muscles in its body to raise its Defense stat", normal, raiseDef);
        AddMove(113, "Light Screen", 1, 0, 4, "A wondrous wall of light is put up to suppress damage from special attacks for five turns", psychic, protectfromsp);        
        AddMove(116, "Focus Energy", 1, 0, 4, "The user takes a deep breath and focuses to raise the critical-hit ratio of its attacks", normal, raiseAtk);
        AddMove(119, "Mirror Move", 1, 0, 4, "The user counters the foe by mimicking the move last used by the foe", flying, copycat);
        AddMove(129, "Swift", 1, 60, 4, "Star-shaped rays are shot at the foe. This attack never misses", normal, nevermiss);
        AddMove(133, "Amnesia", 1, 0, 4, "Raises the Pokémon's Special Defense by two points", psychic, raiseSPDef);
        AddMove(145, "Bubble", 1, 40, 4, "10% chance of lowering target's Speed. Weaker in Double Battles", water, slow);
        AddMove(150, "Splash", 1, 0, 4, "The user just flops and splashes around to no effect at all...", normal, none);
        AddMove(158, "Hyper Fang", 1, 80, 4, "The user bites hard on the foe with it sharp fornt fangs. It may also make the target flinch", normal, none);
        AddMove(162, "Super Fang", 1, 0, 4, "The user chomps hard on the foe with its sharp front fangs. It cuts the target’s HP to half", normal, reducehpbyhalf);
        AddMove(163, "Slash", 1, 70, 4, "The foe is attacked with a slash of claws, etc. It has a high critical-hit ratio", normal, none);
        AddMove(230, "Sweet Scent", 1, 0, 4, "A sweet scent that Slows the foe", normal, slow);

        for (int i = 0; i < moveData.Count; i++)
        {
            if (moveAssetDatabase.Moves[moveData[i].ID] == null)
                moveAssetDatabase.Moves[moveData[i].ID] = moveData[i];
        }

        Debug.Log("Move Moke Data created");
        AssetDatabase.SaveAssets();
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
