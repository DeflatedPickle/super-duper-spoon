using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class LivingStats : MonoBehaviour {
    public float health;
    public float currentHealth;

    public bool canMove = true;
    public bool isStuck;

    [CanBeNull] public GameObject stuckTo;

    [CanBeNull] public GameObject equippedWeapon;
    
    private void Awake() {
        currentHealth = health;
    }
}