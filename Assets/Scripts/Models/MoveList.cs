using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
/// <summary>
///  Each pokemon move is refereced by its moveData
/// </summary>
[System.Serializable]
public class MoveData
{
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
    Dark
}