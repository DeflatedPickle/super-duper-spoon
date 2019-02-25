using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Stats : MonoBehaviour {
    public float health;
    public float currentHealth;

    public bool canMove = true;
    public bool isStuck;

    [CanBeNull] public GameObject stuckTo;
    
    private void Awake() {
        currentHealth = health;
    }
}