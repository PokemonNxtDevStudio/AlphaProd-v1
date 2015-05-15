using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PokeUI : MonoBehaviour
{
    //[SerializeField]
    public Image m_pokeIcon;
    //public Sprite PokeIcon { get { return m_pokeIcon; } set { m_pokeIcon = value; } }
   // [SerializeField]
    private Text m_pokeButtonName;

    private Pokemon m_pokemon;


    void Start()
    {
        //Image ig = transform.GetComponent<Image>();
        m_pokeIcon = transform.GetComponent<Image>();//gameObject.transform.GetComponent<Sprite>();//.GetComponent<Image>().sprite;
        m_pokeButtonName = gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        if (m_pokeIcon == null)
        {
            Debug.Log("there was no pokemonIcon");
        }
    }
    public void SetThisPokemonIconAndName(Pokemon poke)
    {
        if(poke != null)       
        {
            m_pokemon = poke;
            m_pokeIcon.sprite = poke.Icon;
            m_pokeButtonName.text = poke.Name;
        }
        else
        {
            Debug.Log("None Existing Pokemon To Set up the Icon and Name");
            //no poke
        }
        ShowThisPokemonInfo();
    }
	
	
	public void ShowThisPokemonInfo()
    {
        if(m_pokemon != null)
        {
            string ty1 = "";
            switch(m_pokemon.Type1)
            {
                case PokemonType.Normal:
                    ty1 = "Normal";
                    break;
                case PokemonType.Bug:
                    ty1 = "Bug";
                    break;
                case PokemonType.Electricity:
                    ty1 = "Electic";
                    break;
                case PokemonType.Fire:
                    ty1 = "Fire";
                    break;
                case PokemonType.Flying:
                    ty1 = "Flying";
                    break;
                case PokemonType.Ghost:
                    ty1 = "Ghost";
                    break;
                case PokemonType.Grass:
                    ty1 = "Grass";
                    break;
                case PokemonType.Ground:
                    ty1 = "Ground";
                    break;
                case PokemonType.None:
                    ty1 = "None";
                    break;
                case PokemonType.Poison:
                    ty1 = "Poison";
                    break;
                case PokemonType.Steel:
                    ty1 = "Steel";
                    break;
                case PokemonType.Water:
                    ty1 = "Water";
                    break;

            }
            string ty2 = "";
            switch (m_pokemon.Type2)
            {
                case PokemonType.Normal:
                    ty2 = "Normal";
                    break;
                case PokemonType.Bug:
                    ty2 = "Bug";
                    break;
                case PokemonType.Electricity:
                    ty2 = "Electic";
                    break;
                case PokemonType.Fire:
                    ty2 = "Fire";
                    break;
                case PokemonType.Flying:
                    ty2 = "Flying";
                    break;
                case PokemonType.Ghost:
                    ty2 = "Ghost";
                    break;
                case PokemonType.Grass:
                    ty2 = "Grass";
                    break;
                case PokemonType.Ground:
                    ty2 = "Ground";
                    break;
                case PokemonType.None:
                    ty2 = "None";
                    break;
                case PokemonType.Poison:
                    ty2 = "Poison";
                    break;
                case PokemonType.Steel:
                    ty2 = "Steel";
                    break;
                case PokemonType.Water:
                    ty2 = "Water";
                    break;

            }

            NxtUiManager.instance.ShowCurPokemonStatus(m_pokemon.Name, m_pokemon.level, m_pokemon.currentEXP, 100, "normal", ty1, ty2, m_pokemon.Health, m_pokemon.Attack, m_pokemon.defence, m_pokemon.PP, m_pokemon.speed);
        }
        else
        {
            Debug.Log("None Existing Pokemon");
        }
       
    }
}
