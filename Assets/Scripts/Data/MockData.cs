using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MockData : MonoBehaviour 
{

    //private string movesURL = "http://mjbcdn.com/nxt/login/getMoveInfo.php";
   // private string basicURL = "http://mjbcdn.com/nxt/login/getBasicInfo.php";

    public static List<Pokemon> pokeData;
    public static List<MoveData> moveData;
    public PokeAssetDatabase pokeAssetDatabase = null;
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
        mockData();

        pokeAssetDatabase.items = new Pokemon[150];

        for (int i = 0; i < pokeData.Count; i++)
        {
            if (pokeAssetDatabase.items[i] == null)
                pokeAssetDatabase.items[i] = pokeData[i];
        }        
    }

    void mockData()
    {
        moveData = new List<MoveData> { };
        pokeData = new List<Pokemon> { };
   

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
        #endregion

}
