using System;
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

    public bool doesStick;

    protected GameObject Player;
    protected Rigidbody2D PlayerRigidbody2D;
    protected GameObject Hand;
    protected ImmobilizeEntity ImmobilizeEntity;
    protected Rigidbody2D Rigidbody2D;
    protected Camera Camera;
    protected LookAtMouse LookAtMouse;
    protected MethodPassThrough MethodPassThrough;
    protected WeaponThrown WeaponThrown;
    
    private Animator _animator;

    private static readonly int IsSwinging = Animator.StringToHash("IsSwinging");
    private static readonly int IsThrowing = Animator.StringToHash("IsThrowing");

    private void Awake() {
        Player = GameObject.Find("Player");
        PlayerRigidbody2D = Player.GetComponent<Rigidbody2D>();
        Hand = Player.transform.GetChild(0).gameObject;
        ImmobilizeEntity = Hand.GetComponent<ImmobilizeEntity>();
        MethodPassThrough = Hand.GetComponent<MethodPassThrough>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Camera = FindObjectOfType<Camera>();
        LookAtMouse = GetComponent<LookAtMouse>();
        WeaponThrown = GetComponent<WeaponThrown>();
        
        _animator = transform.parent.GetComponent<Animator>();
    }

    protected void Update() {
        // Attack
        if (Input.GetMouseButtonDown(0)) {
            switch (attackType) {
                case AttackType.Swing:
                    if (!_animator.GetBool(IsSwinging)) {
                        _animator.SetTrigger(IsSwinging);
                    }

                    break;

                case AttackType.Throw:
                    if (WeaponThrown.returning) {
                        if (!_animator.GetBool(IsThrowing)) {
                            _animator.SetTrigger(IsThrowing);

                            LookAtMouse.shouldLook = false;
                            WeaponThrown.returning = false;
                        }
                    }
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
    }

    protected void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Enemy")) {
            if (doesStick) {
                ImmobilizeEntity.entity = other.gameObject;
                ImmobilizeEntity.entityStats = other.gameObject.GetComponent<LivingStats>();
                ImmobilizeEntity.entityStats.stuckTo = gameObject;
                
            }
        }
    }
}