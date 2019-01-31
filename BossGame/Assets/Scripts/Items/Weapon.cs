using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
public class Weapon : Item {
    /// <summary>
    /// The amount of damage applied by the weapon during the swing time
    /// </summary>
    public float ItemDamage;

    /// <summary>
    /// The amount of time the weapon can apply damage for
    /// </summary>
    public float AttackTime;

    /// <summary>
    /// If or not the weapon has been swung
    /// </summary>
    public float IsRecovering;

    /// <summary>
    /// The amount of time it takes to recover from an attack
    /// </summary>
    public float RecoverTime;

    /// <summary>
    /// If the weapon can block melee attacks
    /// </summary>
    public bool CanBlockMelee;

    /// <summary>
    /// The amount of melee damage that can be blocked (-1 for all)
    /// </summary>
    public float BlockMeleeAmount;

    /// <summary>If the weapon can block ranged attacks</summary>
    public bool CanBlockRanged;

    /// <summary>
    /// The amount of ranged damage that can be blocked (-1 for all)
    /// </summary>
    public float BlockRangeAmount;

    private void Start() {
    }

    private void Update() {
        // Attack
        if (Input.GetMouseButtonDown(0)) {
            
        }
    }
}