using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
public class WeaponThrown : Weapon {
    public GameObject replacementObject;

    public bool inAir;
    public bool stuckInObject;
    public GameObject stuckObject;
    public bool returning;

    private void Update() {
        // Attack
        if (Input.GetMouseButtonDown(0)) {
            if (stuckInObject) {
                transform.parent = null;

                returning = true;
            }
            else {
                if (!inAir) {
                    // ReSharper disable once Unity.InefficientPropertyAccess
                    transform.parent = null;

                    // _rigidbody2D.AddRelativeForce(Vector2.up * 140);

                    var mousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);
                    var direction = mousePosition - transform.position;
                    Rigidbody2D.AddForce(direction * 180);

                    inAir = true;
                    stuckInObject = false;

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
        }
        else {
            if (Vector3.Distance(transform.position, Player.transform.position) > 1.4f) {
                Rigidbody2D.velocity = Vector2.zero;
                inAir = false;
                stuckInObject = true;
            }
        }
    }

    private void OnDrawGizmos() {
        if (replacementObject.activeSelf) {
            Debug.DrawLine(replacementObject.transform.position, transform.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Enemy")) {
            transform.parent = other.gameObject.transform;

            Rigidbody2D.velocity = Vector2.zero;
            inAir = false;
            stuckInObject = true;

            // Stick the object more into the enemy
            transform.position = Vector3.MoveTowards(transform.position, other.transform.position, 0.1f);
        }
    }
}