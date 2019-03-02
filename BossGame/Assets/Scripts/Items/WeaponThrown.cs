using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
public class WeaponThrown : Weapon {
    public GameObject replacementObject;

    public bool inAir;
    public bool stuckInFloor;
    public bool stuckInObject;
    public GameObject stuckObject;
    public bool returning;

    public GameObject chains;
    public float chainLength = 4.6f;

    // Sticky icky UWU
    public bool firstStick;

    public Rigidbody2D stuckObjectRigidBody;

    private GameObject _knifePoint;
    private GameObject _handlePoint;

    private Vector3 _offset;

    private Vector3 _knifePointPosition;
    private Vector3 _handlePointPosition;

    private void Start() {
        _knifePoint = transform.GetChild(0).gameObject;
        _handlePoint = replacementObject.transform.GetChild(0).gameObject;

        _knifePointPosition = _knifePoint.transform.localPosition;
        _handlePointPosition = _handlePoint.transform.localPosition;
    }

    protected new void Update() {
        base.Update();
        _knifePoint.transform.localPosition = _knifePointPosition;
        _handlePoint.transform.localPosition = _handlePointPosition;

        // It hit a thing
        if (stuckInObject || stuckInFloor) {
            if (stuckInObject) {
                transform.localPosition = _offset;

                if (firstStick) {
                    firstStick = false;

                    stuckObjectRigidBody.AddForce(stuckObject.transform.position - Player.transform.position * 14);
                }
            }
            
            // Pull it back
            // TODO: Maybe make it pull back by spinning around, like fishing
            if (Input.GetMouseButtonDown(0)) {
                transform.parent = null;

                if (stuckInObject) {
                    stuckObjectRigidBody.AddForce((Player.transform.position - stuckObject.transform.position) * 36);
                    PlayerRigidbody2D.AddForce((Player.transform.position - stuckObject.transform.position) * 20);
                }
                
                ImmobilizeEntity.UnstickEntity();

                returning = true;
            }
        }
        else {
            if (MethodPassThrough.throwingWeapon) {
                MethodPassThrough.throwingWeapon = false;

                LookAtMouse.shouldLook = true;

                ThrowWeapon();
            }
        }

        if (returning) {
            transform.position = Hand.transform.position;
            transform.parent = Hand.transform;

            // replacementObject.SetActive(false);

            LookAtMouse.shouldLook = true;

            inAir = false;
            stuckInObject = false;
            stuckInFloor = false;
        }
        else {
            // It hit the floor
            if (!stuckInObject && Vector3.Distance(transform.position, Player.transform.position) > chainLength) {
                Rigidbody2D.velocity = Vector2.zero;
                inAir = false;
                stuckInObject = false;
                stuckInFloor = true;

                transform.rotation = new Quaternion(-3, 2, 0f, 0f);
            }
        }
    }

    private void ThrowWeapon() {
        // Throw it
        if (!inAir) {
            // ReSharper disable once Unity.InefficientPropertyAccess
            transform.parent = null;

            var mousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            var direction = (mousePosition - Player.transform.position).normalized;
            
            Rigidbody2D.AddForce(direction * 380);
            PlayerRigidbody2D.AddForce(direction * 80);

            inAir = true;
            stuckInObject = false;
            stuckInFloor = false;

            returning = false;

            // Stop looking at the mouse when we throw it
            LookAtMouse.shouldLook = false;

            replacementObject.SetActive(true);

            firstStick = true;
        }
    }

    private void OnDrawGizmos() {
        if (replacementObject != null) {
            if (replacementObject.activeSelf) {
                if (_handlePoint != null && _knifePoint != null) {
                    Debug.DrawLine(_handlePoint.transform.position, _knifePoint.transform.position);
                }
            }
        }
    }

    protected new void OnTriggerEnter2D(Collider2D other) {
        base.OnTriggerEnter2D(other);
        if (other.gameObject.CompareTag("Enemy") && inAir) {
            transform.parent = other.gameObject.transform;

            Rigidbody2D.velocity = Vector2.zero;
            inAir = false;
            stuckInObject = true;
            stuckInFloor = false;

            // Stick the object more into the enemy
            transform.position = Vector3.MoveTowards(transform.position, other.transform.position, 0.6f);
            _offset = transform.localPosition;

            stuckObject = other.gameObject;
            stuckObjectRigidBody = stuckObject.GetComponent<Rigidbody2D>();
            ImmobilizeEntity.StickEntity();
        }
    }

    private void OnDisable() {
        replacementObject.SetActive(false);
        chains.SetActive(false);
    }

    private void OnEnable() {
        replacementObject.SetActive(true);
        chains.SetActive(true);
    }
}