using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class Pokemon : AssetItem
{
   
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
    public string moveCast;

    public bool thrown = false;

}