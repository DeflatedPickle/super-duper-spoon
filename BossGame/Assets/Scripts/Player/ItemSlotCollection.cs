using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// ReSharper disable once CheckNamespace
public class ItemSlotCollection : MonoBehaviour {
    public GameObject parent;
    public bool slotsOpen;
    public int selectedSlot;

    public float rotation;

    public Sprite gildedSprite;
    public GameObject gildedObject;

    private LivingStats _livingStats;

    private void Awake() {
        _livingStats = parent.GetComponent<LivingStats>();
    }

    private void Start() {
        gildedObject = new GameObject();
        gildedObject.transform.parent = transform;
        
        var spriteRenderer = gildedObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = gildedSprite;
    }

    private void Update() {
        if (Input.GetKeyDown("i")) {
            slotsOpen = !slotsOpen;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetAxis("Mouse ScrollWheel") < 0f) {
            if (parent.transform.childCount - 1 >= selectedSlot) {
                parent.transform.GetChild(0).GetChild(selectedSlot).gameObject.SetActive(false);
            }
            
            if (selectedSlot - 1 >= 0) {
                selectedSlot--;
            }
            else {
                selectedSlot = transform.GetChild(0).childCount - 1;
            }

            if (parent.transform.childCount - 1 >= selectedSlot) {
                _livingStats.equippedWeapon = parent.transform.GetChild(0).GetChild(selectedSlot).gameObject;
                parent.transform.GetChild(0).GetChild(selectedSlot).gameObject.SetActive(true);
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetAxis("Mouse ScrollWheel") > 0f) {
            if (parent.transform.childCount - 1 >= selectedSlot) {
                parent.transform.GetChild(0).GetChild(selectedSlot).gameObject.SetActive(false);
            }
            
            if (selectedSlot + 1 < transform.GetChild(0).childCount) {
                selectedSlot++;
            }
            else {
                selectedSlot = 0;
            }

            if (parent.transform.childCount - 1 >= selectedSlot) {
                _livingStats.equippedWeapon = parent.transform.GetChild(0).GetChild(selectedSlot).gameObject;
                parent.transform.GetChild(0).GetChild(selectedSlot).gameObject.SetActive(true);
            }
        }

        // Show the slots
        if (slotsOpen) {
            var centre = parent.transform.position;

            foreach (var i in Enumerable.Range(0, transform.GetChild(0).childCount)) {
                var radius = 2.4f;
                rotation -= 0.0004f;
                if (rotation <= 0) rotation = 360;

                var t = 2 * Math.PI * i / transform.GetChild(0).childCount;

                if (selectedSlot == i) {
                    radius += 0.4f;
                }
                
                var x = (float) (centre.x + radius * Math.Cos(t + rotation));
                var y = (float) (centre.y + radius * Math.Sin(t + rotation));

                transform.GetChild(0).GetChild(i).position = new Vector3(x, y, -6);

                if (selectedSlot == i) {
                    gildedObject.transform.position = new Vector3(x, y, -7);
                }
            }

            transform.GetChild(0).gameObject.SetActive(true);
            gildedObject.SetActive(true);
        }
        // Hide the slots
        else {
            transform.GetChild(0).gameObject.SetActive(false);
            gildedObject.SetActive(false);
        }
    }
}