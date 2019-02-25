using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

// ReSharper disable once CheckNamespace
public class BossPhases : MonoBehaviour {
    public int currentPhase;

    private GameObject _player;
    private Stats _stats;

    public bool isCharging;
    private int _chargeChance;
    private int _chargePhase;

    private Vector2? _chargeDestination;

    private void Awake() {
        _player = GameObject.Find("Player");

        _stats = GetComponent<Stats>();
    }

    private void Update() {
        switch (currentPhase) {
            case 0:
                PhaseZero();
                break;

            case 1:
                PhaseOne();
                break;

            case 2:
                break;

            case 3:
                break;
        }
    }

    // The boss can't attack, they just wander slowly towards the player
    // Once hit, they'll move to phase one
    private void PhaseZero() {
        if (_stats.health < _stats.currentHealth) {
            currentPhase = 1;
        }
        else if (Vector3.Distance(transform.position, _player.transform.position) > 0.5f) {
            transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, 
                0.2f * Time.deltaTime);
        }
    }

    // The boss moves quickly towards the player, doing occasional charges towards them
    // The player can trick them into charging into a wall, damaging the boss and causing confusion
    private void PhaseOne() {
        if (!isCharging) {
            _chargeChance = Random.Range(0, 20);
        
            if (Vector3.Distance(transform.position, _player.transform.position) > 1f) {
                transform.position = Vector3.MoveTowards(transform.position, _player.transform.position,
                    2f * Time.deltaTime);
            }
        }
        else {
            if (_chargeDestination == null) {
                _chargeDestination = _player.transform.position;
            }
        }
    }
}