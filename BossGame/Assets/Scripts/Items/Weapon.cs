using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
public class Weapon : Item {
    /// <summary>
    /// The amount of damage applied by the weapon
    /// </summary>
    public float weaponDamage;

    /// <summary>
    /// The amount of time the weapon can apply damage for
    /// </summary>
    public float attackTime;

    /// <summary>
    /// If or not the weapon has been swung
    /// </summary>
    public bool isRecovering;

    /// <summary>
    /// The amount of time it takes to recover from an attack
    /// </summary>
    public float recoverTime;

    /// <summary>
    /// If the weapon can block melee attacks
    /// </summary>
    public bool canBlockMelee;

    /// <summary>
    /// The amount of melee damage that can be blocked (-1 for all)
    /// </summary>
    public float blockMeleeAmount;

    /// <summary>If the weapon can block ranged attacks</summary>
    public bool canBlockRanged;

    /// <summary>
    /// The amount of ranged damage that can be blocked (-1 for all)
    /// </summary>
    public float blockRangeAmount;

    private void Start() {
    }

    private void Update() {
        // Attack
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("Attacking");
        }
        // Block
        else if (Input.GetMouseButtonDown(1)) {
            Debug.Log("Blocking");
        }
    }
}