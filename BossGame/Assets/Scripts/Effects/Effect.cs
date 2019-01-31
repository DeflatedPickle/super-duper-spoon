﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour {
    public enum EffectType {
        DamageMelee,
        DamageRanged,
        DamageArea
    }

    public enum InflictType {
        Single,
        Tick
    }

    /// <summary>
    /// The type the effect is
    /// </summary>
    public EffectType TypeEffect;

    /// <summary>
    /// How long the effect will be applied on the entity
    /// </summary>
    public float EffectLength;

    /// <summary>
    /// The amount inflicted on the entity
    /// </summary>
    public float InflictAmount;

    /// <summary>
    /// An ammount applied to the inflict amount
    /// </summary>
    public float InflictMultiplier;

    /// <summary>
    /// The type the inflict is
    /// </summary>
    public InflictType TypeInflict;

    /// <summary>
    /// The entity with the effect
    /// </summary>
    public GameObject Entity;

    void Start() {
    }

    void Update() {
    }
}