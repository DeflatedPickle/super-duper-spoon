using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
public class Weapon : Item {
    public enum AttackType {
        // Stab,
        Swing,
        Throw,
        Shoot
    }

    public enum SecondaryType {
        BlockMelee,
        BlockRanged,
        BlockBoth
    }

    /// <summary>
    /// The type of attack this weapon uses
    /// </summary>
    public AttackType attackType;

    /// <summary>
    /// The secondary type this weapon has
    /// </summary>
    public SecondaryType secondaryType;

    /// <summary>
    /// The amount of damage applied by the weapon
    /// </summary>
    public float weaponDamage;

    /// <summary>
    /// If or not the weapon is attacking
    /// </summary>
    public bool isAttacking;

    /// <summary>
    /// The amount of time the weapon can apply damage for
    /// </summary>
    public float attackTime;

    private float _attackTimeCounter;

    /// <summary>
    /// If or not the weapon is setting up an attack
    /// </summary>
    public bool isSettingUp;

    /// <summary>
    /// The amount of time it takes to setup an attack
    /// </summary>
    public float setupTime;

    private float _setupTimeCounter;

    /// <summary>
    /// If or not the weapon is recovering from an attack
    /// </summary>
    public bool isRecovering;

    /// <summary>
    /// The amount of time it takes to recover from an attack
    /// </summary>
    public float recoverTime;

    private float _recoverTimeCounter;

    /// <summary>
    /// The amount of melee damage that can be blocked (-1 for all)
    /// </summary>
    public float blockMeleeAmount;

    /// <summary>
    /// The amount of ranged damage that can be blocked (-1 for all)
    /// </summary>
    public float blockRangeAmount;

    private bool _setGeneralCounter;
    private float _generalWaitCounter;

    private void Update() {
        // Attack
        if (Input.GetMouseButtonDown(0)) {
            switch (attackType) {
                case AttackType.Swing:
                    if (_attackTimeCounter <= 0f) {
                        // If it's not recovering
                        if (_recoverTimeCounter <= 0f) {
                            // If it's not setting up
                            if (_setupTimeCounter <= 0f) {
                                _setupTimeCounter = setupTime;
                                isSettingUp = true;
                                
                                transform.rotation = entity.transform.rotation;
                            }
                        }
                    }

                    break;

                case AttackType.Throw:
                    break;

                case AttackType.Shoot:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        // Secondary
        else if (Input.GetMouseButtonDown(1)) {
            switch (secondaryType) {
                case SecondaryType.BlockMelee:
                    break;

                case SecondaryType.BlockRanged:
                    break;

                case SecondaryType.BlockBoth:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        if (isSettingUp) {
            if (_setupTimeCounter > 0f) {
                Debug.Log("Perform setup");
                _setupTimeCounter--;

                switch (attackType) {
                    case AttackType.Swing:
                        transform.Rotate(0f, 0f, (setupTime * -1) * 2 / 10);
                        break;

                    case AttackType.Throw:
                        break;

                    case AttackType.Shoot:
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else if (_setupTimeCounter == 0f) {
                _setGeneralCounter = true;
                _setupTimeCounter--;
            }
            else if (_setupTimeCounter < 0f) {
                if (_setGeneralCounter) {
                    _generalWaitCounter = 16;
                    _setGeneralCounter = false;
                }

                if (_generalWaitCounter > 0) {
                    Debug.Log("Waiting...");
                    _generalWaitCounter--;
                }
                else {
                    _attackTimeCounter = attackTime;
                    isAttacking = true;
                    isSettingUp = false;
                }
            }
        }

        if (isAttacking) {
            if (_attackTimeCounter > 0f) {
                Debug.Log("Perform attack");
                _attackTimeCounter--;

                switch (attackType) {
                    case AttackType.Swing:
                        transform.Rotate(0f, 0f, attackTime * 2 / 10);
                        break;

                    case AttackType.Throw:
                        break;

                    case AttackType.Shoot:
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else if (_attackTimeCounter == 0f) {
                _setGeneralCounter = true;
                _attackTimeCounter--;
            }
            else if (_attackTimeCounter <= 0f) {
                if (_setGeneralCounter) {
                    _generalWaitCounter = 26;
                    _setGeneralCounter = false;
                }

                if (_generalWaitCounter > 0) {
                    Debug.Log("Waiting...");
                    _generalWaitCounter--;
                }
                else {
                    _recoverTimeCounter = recoverTime;
                    isRecovering = true;
                    isAttacking = false;
                }
            }
        }

        if (isRecovering) {
            if (_recoverTimeCounter > 0f) {
                Debug.Log("Perform recover");
                _recoverTimeCounter--;

                switch (attackType) {
                    case AttackType.Swing:
                        transform.Rotate(0f, 0f, (recoverTime * -1) / 10);
                        break;

                    case AttackType.Throw:
                        break;

                    case AttackType.Shoot:
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}