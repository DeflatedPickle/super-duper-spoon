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

    private GameObject _knifePoint;
    private GameObject _handlePoint;

    private void Start() {
        _knifePoint = transform.GetChild(0).gameObject;
        _handlePoint = replacementObject.transform.GetChild(0).gameObject;
    }

    private void Update() {
        // Attack
        if (Input.GetMouseButtonDown(0)) {
            // Pick it up
            if (stuckInObject || stuckInFloor) {
                transform.parent = null;
                ImmobilizeEntity.UnstickEntity();

                returning = true;
            }
            else {
                // Throw it
                if (!inAir) {
                    // ReSharper disable once Unity.InefficientPropertyAccess
                    transform.parent = null;

                    // _rigidbody2D.AddRelativeForce(Vector2.up * 140);

                    var mousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);
                    mousePosition.z = 0f;
                    var direction = (mousePosition - transform.position).normalized;
                    Rigidbody2D.AddForce(direction * 180);
                    PlayerRigidbody2D.AddForce(direction * 80);

                    inAir = true;
                    stuckInObject = false;
                    stuckInFloor = false;

                    returning = false;

                    // Stop looking at the mouse when we throw it
                    LookAtMouse.shouldLook = false;

                    replacementObject.SetActive(true);
                }
            }
        }

        if (returning) {
            transform.position = Hand.transform.position;
            transform.parent = Hand.transform;

            replacementObject.SetActive(false);

            LookAtMouse.shouldLook = true;

            inAir = false;
            stuckInObject = false;
            stuckInFloor = false;
        }
        else {
            // It hit the floor
            if (!stuckInObject && Vector3.Distance(transform.position, Player.transform.position) > 1.4f) {
                Rigidbody2D.velocity = Vector2.zero;
                inAir = false;
                stuckInObject = false;
                stuckInFloor = true;

                transform.rotation = new Quaternion(-3, 2, 0f, 0f);
            }
        }
    }

    private void OnDrawGizmos() {
        if (replacementObject.activeSelf) {
            Debug.DrawLine(_handlePoint.transform.position, _knifePoint.transform.position);
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
            transform.position = Vector3.MoveTowards(transform.position, other.transform.position, 0.1f);

            stuckObject = other.gameObject;
            ImmobilizeEntity.StickEntity();
        }
    }
}