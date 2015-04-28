using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
///  Each pokemon move is refereced by its moveData
/// </summary>
public class MoveData
{
    /// <summary>
    /// MoveName
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// PPCost for the Move
    /// </summary>
    public float PP { get; set; }
    /// <summary>
    /// Accuracy? We might need this later
    /// </summary>
    public float Accuracy { get; set; }
    /// <summary>
    /// Move ID reference
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Which Pokemon ID is referencing this move
    /// </summary>
    public float PokemonId { get; set; }
    /// <summary>
    /// How much Damage does the move cause
    /// </summary>
    public float Power { get; set; }
    public string LongText { get; set; }
    public string ShortEffect { get; set; }
    /// <summary>
    ///  Type of Move(Fire/Water .etc?
    /// </summary>
    public string DamageType { get; set; }
    private float cooldown;
    public float Cooldown
    {
        get { return cooldown; }
        set { cooldown = value; CooldownTimer = value; }
    }
    public float CooldownTimer { get; set; }

    public bool IsOnCooldown = false;
    public string IconName { get; set; }
    public string Description { get; set; }
    public string AudioClipName { get; set; }
    public GameObject VFXPrefab { get; set; }

    public GameObject MoveUI { get; set; }



}
