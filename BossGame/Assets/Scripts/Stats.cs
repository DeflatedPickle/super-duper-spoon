﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {
    public float health;
    public float currentHealth;
    
    private void Awake() {
        currentHealth = health;
    }
}