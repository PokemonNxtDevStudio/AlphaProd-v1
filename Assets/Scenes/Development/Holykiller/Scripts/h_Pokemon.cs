using UnityEngine;
using System.Collections.Generic;

public class h_Pokemon : MonoEntity
{


    //[SerializeField]
   // private GameObject m_pokemonprefab;
   // public GameObject PokemonPrefab { get { return m_pokemonprefab; } set { m_pokemonprefab = value; } }

    [SerializeField]
    private bool m_released = false;
    public bool Released { get { return m_released; } set { m_released = value; } }

    [SerializeField]
    private bool m_isPlayer = false;
    public bool isPlayer { get { return m_isPlayer; } set { m_isPlayer = value; } }



    [SerializeField]
    private PokeData data = new PokeData();
    public PokeData pokeData { get { return data; } set { data = value; } }
    


    [SerializeField]
    private bool m_isCapture = false;
    public bool IsCapture { get { return m_isCapture; } set { m_isCapture = value; } }

    [SerializeField] //pokeball that is trap on
    private PokemonPokeball m_pokeball = PokemonPokeball.None;
    public PokemonPokeball Pokeball { get { return m_pokeball; } set { m_pokeball = value; } }
}
