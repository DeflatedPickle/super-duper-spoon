using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour {
    [Range(1, 10)] public int MoveForce = 8;

    [Range(1, 10)] public float DashForce = 3;
    [Range(0, 4)] public float DashTime = 0.4f;
    private float _dashTimer;
    private bool _doDash;

    private int _facingHorizontal = 1;
    private int _facingVertical = 1;

    private Rigidbody2D _rigidbody2D;

    private bool _north;
    private bool _south;
    private bool _east;
    private bool _west;

    void Awake() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update() {
        var moveCalc = new Vector2();

        // Movement
        if (Input.GetKey(KeyCode.A)) {
            _facingHorizontal = -1;
            moveCalc.x = _facingHorizontal * MoveForce;
            _west = true;
        }
        else if (Input.GetKey(KeyCode.D)) {
            _facingHorizontal = 1;
            moveCalc.x = _facingHorizontal * MoveForce;
            _east = true;
        }

        if (Input.GetKey(KeyCode.W)) {
            moveCalc.y = MoveForce;
            _north = true;
        }
        else if (Input.GetKey(KeyCode.S)) {
            moveCalc.y = -MoveForce;
            _south = true;
        }

        // Dash
        if (Input.GetMouseButtonDown(1)) {
            _doDash = true;
        }

        if (_doDash) {
            if (_dashTimer <= 0) {
                _dashTimer = DashTime;
                _rigidbody2D.velocity = Vector2.zero;

                _doDash = false;
                _north = false;
                _south = false;
                _east = false;
                _west = false;
            }
            else {
                _dashTimer -= Time.deltaTime;

                // North
                if (_north && !_south && !_east && !_west) {
                    _rigidbody2D.velocity = Vector2.up * DashForce;
                }
                // South
                if (!_north && _south && !_east && !_west) {
                    _rigidbody2D.velocity = Vector2.down * DashForce;
                }
                // East
                if (!_north && !_south && _east && !_west) {
                    _rigidbody2D.velocity = Vector2.right * DashForce;
                }
                // West
                if (!_north && !_south && !_east && _west) {
                    _rigidbody2D.velocity = Vector2.left * DashForce;
                }
            }
        }

        _rigidbody2D.AddForce(moveCalc);
    }
}