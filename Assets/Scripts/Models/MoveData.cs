using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
/// <summary>
///  Each pokemon move is refereced by its moveData
/// </summary>
[System.Serializable]
public class MoveData : AssetItem
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
    public DeamageType MoveDemageType;

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
    private float m_pp;
    public float PP { get { return m_pp; } set { m_pp = value; } }
    private float m_power;
    public float Power { get { return m_power; } set { m_power = value; } }
    private bool m_isoncooldown = false;
    public bool IsOnCooldown { get { return m_isoncooldown; } set { m_isoncooldown = value; } }
    private float m_movecooldown;
    public float MoveCooldown { get { return m_movecooldown; } set { m_movecooldown = value; } }
    private DeamageType m_movetype;
    public DeamageType MoveType { get { return m_movetype; } set { m_movetype = value; } }
    private MoveEffect m_moveeffect;
    public MoveEffect MoveEffect { get { return m_moveeffect; } set { m_moveeffect = value; } }
    private AudioClip m_moveaudio = null;
    public AudioClip MoveAudio { get { return m_moveaudio; } set { m_moveaudio = value; } }
    private GameObject m_movevfxprefab = null;
    public GameObject MoveVFXPrefab { get { return m_movevfxprefab; } set { m_movevfxprefab = value; } }

    public MoveData(int mId, string mName, float mPP, float mPower, float mCooldown, string mDescription, DeamageType mType, MoveEffect mEffect)
    {
        m_id = mId;
        m_name = mName;
        m_pp = mPP;
        m_power = mPower;
        m_movecooldown = mCooldown;
        m_description = mDescription;
        m_movetype = mType;
        m_moveeffect = mEffect;
    }

}
public enum DeamageType
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
    Poison
}
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
    Sleep
}