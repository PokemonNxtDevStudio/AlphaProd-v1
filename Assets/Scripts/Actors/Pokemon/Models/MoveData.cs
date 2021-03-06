﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
/// <summary>
///  Each pokemon move is refereced by its moveData
/// </summary>
[Serializable]
public class MoveData : Entity
{
    /*
    /// <summary>
    /// MoveName
    /// </summary>
    public string Name { get; set; }
    public string MoveName;
    /// <summary>
    /// PPCost for the Move
    /// </summary>
    public float PP { get; set; }
    public float MovePP;
    /// <summary>
    /// Accuracy? We might need this later
    /// </summary>
    public float Accuracy { get; set; }
    public float MoveAccuracy;
    /// <summary>
    /// Move ID reference
    /// </summary>
    public int Id { get; set; }
    public int MoveID;
    /// <summary>
    /// Which Pokemon ID is referencing this move
    /// </summary>
    public float PokemonId { get; set; }
    public float MovePokemonID;
    /// <summary>
    /// How much Damage does the move cause
    /// </summary>
    public float Power { get; set; }
    public float MovePower;
    public string LongText { get; set; }
    public string MoveLongText;
    public string ShortEffect { get; set; }
    public string MoveShortEffect;
    /// <summary>
    ///  Type of Move(Fire/Water .etc?
    /// </summary>
    public string DamageType { get; set; }
    public string MoveDamageTypeString;
    public Type MoveDemageType;

    private float cooldown;

    public float Cooldown
    {
        get { return cooldown; }
        set { cooldown = value; CooldownTimer = value; }
    }
    public float MoveCooldown;
    public float CooldownTimer { get; set; }
    public float MoveCooldownTimer;

    public bool IsOnCooldown = false;
    public string IconName { get; set; }
    public string MoveIconName;
    public string Description { get; set; }
    public string MoveDescription;
    public string AudioClipName { get; set; }
    public string MoveAudioClipName;
    public AudioClip MoveAudio;
    public GameObject VFXPrefab { get; set; }
    public GameObject MoveVFXPrefab;
    
    public GameObject MoveUI { get; set; }
    public GameObject MoveUIImage;

    public Sprite MoveUIIcon { get; set; }
    public Sprite MoveSpriteIcon;
    */

	[SerializeField]
	private int m_TM_ID;
	public int TM { get { return m_TM_ID; } set { m_TM_ID = value; } }
	[SerializeField]
    private float m_pp;
    public float PP { get { return m_pp; } set { m_pp = value; } }
  
    [SerializeField]
    private float m_power;
    public float Power { get { return m_power; } set { m_power = value; } }
    [SerializeField]
   
    private bool m_isoncooldown = false;
    public bool IsOnCooldown { get { return m_isoncooldown; } set { m_isoncooldown = value; } }
   
    [SerializeField]
    private float m_movecooldown;
    public float MoveCooldown { get { return m_movecooldown; } set { m_movecooldown = value; } }
   
    [SerializeField]
	private MoveType m_movetype;
    public MoveType MoveType { get { return m_movetype; } set { m_movetype = value; } }
   
	[SerializeField]
	private MoveCategory m_moveCategory;
	public MoveCategory MoveCategory { get { return m_moveCategory; } set { m_moveCategory = value; } }

	[SerializeField]
    private MoveEffect m_moveeffect;
    public MoveEffect MoveEffect { get { return m_moveeffect; } set { m_moveeffect = value; } }

    [SerializeField]
    private AudioClip m_moveaudio = null;
    public AudioClip MoveAudio { get { return m_moveaudio; } set { m_moveaudio = value; } }

    [SerializeField]
    private GameObject m_movevfxprefab = null;
    public GameObject MoveVFXPrefab { get { return m_movevfxprefab; } set { m_movevfxprefab = value; } }


    #region CastDetails
    
    //Todo: implement in editor
    private float minCastDistance;

    public float MinCastDistance { get { return minCastDistance; } set { minCastDistance = value; } }
    private float maxCastDistance;
    public float MaxCastDistance { get { return MaxCastDistance; } set { MaxCastDistance = value; } }
    #endregion

    [SerializeField]
	public int levelLearnt;
    public MoveData(int mId, string mName, float mPP, float mPower, float mCooldown, string mDescription, Type mType, MoveEffect mEffect)
    {
        this.ID = mId;
        this.Name = mName;
        this.PP = mPP;
        this.Power = mPower;
        this.MoveCooldown = mCooldown;
        this.Description = mDescription;
        //this.MoveType = mType;
        this.MoveEffect = mEffect;
    }
    public MoveData(MoveData move)
    {
        this.ID = move.ID;
        this.Name = move.Name;
        this.PP = move.PP;
        this.Power = move.Power;
        this.MoveCooldown = move.MoveCooldown;
        this.Description = move.Description;
       // this.MoveType = move.MoveType;
        this.MoveEffect = move.MoveEffect;
    }
    public MoveData()
    {
        this.ID = 0;
        this.Name = "";
        this.PP = 0;
        this.Power = 0;
        this.MoveCooldown = 0;
        this.Description = "";

        this.MoveEffect = MoveEffect.None;
    }

}
[System.Serializable]
public enum MoveCategory
{
	None,
	Physical,
	Special,
	Buff,
	Debuff,
	Passive

}
[System.Serializable]
public enum MoveType
{
    None,
    Normal,
    Water,
    Fire,
    Ground,
    Electricity,
    Grass,
    Bug,
    Ghost,
    Dark,
    Ice,
    Fighting,
    Steel,
    Poison,
    Flying,
    Psychic
}
[System.Serializable]
public enum MoveEffect 
{
    None,
    Burn,
    Freeze,
    Drain,
    Poisons,
    LowerDef,
    LowerAtk,
    Slow,
    Blind,
    RaiseSpecialAttack,
    RaiseAttack,
    RaiseDefence,
    RaiseSpecialDefence,
    RaiseSpeed,
    Sleep,
    CopyCat,
    ReduceFoeHPToHalf,
    Paralyse,
    NeverMiss,
    ProtectFromSpecialAttacks,
    MakesACopyOfHisSelf,
    AreaEffect,
    ConstantDamage,
    Confuse
}