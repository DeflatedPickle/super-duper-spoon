using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <inheritdoc />
/// <summary>
/// An effect applied by a weapon or a charm
/// </summary>
// ReSharper disable once CheckNamespace
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
    public EffectType effectType;

    /// <summary>
    /// How long the effect will be applied on the entity
    /// </summary>
    public float effectLength;

    /// <summary>
    /// The amount inflicted on the entity
    /// </summary>
    public float inflictAmount;

    /// <summary>
    /// An ammount applied to the inflict amount
    /// </summary>
    public float inflictMultiplier;

    /// <summary>
    /// The type the inflict is
    /// </summary>
    public InflictType typeInflict;

    /// <summary>
    /// The entity with the effect
    /// </summary>
    public GameObject entity;

    void Start() {
    }

    void Update() {
    }
}